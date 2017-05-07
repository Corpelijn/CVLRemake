using Assets.Scripts.Game.ObjectClasses;
using Assets.Scripts.Interfaces;
using CoBa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Game
{
    class ResourceRequirement : IDrawable
    {
        #region "Fields"

        private GameObject gameObject;
        private GameObject requirementObject;
        private Text canvasObject;
        private int value;
        private string objectName;

        #endregion

        #region "Constructors"

        public ResourceRequirement(string objectName, int value = 1)
        {
            this.value = value;
            this.objectName = objectName;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Draw(Transform parent, Vector3 position)
        {
            gameObject = ObjectPool.Instantiate(objectName);
            requirementObject = ObjectPool.Instantiate("resourceRequirement");

            canvasObject = requirementObject.GetComponentInChildren<Text>();
            canvasObject.color = Vault.INSTANCE.HasItem("axe") ? new Color(0.294f, 0.784f, 0.294f) : new Color(0.745f, 0.255f, 0.255f);
            canvasObject.text = value.ToString();
            canvasObject.transform.LookAt(Camera.main.transform);

            gameObject.transform.SetParent(requirementObject.transform);
            requirementObject.transform.SetParent(parent);
            requirementObject.transform.position = position;
        }

        public void Destroy()
        {
            ObjectPool.Destroy(gameObject);
            ObjectPool.Destroy(requirementObject);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
