using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistKingdomsFileBaker
{
    abstract class ParsableFile
    {
        #region "Fields"

        protected string filename;

        #endregion

        #region "Constructors"

        protected ParsableFile(string filename)
        {
            this.filename = filename;
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        protected string RoundBitsToBytes(string input, int byteCount)
        {
            while (input.Length < byteCount * 8)
            {
                input = "0" + input;
            }

            return input;
        }

        protected string RoundBits(string input, int count)
        {
            while (input.Length < count)
            {
                input = "0" + input;
            }
            return input;
        }

        #endregion

        #region "Abstract/Virtual Methods"

        public abstract void WriteData(ref BinaryFileWriter writer);

        protected abstract void ParseFile();

        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"

        public static ParsableFile GetFileIdentifier(string filename)
        {
            ParsableFile file = null;

            if (filename.EndsWith(".tile"))
            {
                file = new TileFile(filename);
            }
            else if (filename.EndsWith(".grid"))
            {
                file = new GridFile(filename);
            }

            if (file != null)
                file.ParseFile();

            return file;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
