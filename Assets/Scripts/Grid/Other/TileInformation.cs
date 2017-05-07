using Assets.Scripts.Environment.Tiles;
using Assets.Scripts.Grid.GridObjects;
using CoBa;
using UnityEngine;

namespace Assets.Scripts.Grid.Other
{
    class TileInformation : GridObject
    {
        #region "Fields"

        private TileIdentifier identifier;
        private Tile tile;
        private Transform parentTransform;
        private float currentX;
        private float currentY;

        private GameObject[] selectedObjects;

        #endregion

        #region "Constructors"

        public TileInformation(Tile tile, Grid grid, int width, int height) : base(grid, 0, 0, width, height)
        {
            this.tile = tile;
        }

        #endregion

        #region "Properties"

        public bool Fog
        {
            get { return tile.Fog; }
        }

        public bool IsAvailable
        {
            get { return tile.Available; }
        }

        public bool IsSelected
        {
            get { return tile.IsSelected; }
        }

        #endregion

        #region "Methods"

        public void UpdateParticleSystems()
        {
            if (Fog && !tile.DrawingInfo["fog"])
            {
                GameObject mist = ObjectPool.Instantiate("fog");
                mist.transform.position = new Vector3(currentX + Width / 2f, 1, currentY + Height / 2f);
                ParticleSystem psystem = mist.GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule shape = psystem.shape;
                //psystem.startColor = new Color(0.1451f, 0.0196f, 0.1922f, 0.5961f);
                shape.box = new Vector3(Width, Height, 5);
                ParticleSystem.EmissionModule emission = psystem.emission;
                emission.rateOverTime = Width * Height * 2;

                mist.transform.SetParent(parentTransform);

                tile.DrawingInfo["fog"] = true;

                tile.ObjectGrid.IgnoreRaycast(true);
            }
            else if (!Fog && tile.DrawingInfo["fog"])
            {
                tile.ObjectGrid.IgnoreRaycast(false);
            }

            if (IsSelected && !tile.DrawingInfo["selected"])
            {
                selectedObjects = new GameObject[5];
                selectedObjects[0] = ObjectPool.Instantiate("edge");
                selectedObjects[0].transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f - Height / 2f);
                ParticleSystem psystem = selectedObjects[0].GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule shape = psystem.shape;
                shape.radius = tile.Width / 2f;

                selectedObjects[0].transform.SetParent(parentTransform);

                selectedObjects[1] = ObjectPool.Instantiate("edge");
                selectedObjects[1].transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f + Height / 2f);
                psystem = selectedObjects[1].GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Width / 2f;

                selectedObjects[1].transform.SetParent(parentTransform);

                selectedObjects[2] = ObjectPool.Instantiate("edge");
                selectedObjects[2].transform.position = new Vector3(currentX + Width / 2f + Width / 2f, 0, currentY + Height / 2f);
                selectedObjects[2].transform.eulerAngles = new Vector3(0, 90, 0);
                psystem = selectedObjects[2].GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Height / 2f;

                selectedObjects[2].transform.SetParent(parentTransform);

                selectedObjects[3] = ObjectPool.Instantiate("edge");
                selectedObjects[3].transform.position = new Vector3(currentX + Width / 2f - Width / 2f, 0, currentY + Height / 2f);
                selectedObjects[3].transform.eulerAngles = new Vector3(0, 90, 0);
                psystem = selectedObjects[3].GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Height / 2f;

                selectedObjects[3].transform.SetParent(parentTransform);

                selectedObjects[4] = ObjectPool.Instantiate("selectionFrame");
                selectedObjects[4].transform.localScale = new Vector3(Width, 1, Height);
                selectedObjects[4].transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f);
                selectedObjects[4].transform.SetParent(parentTransform);

                tile.DrawingInfo["selected"] = true;
            }
            else if (!IsSelected && tile.DrawingInfo["selected"])
            {
                foreach (GameObject obj in selectedObjects)
                {
                    ObjectPool.Destroy(obj);
                }

                tile.DrawingInfo["selected"] = false;

                selectedObjects = null;
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void DrawObjects(float x, float y, Transform parent)
        {
            parentTransform = parent;
            currentX = x;
            currentY = y;

            UpdateParticleSystems();

            identifier = parent.gameObject.AddComponent<TileIdentifier>();
            identifier.Tile = tile;
            identifier.Information = this;

            IsDrawn = true;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
