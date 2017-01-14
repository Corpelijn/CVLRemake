using Assets.Scripts.Game;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Input;
using Assets.Scripts.Input.Interfaces;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Ground
{
    class Water : GridObject, IMouseClick
    {
        #region "Fields"



        #endregion

        #region "Constructors"

        public Water(Grid grid, int x, int y) : base(grid, x, y, 1, 1)
        {

        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void SetRotation(Transform go)
        {
            Dictionary<Direction, GridObject> objs = this.parent.GetSurrounding(this);

            GridObject[] surroundingWater = objs.Where(x => x.Value.GetType() == typeof(Water)).Select(s => s.Value).ToArray();

            // TODO:
            go.transform.eulerAngles = new Vector3(0, 90, 0);
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void DrawObjects(float x, float y, Transform parent)
        {
            GameObject go = ObjectPool.GetNewObject("water");
            go.transform.position = new Vector3(x + X, 0, y + Y);
            go.transform.SetParent(parent);
            go.GetComponentInChildren<ClickInputObject>().SourceObject = this;

            SetRotation(go.transform);

            gameObjects.Add(go);

            IsDrawn = true;

            // Set the click parameter active
            ClickInput.INSTANCE.LevelHasClickableItems++;
        }

        protected override void UpdatedWithNeighbourObjects(Dictionary<Direction, GridObject> neighbours)
        {
            // Find any neighbour water tiles
            Dictionary<Direction, GridObject> water = neighbours.Where(w => w.Value.GetType() == typeof(Water)).ToDictionary(a => a.Key, b => b.Value);

            // Create a very small grid for the water and dirt/grass tiles
            Grid grid = new Grid("currentTile");
        }

        public override void Destroy()
        {
            base.Destroy();

            // Set the click parameter inactive
            ClickInput.INSTANCE.LevelHasClickableItems--;
        }

        public void OnMouseClick(int button, Vector3 hitPoint)
        {
            // Spawn 3 water resources at the place where the player clicked
            float radiusWidth = 0.5f;

            for (int i = 0; i < 3; i++)
            {
                ResourceDrawer.INSTANCE.AddResource(ResourceValues.Water, hitPoint + new Vector3(PRNG.GetFloatNumber(-radiusWidth, radiusWidth), 0, PRNG.GetFloatNumber(-radiusWidth, radiusWidth)));
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
