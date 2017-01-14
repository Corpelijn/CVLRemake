using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Buildings
{
    class Building : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        protected Building(Grid grid, int x, int y, int width, int height) : base(grid, x, y, width, height)
        {

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
            throw new NotImplementedException();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
