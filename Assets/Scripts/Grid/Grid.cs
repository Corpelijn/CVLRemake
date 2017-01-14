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
        private bool normalized;
        private List<GridObject> gridObjects;
        private string name;
        private Transform parent;

        #endregion

        #region "Constructors"

        public Grid(string name)
        {
            this.name = name;
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

        #endregion

        #region "Methods"

        public void AddObject(GridObject obj)
        {
            gridObjects.Add(obj);
        }

        private void Normalize()
        {
            int minx = gridObjects.Min(x => x.X);
            int maxx = gridObjects.Max(x => x.X + x.Width);
            int miny = gridObjects.Min(y => y.Y);
            int maxy = gridObjects.Max(y => y.Y + y.Height);

            width = maxx - minx;
            height = maxy - miny;
            startX = minx;
            startY = miny;

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
