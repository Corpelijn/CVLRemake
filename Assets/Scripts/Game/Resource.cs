using Assets.Scripts.Input;
using Assets.Scripts.Input.Interfaces;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Game
{
    class Resource : IMouseHoverEnter, IMouseHover
    {
        #region "Fields"

        private ResourceValues value;
        private Vector3 position;
        private GameObject gameObject;
        private bool isDrawn;
        private int pickupCounter;

        private float aliveTime;

        #endregion

        #region "Constructors"

        public Resource(ResourceValues value, Vector3 position)
        {
            this.value = value;
            this.position = position;
            aliveTime = PRNG.GetFloatNumber(0f, 1f);
        }

        public Resource(ResourceValues value)
        {
            this.value = value;
        }

        #endregion

        #region "Properties"

        public bool IsDrawn
        {
            get { return isDrawn; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        #endregion

        #region "Methods"

        public void Draw()
        {
            gameObject = ObjectPool.Instantiate("resource" + (int)value);
            gameObject.transform.position = position;
            gameObject.transform.eulerAngles = new Vector3(0, PRNG.GetFloatNumber(0, 359), 0);
            gameObject.GetComponentInChildren<ClickInputObject>().SourceObject = this;
            gameObject.GetComponentInChildren<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;

            isDrawn = true;

            ClickInput.INSTANCE.LevelHasHoverableItems++;
        }

        public void Update()
        {
            gameObject.transform.position = position;

            aliveTime += Time.deltaTime;
            if (aliveTime >= 5f)
            {
                MoveToPlayerInventory();
            }
        }

        public void Destroy()
        {
            ObjectPool.Destroy(gameObject);

            ClickInput.INSTANCE.LevelHasHoverableItems--;
            isDrawn = false;
        }

        private void MoveToPlayerInventory()
        {
            ResourceDrawer.INSTANCE.MoveResourceToPlayer(this);
            gameObject.GetComponentInChildren<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void OnMouseHoverEnter()
        {
            if (pickupCounter >= 1)
            {
                MoveToPlayerInventory();
            }

            pickupCounter++;
        }

        public void OnMouseHover(Vector3 position)
        {
            if (pickupCounter >= 4)
            {
                MoveToPlayerInventory();
            }

            pickupCounter++;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
