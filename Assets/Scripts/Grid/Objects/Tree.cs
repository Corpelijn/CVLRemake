using Assets.Scripts.Game;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Input;
using Assets.Scripts.Input.Interfaces;
using Assets.Scripts.Other;
using CoBa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Grid.Objects
{
    class Tree : GridObject, IMouseClick
    {
        #region "Fields"

        private bool treeIsCut;
        private string treeName;
        private GameObject treeGameObject;
        private bool treeIsFallingOver;

        private float interpolation;
        private Vector3 originalPosition;

        #endregion

        #region "Constructors"

        public Tree(Grid grid, int x, int y) : base(grid, x, y, 1, 1)
        {
            treeIsCut = false;
            treeName = "tree" + PRNG.GetNumber(0, 10);
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void DestroyTree()
        {
            if (treeGameObject != null)
            {
                ObjectPool.Destroy(treeGameObject);

                ResourceDrawer.INSTANCE.AddResource(ResourceValues.Wood, originalPosition);

                treeGameObject = null;

                // Set the click parameter inactive
                ClickInput.INSTANCE.LevelHasClickableItems--;
            }
        }

        private void TreeFallover()
        {
            if (treeIsFallingOver && interpolation < 1f)
            {
                treeGameObject.transform.position = Vector3.Lerp(originalPosition, originalPosition + new Vector3(0, 0.25f, 0), interpolation);
                treeGameObject.transform.eulerAngles = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(90, 0, 0), interpolation);
                interpolation += 2f * Time.deltaTime;
            }
            else if (treeIsFallingOver && interpolation != 2f)
            {
                DestroyTree();
                interpolation = 2f;
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void DrawObjects(float x, float y, Transform parent)
        {
            //GameObject floor = ObjectPool.Instantiate("grass");
            //floor.transform.position = new Vector3(x + X, 0, y + Y);
            //floor.transform.SetParent(parent);
            //gameObjects.Add(floor);

            if (!treeIsCut)
            {
                treeGameObject = ObjectPool.Instantiate(treeName);
                treeGameObject.transform.position = new Vector3(x + X + Width / 2f, 0, y + Y + Height / 2f);
                treeGameObject.transform.SetParent(parent);
                treeGameObject.transform.eulerAngles = new Vector3();
                treeGameObject.GetComponentInChildren<ClickInputObject>().SourceObject = this;
                originalPosition = treeGameObject.transform.position;
                gameObjects.Add(treeGameObject);
            }

            IsDrawn = true;

            // Set the click parameter active
            ClickInput.INSTANCE.LevelHasClickableItems++;
        }

        public override void Destroy()
        {
            base.Destroy();

            if (treeGameObject != null)
            {
                // Set the click parameter inactive
                ClickInput.INSTANCE.LevelHasClickableItems--;
            }
        }

        public void OnMouseClick(int button, Vector3 hitPoint)
        {
            if (button == 0 && !treeIsFallingOver)
            {
                treeIsCut = true;
                treeIsFallingOver = true;
                interpolation = 0;
            }
        }

        public override void Update()
        {
            TreeFallover();
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
