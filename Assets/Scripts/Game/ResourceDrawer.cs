using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    class ResourceDrawer : MonoBaseClass<ResourceDrawer>
    {
        #region "Fields"

        private List<Resource> resources;
        private Dictionary<Resource, float> movingResources;

        public float ResourceMovementSpeed = 1f;
        public Transform ResourceMovementPoint = null;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void AddResource(ResourceValues value, Vector3 position)
        {
            resources.Add(new Resource(value, position));
        }

        private void DrawResources()
        {
            foreach (Resource resource in resources)
            {
                if (!resource.IsDrawn)
                    resource.Draw();
                else
                    resource.Update();
            }
        }

        public void MoveResourceToPlayer(Resource resource)
        {
            try
            {
                movingResources.Add(resource, 0f);
            }
            catch (Exception)
            {
            }
        }

        private void UpdateMovingResources()
        {
            Resource[] resources = movingResources.Keys.ToArray();
            foreach (Resource resource in resources)
            {
                float interpolation = movingResources[resource];
                resource.Position = Vector3.Lerp(resource.Position, ResourceMovementPoint.position, interpolation);
                interpolation += ResourceMovementSpeed * Time.deltaTime;
                movingResources[resource] = interpolation;

                if (interpolation > 1f)
                {
                    resource.Destroy();
                    movingResources.Remove(resource);
                    this.resources.Remove(resource);
                    GameManager.INSTANCE.AddResourceToVault(resource);
                }
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Start()
        {
            resources = new List<Resource>();
            movingResources = new Dictionary<Resource, float>();
        }

        public override void Update()
        {
            DrawResources();

            UpdateMovingResources();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
