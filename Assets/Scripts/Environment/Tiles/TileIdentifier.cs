using Assets.Scripts.Grid.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Environment.Tiles
{
    class TileIdentifier : MonoBehaviour
    {
        #region "Fields"

        private Tile tile;
        private TileInformation information;

        public bool Fog;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Tile Tile
        {
            get { return tile; }
            set { tile = value; }
        }

        public TileInformation Information
        {
            get { return information; }
            set { information = value; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Update()
        {
            if (tile != null)
            {
                Fog = tile.Fog;
                if (tile.IsSelected)
                {
                    information.UpdateParticleSystems();
                }
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
