using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Other
{
    class BinaryStreamReader
    {
        #region "Fields"

        private FileStream stream;
        private string currentByte;

        #endregion

        #region "Constructors"

        public BinaryStreamReader(string filename)
        {
            stream = new FileStream(filename, FileMode.Open);
            ParseNextByte();
        }

        #endregion

        #region "Properties"

        public bool EndOfStream
        {
            get
            {
                return stream.Position == stream.Length && currentByte.Length == 0;
            }
        }

        public long BitsLeft
        {
            get { return (stream.Length - stream.Position) * 8 + currentByte.Length; }
        }

        #endregion

        #region "Methods"

        public string ReadBit()
        {
            if (currentByte.Length == 0)
                ParseNextByte();

            string bit = currentByte.Substring(0, 1);
            currentByte = currentByte.Remove(0, 1);
            return bit;
        }

        public string ReadBits(int amount)
        {
            string bits = "";
            for (int i = 0; i < amount; i++)
            {
                bits += ReadBit();
            }

            return bits;
        }

        public string ReadCharacters(int amount)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < amount; i++)
            {
                sb.Append((char)Convert.ToByte(ReadBits(8), 2));
            }

            return sb.ToString();
        }

        public string ReadFunctionCharacter()
        {
            byte character = Convert.ToByte(ReadBits(6), 2);

            if (character < 10)
                character += 48;
            else if (character < 36)
                character += 55;
            else if (character < 62)
                character += 61;
            else
                character = 95;

            return ((char)character).ToString();
        }

        private void ParseNextByte()
        {
            string b = Convert.ToString(stream.ReadByte(), 2);
            while (b.Length < 8)
            {
                b = "0" + b;
            }

            currentByte += b;
        }

        public void Close()
        {
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
