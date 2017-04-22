using Assets.Scripts.Environment;
using CoBa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.GridObjects
{
    abstract class GridObject
    {
        #region "Fields"

        private int x;
        private int y;
        private int width;
        private int height;
        private bool isDrawn;

        protected List<GameObject> gameObjects;
        protected Grid parent;
        protected float rotation;

        #endregion

        #region "Constructors"

        protected GridObject(Grid grid, int x, int y, int width, int height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
            parent = grid;
            isDrawn = false;
            gameObjects = new List<GameObject>();
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

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        protected bool IsDrawn
        {
            get { return isDrawn; }
            set { isDrawn = value; }
        }

        public Bounds Bounds
        {
            get { return new Bounds(new Vector3(x, 0, y), new Vector3(width, 1, height)); }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"

        public virtual void Draw(float x, float y, Transform parent)
        {
            /*Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition += Camera.main.transform.forward * EnvironmentGenerator.INSTANCE.DrawingDistance * 1.5f;

            float distanceToCamera = Vector2.Distance(new Vector2(cameraPosition.x, cameraPosition.z), new Vector2(x + X, y + Y));
            if (IsDrawn && distanceToCamera > EnvironmentGenerator.INSTANCE.DrawingDistance)
            {
                Destroy();
            }
            else*/
            if (!IsDrawn)// && distanceToCamera < EnvironmentGenerator.INSTANCE.DrawingDistance)
            {
                DrawObjects(x, y, parent);

                MethodInfo method = GetType().GetMethod("UpdatedWithNeighbourObjects", BindingFlags.Instance | BindingFlags.NonPublic);
                if (method.GetBaseDefinition().DeclaringType != method.DeclaringType)
                {
                    UpdatedWithNeighbourObjects(this.parent.GetSurrounding(this));
                }
            }
        }

        protected abstract void DrawObjects(float x, float y, Transform parent);

        public virtual void Update()
        {

        }

        protected virtual void UpdatedWithNeighbourObjects(Dictionary<Direction, GridObject> neighbours)
        {

        }

        public virtual void Destroy()
        {
            foreach (GameObject go in gameObjects)
            {
                ObjectPool.Destroy(go);
            }

            gameObjects.Clear();

            IsDrawn = false;
        }

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
