using Assets.Scripts.Grid.GridObjects;
using UnityEngine;

namespace Assets.Scripts.Grid.Other
{
    class Fog : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Fog(Grid grid, int width, int height) : base(grid, 0, 0, width, height)
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
            GameObject mist = ObjectPool.Instantiate("fog");
            mist.transform.position = new Vector3(x + X + Width / 2f, 0, y + Y + Height / 2f);
            ParticleSystem.ShapeModule shape = mist.GetComponent<ParticleSystem>().shape;
            shape.box = new Vector3(Width - 1, Height - 1, 4);
            mist.transform.SetParent(parent);

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
