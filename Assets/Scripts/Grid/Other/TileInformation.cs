using Assets.Scripts.Environment.Tiles;
using Assets.Scripts.Grid.GridObjects;
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

        private bool fogDrawn;
        private bool selectedDrawn;

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
                mist.transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f);
                ParticleSystem psystem = mist.GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule shape = psystem.shape;
                //psystem.startColor = new Color(0.1451f, 0.0196f, 0.1922f, 0.5961f);
                shape.box = new Vector3(Width, Height, 6);

                mist.transform.SetParent(parentTransform);

                tile.DrawingInfo["fog"] = true;
            }

            if (IsSelected && !tile.DrawingInfo["selected"])
            {
                GameObject edge = ObjectPool.Instantiate("edge");
                edge.transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f - Height / 2f);
                ParticleSystem psystem = edge.GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule shape = psystem.shape;
                shape.radius = tile.Width / 2f;

                edge.transform.SetParent(parentTransform);

                edge = ObjectPool.Instantiate("edge");
                edge.transform.position = new Vector3(currentX + Width / 2f, 0, currentY + Height / 2f + Height / 2f);
                psystem = edge.GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Width / 2f;

                edge.transform.SetParent(parentTransform);

                edge = ObjectPool.Instantiate("edge");
                edge.transform.position = new Vector3(currentX + Width / 2f + Width / 2f, 0, currentY + Height / 2f);
                edge.transform.eulerAngles = new Vector3(0, 90, 0);
                psystem = edge.GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Height / 2f;

                edge.transform.SetParent(parentTransform);

                edge = ObjectPool.Instantiate("edge");
                edge.transform.position = new Vector3(currentX + Width / 2f - Width / 2f, 0, currentY + Height / 2f);
                edge.transform.eulerAngles = new Vector3(0, 90, 0);
                psystem = edge.GetComponent<ParticleSystem>();
                shape = psystem.shape;
                shape.radius = tile.Height / 2f;

                edge.transform.SetParent(parentTransform);

                tile.DrawingInfo["selected"] = true;
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
