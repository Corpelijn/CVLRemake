using Assets.Scripts.Environment.Tiles;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Content.FileParsers
{
    class MKFile
    {
        #region "Fields"

        private BinaryStreamReader reader;
        private List<Tile> tiles;
        private List<TileGrid> gridInformation;

        #endregion

        #region "Constructors"

        public MKFile(BinaryStreamReader reader)
        {
            this.reader = reader;
            tiles = new List<Tile>();
            gridInformation = new List<TileGrid>();
        }

        #endregion

        #region "Properties"

        public Tile[] Tiles
        {
            get { return tiles.ToArray(); }
        }

        public TileGrid[] GridInformation
        {
            get { return gridInformation.ToArray(); }
        }

        #endregion

        #region "Methods"

        public void ParseFile()
        {
            while (!reader.EndOfStream)
            {
                // Read the file type
                int fileType = Convert.ToInt32(reader.ReadBits(3), 2);

                // Tile file
                if (fileType == 0)
                {
                    if (reader.BitsLeft < 24)
                        break;

                    Tile tile = Tile.GetTileFromBinaryData(reader);
                    tile.GenerateGrid();
                    tiles.Add(tile);
                }
                // Grid file
                else if (fileType == 1)
                {
                    if (reader.BitsLeft < 18)
                        break;

                    TileGrid grid = TileGrid.GetGridFromBinaryData(reader);
                    gridInformation.Add(grid);
                }
            }
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
