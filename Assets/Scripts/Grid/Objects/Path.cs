using Assets.Scripts.Grid.GridObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Objects
{
    abstract class Path : GridObject
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Path(Grid grid, int x, int y) : base(grid, x, y, 1, 1)
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

        /// <summary>
        /// Ajusts the StonePath layout to be either horizontal (default) or vertical
        /// </summary>
        /// <param name="neighbours">The neighbours of the current object</param>
        protected override void UpdatedWithNeighbourObjects(Dictionary<Direction, GridObject> neighbours)
        {
            // Get the gridobjects that are any type of path
            Dictionary<Direction, GridObject> paths = neighbours.Where(x => x.Value.GetType().IsSubclassOf(typeof(Path))).ToDictionary(a => a.Key, b => b.Value);

            int absoluteDirection = paths.GroupBy(x => Mathf.Abs(x.Key)).OrderByDescending(gp => gp.Count()).Select(s => s.Key).First();
            if (absoluteDirection == 1)
            {
                gameObjects.First().transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
