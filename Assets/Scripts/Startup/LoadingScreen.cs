using Assets.Scripts.Game.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Startup
{
    class LoadingScreen : MonoBaseClass<LoadingScreen>
    {
        #region "Fields"

        private delegate IEnumerator TodoAction();

        public Text Label = null;
        public Scrollbar Progressbar = null;

        private Dictionary<string, TodoAction> actions;
        private KeyValuePair<string, TodoAction> currentAction;

        private List<string> files;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private IEnumerator StartUp()
        {
            files = new List<string>();

            yield return new WaitForSeconds(2);

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

            foreach (string file in files)
            {
                Debug.Log(file);
                yield return new WaitForEndOfFrame();
            }

            SetNextAction();
        }

        private IEnumerator ReadFiles()
        {
            FileContentLoader loader = new FileContentLoader();
            loader.AddFiles(files.ToArray());
            loader.ParseContent();

            yield return new WaitForSeconds(2);

            SetNextAction();
        }

        private IEnumerator LoadUserData()
        {
            yield return new WaitForSeconds(2);

            SetNextAction();
        }

        private IEnumerator FadeOut()
        {
            yield return new WaitForSeconds(2);
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
                actions.Add("Done!", FadeOut);
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

        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
