using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Ground;
using Assets.Scripts.Grid.Objects;
using Assets.Scripts.Grid.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Environment.Tiles
{
    class TileObject
    {
        #region "Fields"

        private int x;
        private int y;
        private TileObjectTypes type;

        #endregion

        #region "Constructors"

        public TileObject(int x, int y, TileObjectTypes type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }

        #endregion

        #region "Properties"

        public int X
        {
            get { return x; }
        }

        public int Y
        {
            get { return y; }
        }

        #endregion

        #region "Methods"

        public GridObject GetGridObject(Grid.Grid grid)
        {
            if (type == TileObjectTypes.Tree)
            {
                return new Tree(grid, x, y);
            }
            else if (type == TileObjectTypes.Stonepath)
            {
                return new StonePath(grid, x, y);
            }
            else if (type == TileObjectTypes.Dirtpath)
            {
                return new DirtPath(grid, x, y);
            }

            return null;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "obj " + type.ToString() + " " + x + " " + y;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
