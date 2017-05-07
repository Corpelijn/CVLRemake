using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistKingdomsFileBaker
{
    class MKFile
    {
        #region "Fields"

        private bool headerWritten;
        private BinaryFileWriter file;

        #endregion

        #region "Constructors"

        public MKFile(string filename)
        {
            headerWritten = false;
            file = new BinaryFileWriter(filename);
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        public void WriteObjectToFile(string filename)
        {
            // Check if the file already has a header
            if (!headerWritten)
            {
                WriteFileHeader();
                headerWritten = true;
            }

            // Read the data from the file
            ParsableFile pfile = ParsableFile.GetFileIdentifier(filename);

            if (pfile != null)
                // Write the data in binary to the MK file
                pfile.WriteData(ref file);
        }

        private void WriteFileHeader()
        {
            // Write the file identifier
            file.WriteString("MK");
            // Write the file version
            file.WriteBits("0000");
        }

        public void Close()
        {
            file.Close();
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
