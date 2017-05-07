using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Input
{
    class ScreenMovement : MonoBaseClass<ScreenMovement>
    {
        #region "Fields"

        private Vector2 lastMousePosition;
        private bool initialPositionSet;

        public float ZoomSpeed = 10f;
        public float MovementSpeed = 1f;

        public bool isMoving;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public bool IsMoving
        {
            get { return isMoving; }
        }

        #endregion

        #region "Methods"

        private void GetMouseInput()
        {
            // Check if the left mouse button is pressed
            if (!UnityEngine.Input.GetMouseButton(0) || !initialPositionSet)
                return;

            // Get the current mouse position
            Vector2 currentMousePosition = UnityEngine.Input.mousePosition;

            Vector2 delta = currentMousePosition - lastMousePosition;
            if (Mathf.Abs(delta.x) > 3 || Mathf.Abs(delta.y) > 3)
                isMoving = true;
            Transform camTransform = Camera.main.transform;

            Vector3 forward = new Vector3(camTransform.forward.x, 0, camTransform.forward.z);
            camTransform.position += (forward * -delta.y * MovementSpeed * Time.deltaTime) + (camTransform.right * -delta.x * MovementSpeed * Time.deltaTime);

            lastMousePosition = currentMousePosition;
        }

        private void CheckInitialPosition()
        {
            if (!initialPositionSet && UnityEngine.Input.GetMouseButtonDown(0))
            {
                lastMousePosition = UnityEngine.Input.mousePosition;
                initialPositionSet = true;
            }
            else if (initialPositionSet && UnityEngine.Input.GetMouseButtonUp(0))
            {
                initialPositionSet = false;
            }
            else if (isMoving && !initialPositionSet)
            {
                isMoving = false;
            }
        }

        private void ZoomInputHandling()
        {
            float zoomDelta = 0f;//UnityEngine.Input.mouseScrollDelta.y;
            if (zoomDelta != 0f)
            {
                Transform camTransform = Camera.main.transform;
                camTransform.position += camTransform.forward * zoomDelta * ZoomSpeed * Time.deltaTime;
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Update()
        {
            CheckInitialPosition();

            GetMouseInput();

            ZoomInputHandling();
        }

        public override void Start()
        {

        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
