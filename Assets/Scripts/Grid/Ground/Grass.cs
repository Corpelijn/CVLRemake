using Assets.Scripts.Environment;
using Assets.Scripts.Grid.GridObjects;
using CoBa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Ground
{
    class Grass : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Grass(Grid grid, int x, int y) : base(grid, x, y, 1, 1)
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
            GameObject go = ObjectPool.Instantiate("grass");
            go.transform.position = new Vector3(x + X + Width / 2f, 0, y + Y + Height / 2f);
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
