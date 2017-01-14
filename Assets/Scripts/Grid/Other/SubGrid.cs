using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Other
{
    class SubGrid : GridObject
    {
        #region "Fields"

        private Grid grid;

        #endregion

        #region "Constructors"

        public SubGrid(Grid parent, Grid grid, int x, int y) : base(parent, x, y, grid.Width, grid.Height)
        {
            this.grid = grid;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void DrawObjects(float x, float y, Transform parent)
        {
            grid.Draw(x + X + grid.Width / 2f, y + Y + grid.Height / 2f, parent);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
