using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Buildings
{
    class Vault : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Vault(Grid grid, int x, int y) : base(grid, x, y, 5, 6)
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
            GameObject vault = ObjectPool.Instantiate("vault");
            vault.transform.position = new Vector3(x + X, 0, y + Y);
            vault.transform.SetParent(parent);
            //castle.GetComponentInChildren<ClickInputObject>().SourceObject = this;
            gameObjects.Add(vault);

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
