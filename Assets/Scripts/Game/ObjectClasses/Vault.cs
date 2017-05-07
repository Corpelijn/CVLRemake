using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.ObjectClasses
{
    class Vault
    {
        #region "Fields"

        private int maxSize;
        private List<string> resourcesInVault;

        #endregion

        #region "Constructors"

        private Vault()
        {
            resourcesInVault = new List<string>();
            resourcesInVault.Add("axe");
        }

        #endregion

        #region "Singleton"

        private static Vault instance;

        public static Vault INSTANCE
        {
            get
            {
                if (instance == null)
                    instance = new Vault();
                return instance;
            }
        }

        #endregion  

        #region "Properties"



        #endregion

        #region "Methods"

        public bool HasItem(string objectName)
        {
            return resourcesInVault.Contains(objectName);
        }

        public bool UseItem(string objectName)
        {
            bool canUse = resourcesInVault.Contains(objectName);
            if (canUse)
            {
                resourcesInVault.Remove(objectName);
            }
            return canUse;
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
