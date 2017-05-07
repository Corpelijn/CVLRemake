using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    class Progressbar : MonoBehaviour
    {
        #region "Fields"

        public Sprite progressbarBack = null;
        public Sprite progressbarBegin = null;
        public Sprite progressbarMiddle = null;
        public Sprite progressbarEnd = null;

        private float value;
        private RectTransform rect_progress;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public float Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public void Start()
        {
            RectTransform rect = gameObject.GetComponent<RectTransform>();
            rect.sizeDelta = new Vector2(0, 40);
            gameObject.AddComponent<CanvasRenderer>();

            HorizontalLayoutGroup group = gameObject.AddComponent<HorizontalLayoutGroup>();
            group.childAlignment = TextAnchor.MiddleCenter;
            group.childControlHeight = false;

            CreateSpacing(transform);

            GameObject bar = new GameObject("Bar");
            Image bar_back = bar.AddComponent<Image>();
            bar.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 40);
            bar_back.sprite = progressbarBack;
            bar_back.preserveAspect = true;
            bar_back.transform.SetParent(transform);
            bar_back.transform.localScale = Vector3.one;

            GameObject progress = new GameObject("Progress");
            Image progress_img = progress.AddComponent<Image>();
            rect_progress = progress.GetComponent<RectTransform>();

            rect_progress.anchorMin = new Vector2(0, 0.5f);
            rect_progress.anchorMax = new Vector2(0, 0.5f);
            rect_progress.sizeDelta = new Vector2(220, 40);
            rect_progress.pivot = new Vector2(0, 0.5f);

            progress_img.sprite = progressbarMiddle;
            progress_img.transform.SetParent(bar.transform);
            progress_img.transform.localScale = Vector3.one;

            rect_progress.localPosition = new Vector3(290f, 0, 0);

            CreateSpacing(transform);
        }

        private void CreateSpacing(Transform parent)
        {
            GameObject spacing = new GameObject("Spacing");
            spacing.transform.SetParent(parent);
            spacing.transform.localScale = Vector3.one;
            Image image = spacing.AddComponent<Image>();
            image.color = new Color(1, 1, 1, 0);
        }

        public void Update()
        {
            rect_progress.localScale = new Vector3(value, 1, 1);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
