using Assets.Scripts.Grid.GridObjects;
using CoBa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Other
{
    class Fog : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Fog(Grid grid, int x, int y, int width, int height) : base(grid, x, y, width, height)
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
            mist.transform.position = new Vector3(x + Width / 2f, 1, y + Height / 2f);
            ParticleSystem psystem = mist.GetComponent<ParticleSystem>();
            ParticleSystem.ShapeModule shape = psystem.shape;
            shape.box = new Vector3(Width, Height, 5);
            ParticleSystem.EmissionModule emission = psystem.emission;
            emission.rateOverTime = Width * Height * 2;

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
