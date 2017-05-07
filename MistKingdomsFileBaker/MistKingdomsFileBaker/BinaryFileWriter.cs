using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistKingdomsFileBaker
{
    class BinaryFileWriter
    {
        #region "Fields"

        private FileStream stream;
        private string currentBits;

        #endregion

        #region "Constructors"

        public BinaryFileWriter(string filename)
        {
            stream = new FileStream(filename, FileMode.Create);
        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"

        private void FlushBytes()
        {
            string currentByte = "";
            while (currentBits.Length >= 8)
            {
                currentByte = currentBits.Substring(0, 8);
                currentBits = currentBits.Remove(0, 8);
                stream.WriteByte(Convert.ToByte(currentByte, 2));
            }
        }

        public void WriteBits(string bits)
        {
            currentBits += bits;

            FlushBytes();
        }

        public void WriteString(string data)
        {
            foreach (char c in data)
            {
                currentBits += GetByte(Convert.ToString(c, 2));
            }

            FlushBytes();
        }

        public void WriteFunctionString(string data)
        {
            string newString = "";
            foreach (char c in data)
            {
                if (c < 58)
                    newString += GetByte(Convert.ToString(c - 48, 2)).Substring(2, 6);
                else if (c <= 'Z' && c >= 'A')
                    newString += GetByte(Convert.ToString(c - 'A' + 10, 2)).Substring(2, 6);
                else if (c <= 'z' && c >= 'a')
                    newString += GetByte(Convert.ToString(c - 'a' + 10 + 26, 2)).Substring(2, 6);
                else if (c == '_')
                    newString += GetByte(Convert.ToString(63, 2)).Substring(2, 6);
            }

            WriteBits(newString);
        }

        private string GetByte(string bits)
        {
            while (bits.Length < 8)
            {
                bits = "0" + bits;
            }
            return bits;
        }

        public void Close()
        {
            FlushBytes();

            // Check if currentBits is empty
            while (currentBits.Length > 0 && currentBits.Length < 8)
            {
                currentBits += "0";
            }

            FlushBytes();

            // Close the stream

            stream.Close();
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
