using Assets.Scripts.Game;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Other;
using Assets.Scripts.Input;
using Assets.Scripts.Input.Interfaces;
using Assets.Scripts.Other;
using CoBa;
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



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void DrawObjects(float x, float y, Transform parent)
        {
            DrawAccordingToNeighbours(x, y, parent);

            IsDrawn = true;

            // Set the click parameter active
            ClickInput.INSTANCE.LevelHasClickableItems++;
        }

        private void DrawAccordingToNeighbours(float x, float y, Transform parent)
        {
            // Create an object to store the watertile in
            GameObject waterParent = new GameObject("WaterTile");
            waterParent.transform.SetParent(parent);
            gameObjects.Add(waterParent);

            // Find any neighbour water tiles
            Dictionary<Direction, GridObject> grassBlocks = this.parent.GetSurrounding(this).Where(w => w.Value.GetType() != typeof(Water)).ToDictionary(a => a.Key, b => b.Value);

            // Draw a water block, there is always a bit of water
            GameObject water = ObjectPool.Instantiate("water");
            water.transform.position = new Vector3(0, 0, 0);
            water.GetComponentInChildren<ClickInputObject>().SourceObject = this;
            water.transform.SetParent(waterParent.transform);

            if (grassBlocks.Count >= 1)
            {
                // Create a very small grid for the water and dirt/grass tiles
                Grid grid = new Grid("Surrounding", 1f / 4f, new Vector2(4, 4));

                // Set the positions for the shore tiles
                Dictionary<Direction, Vector2[]> areas = new Dictionary<Direction, Vector2[]>
                {
                    { Direction.North,      new Vector2[] { new Vector2(1, 3), new Vector2(2, 3)    } },
                    { Direction.NorthEast,  new Vector2[] { new Vector2(3, 3)                       } },
                    { Direction.East,       new Vector2[] { new Vector2(3, 1), new Vector2(3, 2)    } },
                    { Direction.SouthEast,  new Vector2[] { new Vector2(3, 0)                       } },
                    { Direction.South,      new Vector2[] { new Vector2(1, 0), new Vector2(2, 0)    } },
                    { Direction.SouthWest,  new Vector2[] { new Vector2(0, 0)                       } },
                    { Direction.West,       new Vector2[] { new Vector2(0, 1), new Vector2(0, 2)    } },
                    { Direction.NorthWest,  new Vector2[] { new Vector2(0, 3)                       } }
                };

                // Draw the shore tiles on the grid
                foreach (KeyValuePair<Direction, GridObject> surrounding in grassBlocks)
                {
                    Vector2[] positions;
                    areas.TryGetValue(surrounding.Key, out positions);
                    if (positions != null)
                        foreach (Vector2 position in positions)
                        {
                            grid.AddObject(new Grass(grid, (int)position.x, (int)position.y));
                        }
                }

                // Draw the shore tiles and set the parent
                GameObject surroundingTiles = grid.DrawScaled(waterParent.transform);
                surroundingTiles.transform.SetParent(waterParent.transform);
            }

            // Update the position of the parent object
            waterParent.transform.position = new Vector3(x + X + Width / 2f, 0f, y + Y + Height / 2f);
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
