using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Environment.Tiles
{
    class TileGroundTypes
    {
        #region "Fields"

        public static readonly TileGroundTypes Grass = new TileGroundTypes("grass");
        public static readonly TileGroundTypes Water = new TileGroundTypes("water");

        private string value;

        #endregion

        #region "Constructors"

        private TileGroundTypes(string value)
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

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"

        public static implicit operator TileGroundTypes(string name)
        {
            return new TileGroundTypes(name);
        }

        public static bool operator ==(TileGroundTypes right, TileGroundTypes left)
        {
            return right.value == left.value;
        }

        public static bool operator !=(TileGroundTypes right, TileGroundTypes left)
        {
            return right.value != left.value;
        }

        #endregion  
    }
}
