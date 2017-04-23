using Assets.Scripts.Environment;
using Assets.Scripts.Game.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor.SceneManagement;
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
        public GameObject ProgressBarToDisable = null;
        public RectTransform Progressbar = null;

        private Dictionary<string, TodoAction> actions;
        private KeyValuePair<string, TodoAction> currentAction;

        private List<string> files;
        private float actionStepSize;
        private float progress;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private IEnumerator StartUp()
        {
            files = new List<string>();
            progress = 0;
            actionStepSize = 1f / actions.Count;

            yield return new WaitForEndOfFrame();

            progress += actionStepSize;

            SetNextAction();
        }

        private IEnumerator CheckFiles()
        {
            string[] directories;
            
            if (Application.isEditor)
                directories = GameContentImport.INSTANCE.DebugDirectoryToRead;
            else
                directories = GameContentImport.INSTANCE.DirectoryToRead;

            foreach (string directory in directories)
            {
                files.AddRange(Directory.GetFiles(directory, "*", SearchOption.AllDirectories));
            }

            float fileStep = actionStepSize / files.Count;

            foreach (string file in files)
            {
                progress += fileStep;
                yield return new WaitForEndOfFrame();
            }

            SetNextAction();
        }

        private IEnumerator ReadFiles()
        {
            float fileStep = actionStepSize / files.Count;
            FileContentLoader loader = new FileContentLoader();

            foreach(string file in files)
            {
                loader.ParseFile(file);
                progress += fileStep;

                yield return new WaitForEndOfFrame();
            }
            loader.StitchTogether();
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

            Label.gameObject.SetActive(false);
            ProgressBarToDisable.gameObject.SetActive(false);

            progress += actionStepSize / 2f;

            yield return new WaitForEndOfFrame();

            SetNextAction();
        }

        private IEnumerator Unload()
        {
            progress += actionStepSize;

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

        private void SetNextAction()
        {
            if (currentAction.Key != null)
                actions.Remove(currentAction.Key);

            currentAction = actions.First();
            Label.text = currentAction.Key;
            StartCoroutine(currentAction.Value.Invoke());
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
            Progressbar.localScale = new Vector3(progress, 1, 1);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
