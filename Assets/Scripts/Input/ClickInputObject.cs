using Assets.Scripts.Input.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Input
{
    class ClickInputObject : MonoBehaviour
    {
        #region "Fields"

        private IClickableObject sourceObject;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public IClickableObject SourceObject
        {
            get { return sourceObject; }
            set { sourceObject = value; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"

        public void OnMouseHover(Vector3 mousePosition)
        {
            if (typeof(IMouseHover).IsAssignableFrom(sourceObject.GetType()))
                (sourceObject as IMouseHover).OnMouseHover(mousePosition);
        }

        public void OnMouseClick(int button, Vector3 hitPoint)
        {
            if (typeof(IMouseClick).IsAssignableFrom(sourceObject.GetType()))
                (sourceObject as IMouseClick).OnMouseClick(button, hitPoint);
        }

        public void OnMouseHoverEnter()
        {
            if (typeof(IMouseHoverEnter).IsAssignableFrom(sourceObject.GetType()))
                (sourceObject as IMouseHoverEnter).OnMouseHoverEnter();
        }

        public void OnMouseHoverLeave()
        {
            if (typeof(IMouseHoverLeave).IsAssignableFrom(sourceObject.GetType()))
                (sourceObject as IMouseHoverLeave).OnMouseHoverLeave();
        }

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
