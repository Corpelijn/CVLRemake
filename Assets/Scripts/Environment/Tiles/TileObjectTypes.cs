using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Environment.Tiles
{
    class TileObjectTypes
    {
        #region "Fields"

        public static readonly TileObjectTypes Tree = new TileObjectTypes("tree");
        public static readonly TileObjectTypes Stonepath = new TileObjectTypes("stonepath");
        public static readonly TileObjectTypes Dirtpath = new TileObjectTypes("dirtpath");
        public static readonly TileObjectTypes Playerstart = new TileObjectTypes("playerstart");
        public static readonly TileObjectTypes Castle = new TileObjectTypes("castle");
        public static readonly TileObjectTypes Vault = new TileObjectTypes("vault");
        public static readonly TileObjectTypes Bridge = new TileObjectTypes("bridge");
        public static readonly TileObjectTypes Bridge4h = new TileObjectTypes("bridge4h");

        private string value;

        #endregion

        #region "Constructors"

        private TileObjectTypes(string value)
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

        public static implicit operator TileObjectTypes(string name)
        {
            return new TileObjectTypes(name);
        }

        public static implicit operator TileObjectTypes(int value)
        {
            switch (value)
            {
                case 0:
                    return Tree;
                case 1:
                    return Stonepath;
                case 2:
                    return Dirtpath;
                case 3:
                    return Playerstart;
                case 4:
                    return Castle;
                case 5:
                    return Vault;
                case 6:
                    return Bridge;
                case 7:
                    return Bridge4h;
            }

            return null;
        }

        public static bool operator ==(TileObjectTypes right, TileObjectTypes left)
        {
            return right.value == left.value;
        }

        public static bool operator !=(TileObjectTypes right, TileObjectTypes left)
        {
            return right.value != left.value;
        }

        #endregion  
    }
}
