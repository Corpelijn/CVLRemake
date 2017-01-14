using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Environment
{
    class GroundType
    {
        #region "Fields"

        public static readonly GroundType Grass = new GroundType("grass");

        private string value;

        #endregion

        #region "Constructors"

        private GroundType(string value)
        {
            this.value = value; 
        }

        #endregion

        #region "Properties"



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

        public static implicit operator string(GroundType type)
        {
            return type.value;
        }

        #endregion
    }
}
