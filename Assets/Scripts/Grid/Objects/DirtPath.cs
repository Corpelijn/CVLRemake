using Assets.Scripts.Environment;
using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Objects
{
    class DirtPath : Path
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public DirtPath(Grid grid, int x, int y) : base(grid, x, y)
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
            GameObject go = ObjectPool.GetNewObject("dirtpath");
            go.transform.position = new Vector3(x + X + Width / 2f, 0, y + Y + Height / 2f);
            go.transform.eulerAngles = new Vector3(0, rotation, 0);
            go.transform.SetParent(parent);

            gameObjects.Add(go);

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
