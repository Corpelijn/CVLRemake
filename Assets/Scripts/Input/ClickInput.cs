using Assets.Scripts.Environment.Tiles;
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
        private GameObject go;

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
            TrackMousePosition();

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

                    ClickInputObject clickObj = hit.transform.GetComponentInParent<ClickInputObject>();
                    if (clickObj != null)
                        clickObj.OnMouseHoverEnter();
                    lastHoverObject = hit.transform;
                }
                else
                {
                    ClickInputObject cio1 = hit.transform.gameObject.GetComponentInParent<ClickInputObject>();
                    if (cio1 != null)
                        cio1.OnMouseHover(hit.point);
                }
            }
        }

        private void TrackMousePosition()
        {
            //if (go == null)
            //{
            //    go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            //    go.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            //}

            //go.transform.position = UnityEngine.Input.mousePosition;
        }

        private void ReadMouseClick()
        {
            if (!levelHasClickableItems || ScreenMovement.INSTANCE.IsMoving)
                return;

            for (int i = 0; i < 3; i++)
            {
                if (UnityEngine.Input.GetMouseButtonUp(i))
                {
                    // Raycast the item under the mouse
                    RaycastHit hit = new RaycastHit();
                    Ray vRay = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                    CurrentSelection.INSTANCE.SelectTile(null);
                    if (Physics.Raycast(vRay, out hit))
                    {
                        // Get the Tile object
                        Tile tile = GetTile(hit.transform);
                        if (tile == null)
                        {
                            continue;
                        }
                        if (!tile.Fog)
                        {
                            ClickInputObject clickObj = hit.transform.gameObject.GetComponentInChildren<ClickInputObject>();
                            if (clickObj != null)
                                clickObj.OnMouseClick(i, hit.point);
                        }
                        else
                        {
                            CurrentSelection.INSTANCE.SelectTile(tile);
                        }
                    }
                }
            }
        }

        private Tile GetTile(Transform transform)
        {
            Transform current = transform;
            TileIdentifier identifier = null;
            while ((identifier = current.GetComponent<TileIdentifier>()) == null)
            {
                current = current.parent;
                if (current == null)
                    break;
            }

            // Get the tile class
            if (identifier != null)
                return identifier.Tile;

            return null;
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
