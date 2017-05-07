using Assets.Scripts.Environment.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Input
{
    class CameraMovement
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        private CameraMovement()
        {

        }

        #endregion

        #region "Singleton"

        private static CameraMovement instance;

        public static CameraMovement INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new CameraMovement();
                return instance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void FocusCamera(Tile tile)
        {
            Vector3 tilePosition = new Vector3(tile.GroundGrid.LowX, 0, tile.GroundGrid.LowY);
            FocusCamera(tilePosition);
        }

        public void FocusCamera(Vector3 position)
        {
            Vector3 correction = new Vector3(0f/*-11.5f*/, 7.5f, /*7.75f*/0f);
            Camera.main.transform.position = position + correction;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
