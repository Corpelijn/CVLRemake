using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    abstract class MonoBaseClass<T> : MonoBehaviour where T : MonoBaseClass<T>
    {
        #region "Fields"



        #endregion

        #region "Constructors"



        #endregion

        #region "Singleton"

        private static T instance;

        public static T INSTANCE
        {
            get
            {
                return instance;
            }
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"

        public virtual void Update()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Awake()
        {
            instance = (T)Convert.ChangeType(this, typeof(T));
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
