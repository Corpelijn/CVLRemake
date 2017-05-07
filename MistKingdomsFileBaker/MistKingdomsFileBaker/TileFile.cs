using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistKingdomsFileBaker
{
    class TileFile : ParsableFile
    {
        #region "Fields"

        private int width;
        private int height;
        private int uid;
        private string name;
        private List<string> content;

        #endregion

        #region "Constructors"

        public TileFile(string filename) : base(filename)
        {

        }

        #endregion

        #region "Properties"



        #endregion

        #region "Methods"



        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        protected override void ParseFile()
        {
            content = new List<string>();

            // Get all the lines from the file
            string[] lines = System.IO.File.ReadAllText(filename).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.StartsWith("name "))
                {
                    name = line.Substring(5);
                }
                else if (line.StartsWith("id "))
                {
                    uid = Convert.ToInt32(line.Substring(3));
                }
                else if (line.StartsWith("size "))
                {
                    string[] values = line.Substring(5).Split('x');
                    width = Convert.ToInt32(values[0]);
                    height = Convert.ToInt32(values[1]);
                }
                else if (line.StartsWith("special"))
                {

                }
                else if (!line.StartsWith("#"))
                {
                    content.Add(line);
                }
            }
        }

        public override void WriteData(ref BinaryFileWriter writer)
        {
            // Write the data header
            WriteHeader(ref writer);

            // Write the data content
            foreach (string line in content)
            {
                string[] splitted = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                TileContent type = (TileContent)Enum.Parse(typeof(TileContent), splitted[0]);
                string bits = GetAdditionalData(type, line);

                writer.WriteBits(RoundBits(Convert.ToString((int)type, 2), 2));
                writer.WriteBits(bits);
            }

            // End the data content
            writer.WriteBits("11");
        }

        private string GetAdditionalData(TileContent type, string line)
        {
            if (type == TileContent.fill)
            {
                return GetFillData(line);
            }
            else if (type == TileContent.ground)
                return GetGroundData(line);
            else if (type == TileContent.obj)
                return GetObjData(line);

            throw new NotImplementedException(line);
        }

        private string GetFillData(string line)
        {
            string bits = "";
            line = line.Remove(0, 5);
            if (line.StartsWith("ground"))
                bits += "0";

            line = line.Replace("ground ", "");
            GroundTypes ground = (GroundTypes)Enum.Parse(typeof(GroundTypes), line);
            bits += RoundBits(Convert.ToString((int)ground, 2), 4);

            return bits;
        }

        private string GetGroundData(string line)
        {
            string bits = "";
            line = line.Remove(0, 7);

            GroundTypes ground = (GroundTypes)Enum.Parse(typeof(GroundTypes), line.Substring(0, line.IndexOf(' ')));
            bits += RoundBits(Convert.ToString((int)ground, 2), 4);

            line = line.Remove(0, line.IndexOf(' '));
            string[] splitted = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int x = Convert.ToInt32(splitted[0]);
            int y = Convert.ToInt32(splitted[1]);
            bits += RoundBits(Convert.ToString(x, 2), 4) + RoundBits(Convert.ToString(y, 2), 4);

            return bits;
        }

        private string GetObjData(string line)
        {
            string bits = "";
            line = line.Remove(0, 4);

            TileObjects obj = (TileObjects)Enum.Parse(typeof(TileObjects), line.Substring(0, line.IndexOf(' ')));
            bits += RoundBitsToBytes(Convert.ToString((int)obj, 2), 2);

            line = line.Remove(0, line.IndexOf(' '));
            string[] splitted = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int x = Convert.ToInt32(splitted[0]);
            int y = Convert.ToInt32(splitted[1]);
            bits += RoundBits(Convert.ToString(x, 2), 4) + RoundBits(Convert.ToString(y, 2), 4);

            return bits;
        }

        private void WriteHeader(ref BinaryFileWriter writer)
        {
            // Write the data type [000]
            writer.WriteBits("000");

            // Write the unique id for the tile
            writer.WriteBits(RoundBitsToBytes(Convert.ToString(uid, 2), 2));

            // Write the name of the tile
            writer.WriteBits(RoundBitsToBytes(Convert.ToString(name.Length, 2), 1));
            writer.WriteFunctionString(name);

            // Write the size of the tile
            string w = RoundBits(Convert.ToString(width, 2), 4);
            string h = RoundBits(Convert.ToString(height, 2), 4);
            writer.WriteBits(w + h);

            // Write the end of the header
            writer.WriteBits("1111");
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
