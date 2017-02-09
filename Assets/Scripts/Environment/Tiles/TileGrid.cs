using Assets.Scripts.Grid;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Environment.Tiles
{
    class TileGrid
    {
        #region "Fields"

        private int zeroTile;
        List<object[]> connections;

        #endregion

        #region "Constructors"

        private TileGrid()
        {
            connections = new List<object[]>();
            zeroTile = -1;
        }

        #endregion

        #region "Properties"

        public int ZeroTile
        {
            get { return zeroTile; }
        }

        public object[][] Connections
        {
            get { return connections.ToArray(); }
        }

        #endregion

        #region "Methods"

        public void ParseBinaryData(BinaryStreamReader reader)
        {
            int opcode;
            while ((opcode = Convert.ToInt32(reader.ReadBits(2), 2)) != 3)
            {
                if (opcode == 0)
                {
                    zeroTile = Convert.ToInt32(reader.ReadBits(16), 2);
                }
                else if (opcode == 1)
                {
                    int tileId = Convert.ToInt32(reader.ReadBits(16), 2);
                    //Tile tile = tiles.FirstOrDefault(x => x.Id == tileId.ToString());
                    //if (tile == null)
                    //{
                    //    tile = new Tile(tileId);
                    //    tiles.Add(tile);
                    //}
                    Direction dir = Direction.GetDirectionFromValue(Convert.ToInt32(reader.ReadBits(2), 2));
                    int otherId = Convert.ToInt32(reader.ReadBits(16), 2);
                    //Tile otherTile = tiles.FirstOrDefault(x => x.Id == otherId.ToString());
                    //if (otherTile == null)
                    //{
                    //    otherTile = new Tile(otherId);
                    //    tiles.Add(otherTile);
                    //}
                    //otherTile.AddNeighbour(dir, tile);
                    connections.Add(new object[] { otherId, dir, tileId });
                }
            }
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"



        #endregion

        #region "Static Methods"

        public static TileGrid GetGridFromBinaryData(BinaryStreamReader reader)
        {
            TileGrid grid = new TileGrid();
            grid.ParseBinaryData(reader);
            return grid;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
