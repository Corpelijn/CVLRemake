using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Buildings
{
    class Castle : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Castle(Grid grid, int x, int y) : base(grid, x, y, 7, 8)
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
            GameObject castle = ObjectPool.Instantiate("castle");
            castle.transform.position = new Vector3(x + X, 0, y + Y);
            castle.transform.SetParent(parent);
            //castle.GetComponentInChildren<ClickInputObject>().SourceObject = this;
            gameObjects.Add(castle);

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
