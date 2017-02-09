using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Content.FileParsers
{
    abstract class FileParser
    {
        #region "Fields"



        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"

        public abstract object ParseFileText(string filename);

        public abstract object ParseFileBinary(BinaryStreamReader reader);

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
