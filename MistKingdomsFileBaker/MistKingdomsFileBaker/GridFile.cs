using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MistKingdomsFileBaker
{
    class GridFile : ParsableFile
    {
        #region "Fields"

        private int zeroTile;
        private List<Tuple<int, Direction, int>> tiles;

        #endregion

        #region "Constructors"

        public GridFile(string filename) : base(filename)
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
            tiles = new List<Tuple<int, Direction, int>>();
            zeroTile = -1;

            // Get all the lines from the file
            string[] lines = System.IO.File.ReadAllText(filename).Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                if (line.StartsWith("set zero "))
                {
                    zeroTile = Convert.ToInt32(line.Replace("set zero ", ""));
                }
                else if (line.StartsWith("set "))
                {
                    string[] info = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    int tile = Convert.ToInt32(info[1]);
                    Direction dir = (Direction)Enum.Parse(typeof(Direction), info[2]);
                    int destTile = Convert.ToInt32(info[4]);

                    tiles.Add(new Tuple<int, Direction, int>(tile, dir, destTile));
                }
            }
        }

        public override void WriteData(ref BinaryFileWriter writer)
        {
            // Write the header
            writer.WriteBits("001");

            // Write the zero  (if present)
            if (zeroTile != -1)
            {
                writer.WriteBits("00");
                writer.WriteBits(RoundBitsToBytes(Convert.ToString(zeroTile, 2), 2));
            }

            // Write all the other connections
            foreach (Tuple<int, Direction, int> tile in tiles)
            {
                writer.WriteBits("01");
                writer.WriteBits(RoundBitsToBytes(Convert.ToString(tile.Item1, 2), 2));
                writer.WriteBits(RoundBits(Convert.ToString((int)tile.Item2, 2), 2));
                writer.WriteBits(RoundBitsToBytes(Convert.ToString(tile.Item3, 2), 2));
            }

            // Write the closing data
            writer.WriteBits("11");
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
