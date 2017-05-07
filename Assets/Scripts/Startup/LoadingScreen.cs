using Assets.Scripts.Data;
using Assets.Scripts.Data.Database;
using Assets.Scripts.Environment;
using Assets.Scripts.Game;
using Assets.Scripts.Game.Content;
using Assets.Scripts.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Startup
{
    class LoadingScreen : MonoBaseClass<LoadingScreen>
    {
        #region "Fields"

        private delegate IEnumerator TodoAction();

        public Text Label = null;
        public GameObject ProgressBar = null;
        public Image background = null;
        public Image text = null;

        public GameObject EventSystem = null;
        public GameObject ErrorDialog = null;
        public Text ErrorText = null;

        private Dictionary<string, TodoAction> actions;
        private KeyValuePair<string, TodoAction> currentAction;

        private List<LocalFile> files;
        private float actionStepSize;
        private float progress;

        private float fadingStep = 0.75f;
        private float currentFade = 0f;
        private bool fading = false;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private IEnumerator StartUp()
        {
            files = new List<LocalFile>();
            progress = 0;
            actionStepSize = 1f / actions.Count;

            yield return new WaitForEndOfFrame();

            progress += actionStepSize;

            SetNextAction();
        }

        private IEnumerator CheckFiles()
        {
            DataTable results = null;
            using (MySqlDatabaseConnection connection = MySqlDatabaseConnection.GetConnection())
            {
                try
                {
                    connection.Open();
#if !UNITY_EDITOR
                    results = connection.ExecuteQuery("select count(*) as count from game where version=?", StaticValues.VERSION_NUMBER);
                    if (Convert.ToInt32(results.GetDataFromRow(0, "count")) == 0)
                    {
                        System.Diagnostics.Process.Start("updater.exe", "-current=" + StaticValues.VERSION_NUMBER + " -installdir=\"" + Path.GetFullPath(".") + "\"");
                        Application.Quit();
                    }
#endif
                    results = connection.ExecuteQuery("select filename, checksum from file");
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                    yield break;
                }
                yield return new WaitForEndOfFrame();
            }

            foreach (DataRow row in results)
            {
                files.Add(new LocalFile(row["filename"].ToString(), row["checksum"].ToString()));
                yield return new WaitForEndOfFrame();
            }

            try
            {
                ReadFilesFromDirectories();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                yield break;
            }

            progress += actionStepSize;

            SetNextAction();
        }

        private IEnumerator UpdateFiles()
        {
            if (files.Count > 0)
            {
                foreach (LocalFile file in files)
                {
                    try
                    {
                        if (file.NetworkFile && file.NeedsUpdate)
                        {
                            file.GetFromDatabase();
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowError(ex);
                        yield break;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }

            progress += actionStepSize;

            SetNextAction();
        }

        private IEnumerator ReadFiles()
        {
            float fileStep = actionStepSize / files.Count;
            FileContentLoader loader = new FileContentLoader();

            foreach (LocalFile file in files)
            {
                try
                {
                    loader.ParseFile(file.FullPath);
                    progress += fileStep;
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                    yield break;
                }

                yield return new WaitForEndOfFrame();
            }
            try
            {
                loader.StitchTogether();
            }
            catch (Exception ex)
            {
                ShowError(ex);
                yield break;
            }
            yield return new WaitForEndOfFrame();

            EnvironmentGenerator.INSTANCE.ZeroTile = loader.ZeroTile;
            EnvironmentGenerator.INSTANCE.enabled = true;

            yield return new WaitForEndOfFrame();

            SetNextAction();
        }

        private IEnumerator LoadUserData()
        {
            progress += actionStepSize;

            yield return new WaitForEndOfFrame();

            SetNextAction();
        }

        private IEnumerator FadeOut()
        {
            progress += actionStepSize / 2f;

            yield return SceneManager.LoadSceneAsync("game", LoadSceneMode.Additive);

            yield return new WaitForEndOfFrame();

            for (int j = 0; j < Camera.allCamerasCount; j++)
            {
                Transform currentCameraTransform = Camera.allCameras[j].transform;
                for (int i = 0; i < currentCameraTransform.childCount; i++)
                {
                    if (currentCameraTransform.GetChild(i).name == "ResourceMovementPoint")
                    {
                        ResourceDrawer.INSTANCE.ResourceMovementPoint = currentCameraTransform.GetChild(i);
                        break;
                    }
                }
            }

            progress += actionStepSize / 2f;

            yield return new WaitForEndOfFrame();

            SetNextAction();
        }

        private IEnumerator Unload()
        {
            progress += actionStepSize;

            fading = true;

            Label.gameObject.SetActive(false);
            ProgressBar.SetActive(false);

            yield return new WaitUntil(IsDoneFading);

            yield return SceneManager.UnloadSceneAsync("loadingScreen");

            yield return new WaitForEndOfFrame();
        }

        private void ReadDescriptions()
        {
            string filename = @"language\nl.language";
            if (!File.Exists(filename))
            {
                actions.Add("Initializing...", StartUp);
                actions.Add("Checking local files...", CheckFiles);
                actions.Add("Updating files...", UpdateFiles);
                actions.Add("Loading local files...", ReadFiles);
                actions.Add("Loading user progress...", LoadUserData);
                actions.Add("Finishing...", FadeOut);
                actions.Add("Done!", Unload);
            }
            else
            {
                // Read file
            }
        }

        private void ReadFilesFromDirectories()
        {
            string[] directories;

            if (Application.isEditor)
                directories = GameContentImport.INSTANCE.DebugDirectoryToRead;
            else
                directories = GameContentImport.INSTANCE.DirectoryToRead;

            directories = directories.Select(x => Path.GetFullPath(x)).Concat(new string[] { System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\MistKingdoms\" }).ToArray();

            foreach (string directory in directories)
            {
                try
                {
                    string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);
                    foreach (string file in files)
                    {
                        string relativePath = MakeRelative(file, directory);
                        string checksum = Checksum.Calculate(file);
                        LocalFile localFile = this.files.FirstOrDefault(x => x.Filename == relativePath);
                        if (localFile != null && localFile.Checksum == checksum)
                        {
                            localFile.NeedsUpdate = false;
                        }
                        else if (localFile != null)
                        {
                            localFile.BaseDirectory = directory;
                        }
                        else
                        {
                            this.files.Add(new LocalFile(relativePath, directory, checksum));
                        }
                        //Debug.Log(relativePath + " -> " + checksum);
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private string MakeRelative(string filePath, string referencePath)
        {
            Uri fileUri = new Uri(filePath);
            Uri referenceUri = new Uri(referencePath);
            return referenceUri.MakeRelativeUri(fileUri).ToString();
        }

        private void SetNextAction()
        {
            if (currentAction.Key != null)
                actions.Remove(currentAction.Key);

            currentAction = actions.First();
            Label.text = currentAction.Key;
            StartCoroutine(currentAction.Value.Invoke());
        }

        private bool IsDoneFading()
        {
            if (fading)
            {
                return currentFade >= 1f;
            }
            return false;
        }

        private void ShowError(Exception ex)
        {
            // Hide the progressbar
            Label.gameObject.SetActive(false);
            ProgressBar.SetActive(false);

            // Show the error dialog and information
            ErrorDialog.SetActive(true);
            EventSystem.SetActive(true);
            ErrorText.text = ex.ToString();
        }

        public void ExitOnError()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Start()
        {
            actions = new Dictionary<string, TodoAction>();

            ReadDescriptions();

            SetNextAction();
        }

        public override void Update()
        {
            if (fading)
            {
                background.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), currentFade);
                text.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), currentFade);
                currentFade += fadingStep * Time.deltaTime;
            }

            ProgressBar.GetComponent<Progressbar>().Value = progress;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
