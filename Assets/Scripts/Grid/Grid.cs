using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid
{
    class Grid
    {
        #region "Fields"

        private int width;
        private int height;
        private int startX;
        private int startY;
        private Vector2? fixedSize;
        private float scale;
        private bool normalized;
        private List<GridObject> gridObjects;
        private string name;
        private Transform parent;

        #endregion

        #region "Constructors"

        public Grid(string name)
        {
            this.name = name;
            scale = 1f;
            normalized = false;
            gridObjects = new List<GridObject>();
        }

        public Grid(string name, Vector2 fixedSize)
        {
            this.name = name;
            this.fixedSize = fixedSize;
            scale = 1f;
            normalized = false;
            gridObjects = new List<GridObject>();
        }

        public Grid(string name, float scale, Vector2 fixedSize)
        {
            this.name = name;
            this.scale = scale;
            this.fixedSize = fixedSize;
            normalized = false;
            gridObjects = new List<GridObject>();
        }

        #endregion

        #region "Properties"

        public int Width
        {
            get
            {
                if (!normalized)
                    Normalize();
                return width;
            }
        }

        public int Height
        {
            get
            {
                if (!normalized)
                    Normalize();
                return height;
            }
        }

        public int LowX
        {
            get
            {
                if (!normalized)
                    Normalize();
                return startX;
            }
        }

        public int LowY
        {
            get
            {
                if (!normalized)
                    Normalize();
                return startY;
            }
        }

        public Transform Parent
        {
            get { return parent; }
        }

        #endregion

        #region "Methods"

        public void AddObject(GridObject obj)
        {
            gridObjects.Add(obj);
        }

        private void Normalize()
        {
            // Check if we have a fixed size
            if (fixedSize != null)
            {
                // Set the fixed size
                width = (int)fixedSize.Value.x;
                height = (int)fixedSize.Value.y;
                startX = 0;
                startY = 0;
            }
            else
            {
                // Calculate the size and start position
                int minx = gridObjects.Min(x => x.X);
                int maxx = gridObjects.Max(x => x.X + x.Width);
                int miny = gridObjects.Min(y => y.Y);
                int maxy = gridObjects.Max(y => y.Y + y.Height);

                width = maxx - minx;
                height = maxy - miny;
                startX = minx;
                startY = miny;
            }

            normalized = true;
        }

        public void Draw(float x, float y, Transform parent)
        {
            if (!normalized)
                Normalize();

            if (this.parent == null)
            {
                this.parent = new GameObject(name).transform;
                this.parent.SetParent(parent);
            }

            foreach (GridObject gridObject in gridObjects)
            {
                gridObject.Draw(-startX + -width / 2f + x, startY + -height / 2f + y, this.parent);
                gridObject.Update();
            }
        }

        public GameObject DrawScaled(Transform parent)
        {
            Draw(0, 0, parent);

            this.parent.localScale = new Vector3(scale, 1f, scale);
            return parent.gameObject;
        }

        public Dictionary<Direction, GridObject> GetSurrounding(GridObject obj)
        {
            Dictionary<Direction, GridObject> objects = new Dictionary<Direction, GridObject>();
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    GridObject found = gridObjects.FirstOrDefault(x => x.X == obj.X + i && x.Y == obj.Y + j);
                    if (found != null && !(i == 0 && j == 0))
                    {
                        Direction dir = new Direction(i, j);
                        objects.Add(dir, found);
                    }
                }
            }
            return objects;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
