using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Ground;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Environment.Tiles
{
    class TileGround
    {
        #region "Fields"

        private int x;
        private int y;
        private TileGroundTypes type;

        #endregion

        #region "Constructors"

        public TileGround(int x, int y, TileGroundTypes type)
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
            if (type == TileGroundTypes.Grass)
            {
                return new Grass(grid, x, y);
            }
            else if (type == TileGroundTypes.Water)
            {
                return new Water(grid, x, y);
            }

            return null;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return "ground " + type.ToString() + " " + x + " " + y;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
