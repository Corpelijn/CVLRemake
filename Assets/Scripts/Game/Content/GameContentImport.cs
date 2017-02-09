using Assets.Scripts.Environment.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Content
{
    class GameContentImport : MonoBaseClass<GameContentImport>
    {
        #region "Fields"

        private ContentLoader loader;

        public bool ReadFromDirectory = true;
        public string[] DirectoryToRead = null;
        public string[] DebugDirectoryToRead = null;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Tile ZeroTile
        {
            get { return loader.ZeroTile; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Awake()
        {
            base.Awake();

            if (ReadFromDirectory)
            {
                loader = new FileContentLoader();

#if UNITY_EDITOR
                for (int i = 0; i < DebugDirectoryToRead.Length; i++)
                {
                    (loader as FileContentLoader).ParseDirectory(DebugDirectoryToRead[i]);
                    loader.ParseContent();
                }
#else
                for (int i = 0; i < DirectoryToRead.Length; i++)
			    {
                    (loader as FileContentLoader).ParseDirectory(DirectoryToRead[i]);
                    loader.ParseContent();
			    }
#endif
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
