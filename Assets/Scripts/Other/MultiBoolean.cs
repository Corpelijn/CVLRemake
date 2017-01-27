using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Other
{
    class MultiBoolean
    {
        #region "Fields"

        private Dictionary<string, bool> booleans;

        #endregion

        #region "Constructors"

        public MultiBoolean()
        {
            booleans = new Dictionary<string, bool>();
        }

        #endregion

        #region "Properties"

        public bool this[string name]
        {
            get
            {
                if (booleans.ContainsKey(name))
                    return booleans[name];
                else
                    return true;
            }
            set
            {
                if (!booleans.ContainsKey(name))
                {
                    booleans.Add(name, value);
                }
                else
                {
                    booleans[name] = value;
                }
            }
        }

        #endregion

        #region "Methods"



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
