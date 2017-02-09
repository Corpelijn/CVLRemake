using Assets.Scripts.Environment.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Content
{
    abstract class ContentLoader
    {
        #region "Fields"

        protected Tile zeroTile;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Tile ZeroTile
        {
            get { return zeroTile; }
        }

        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"

        public abstract void ParseContent();

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
