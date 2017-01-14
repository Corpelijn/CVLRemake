using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Input
{
    class ClickInput : MonoBaseClass<ClickInput>
    {
        #region "Fields"

        private CountingBoolean levelHasHoverableItems;
        private CountingBoolean levelHasClickableItems;

        public bool hover;
        public bool click;

        private Transform lastHoverObject;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public CountingBoolean LevelHasHoverableItems
        {
            get { return levelHasHoverableItems; }
            set { levelHasHoverableItems = value; }
        }

        public CountingBoolean LevelHasClickableItems
        {
            get { return levelHasClickableItems; }
            set { levelHasClickableItems = value; }
        }

        #endregion

        #region "Methods"

        private void ReadMouseHover()
        {
            // Check if there is any item in the current level that can be hovered over
            if (!levelHasHoverableItems)
                return;

            for (int i = 0; i < 3; i++)
            {
                if (UnityEngine.Input.GetMouseButton(i))
                    return;
            }

            // Raycast the item under the mouse
            RaycastHit hit = new RaycastHit();
            Ray vRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(vRay, out hit))
            {
                if (hit.transform != lastHoverObject)
                {
                    if (lastHoverObject != null)
                    {
                        ClickInputObject cio = lastHoverObject.GetComponentInParent<ClickInputObject>();
                        if (cio != null)
                            cio.OnMouseHoverLeave();
                    }

                    hit.transform.GetComponentInParent<ClickInputObject>().OnMouseHoverEnter();
                    lastHoverObject = hit.transform;
                }
                else
                    hit.transform.gameObject.GetComponentInParent<ClickInputObject>().OnMouseHover(hit.point);
            }
        }

        private void ReadMouseClick()
        {
            if (!levelHasClickableItems)
                return;

            for (int i = 0; i < 3; i++)
            {
                if (UnityEngine.Input.GetMouseButtonUp(i))
                {
                    // Raycast the item under the mouse
                    RaycastHit hit = new RaycastHit();
                    Ray vRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                    if (Physics.Raycast(vRay, out hit))
                    {
                        hit.transform.gameObject.GetComponentInChildren<ClickInputObject>().OnMouseClick(i, hit.point);
                    }
                }
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        //public override void Start()
        public override void Awake()
        {
            base.Awake();

            levelHasHoverableItems = new CountingBoolean();
            levelHasClickableItems = new CountingBoolean();
        }

        public override void Update()
        {
            // Read the current mouse position
            ReadMouseHover();

            // Check if one of the mouse buttons is clicked
            ReadMouseClick();

            hover = levelHasHoverableItems;
            click = levelHasClickableItems;
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
