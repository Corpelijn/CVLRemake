using Assets.Scripts.Environment.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Input
{
    class CurrentSelection
    {
        #region "Fields"

        private Tile selectedTile;

        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static CurrentSelection instance;

        public static CurrentSelection INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new CurrentSelection();
                return instance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void SelectTile(Tile tile)
        {
            if (selectedTile != null)
            {
                selectedTile.IsSelected = false;
            }

            if (tile != null)
            {
                tile.IsSelected = true;
            }

            selectedTile = tile;
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
