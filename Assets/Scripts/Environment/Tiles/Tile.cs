using Assets.Scripts.Grid;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Ground;
using Assets.Scripts.Grid.Other;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Environment.Tiles
{
    class Tile
    {
        #region "Fields"

        private Grid.Grid groundGrid;
        private Grid.Grid objectGrid;
        private string name;
        private string id;
        private int width;
        private int height;
        private bool noFog;
        private bool available;
        private TileGroundTypes fillGround;
        private List<TileGround> ground;
        private List<TileObject> objects;
        private Dictionary<Direction, Tile> neighbours;

        private MultiBoolean drawingInfo;

        private bool selected;

        #endregion

        #region "Constructors"

        public Tile(string data)
        {
            objects = new List<TileObject>();
            neighbours = new Dictionary<Direction, Tile>();
            ground = new List<TileGround>();
            drawingInfo = new MultiBoolean();
            InitDrawingInfo();

            //string[] lines = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            //ParseFile(lines);
            GenerateGrid();
        }

        private Tile()
        {
            objects = new List<TileObject>();
            neighbours = new Dictionary<Direction, Tile>();
            ground = new List<TileGround>();
            drawingInfo = new MultiBoolean();
            InitDrawingInfo();
        }

        public Tile(int id)
        {
            this.id = id.ToString();
            neighbours = new Dictionary<Direction, Tile>();
        }

        #endregion

        #region "Properties"

        public Grid.Grid GroundGrid
        {
            get { return groundGrid; }
        }

        public Grid.Grid ObjectGrid
        {
            get { return objectGrid; }
        }

        public string Id
        {
            get { return id; }
        }

        public Dictionary<Direction, Tile> Neighbours
        {
            get { return neighbours; }
        }

        public bool Fog
        {
            get { return !noFog; }
            set { noFog = !value; }
        }

        public bool IsSelected
        {
            get { return selected; }
            set { selected = value; }
        }

        public bool Available
        {
            get { return available; }
            set { available = value; }
        }

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public MultiBoolean DrawingInfo
        {
            get { return drawingInfo; }
            set { drawingInfo = value; }
        }

        #endregion

        #region "Methods"

        public void AddNeighbour(Direction direction, Tile tile)
        {
            neighbours.Add(direction, tile);
        }

        private void InitDrawingInfo()
        {
            drawingInfo["fog"] = false;
            drawingInfo["selected"] = false;
        }

        public void GenerateGrid()
        {
            // Reset the random generator
            PRNG.ChangeSeed(0);

            groundGrid = new Grid.Grid(name + "_ground (" + id + ")");
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    TileGround ground = this.ground.FirstOrDefault(f => f.X == i && f.Y == j);
                    if (ground == null)
                        ground = new TileGround(i, j, fillGround);

                    GridObject go = ground.GetGridObject(groundGrid);
                    if (go != null)
                        groundGrid.AddObject(go);
                }
            }

            // Reset the random generator
            PRNG.ChangeSeed(0);

            objectGrid = new Grid.Grid(name + "_objects (" + id + ")", new Vector2(width, height));
            foreach (TileObject obj in this.objects)
            {
                GridObject go = obj.GetGridObject(objectGrid);
                if (go != null)
                    objectGrid.AddObject(go);
            }

            //grids.Add(grid);

            // Add the TileInformation component
            groundGrid.AddObject(new TileInformation(this, groundGrid, width, height));
            objectGrid.AddObject(new TileInformation(this, objectGrid, width, height));
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return name + " (" + id + ")";
        }

        public void ParseDataText(string data)
        {
            string[] lines = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            string line = null;
            int index = 0;
            while (index < lines.Length)
            {
                line = lines[index];
                string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (words[0].StartsWith("#"))
                { }
                else if (words[0] == "name")
                {
                    name = string.Join(" ", words.Skip(1).ToArray());
                }
                else if (words[0] == "id")
                {
                    id = words[1];
                }
                else if (words[0] == "size")
                {
                    string[] sizes = words[1].Split('x');
                    width = Convert.ToInt32(sizes[0]);
                    height = Convert.ToInt32(sizes[1]);
                }
                else if (words[0] == "special")
                {
                    if (words[1] == "nofog")
                    {
                        noFog = true;
                    }
                }
                else if (words[0] == "fill")
                {
                    if (words[1] == "ground")
                    {
                        fillGround = words[2];
                    }
                }
                else if (words[0] == "ground")
                {
                    ground.Add(new TileGround(Convert.ToInt32(words[2]), Convert.ToInt32(words[3]), words[1]));
                }
                else if (words[0] == "obj")
                {
                    objects.Add(new TileObject(Convert.ToInt32(words[2]), Convert.ToInt32(words[3]), words[1]));
                }
                index++;
            }

        }

        public void ParseDataBinary(BinaryStreamReader reader)
        {
            id = Convert.ToInt32(reader.ReadBits(16), 2).ToString();
            int length = Convert.ToInt32(reader.ReadBits(8), 2);

            StringBuilder name = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                name.Append(reader.ReadFunctionCharacter());
            }

            this.name = name.ToString();

            width = Convert.ToInt16(reader.ReadBits(4), 2);
            height = Convert.ToInt16(reader.ReadBits(4), 2);

            // Read additional file information
            int tileInformationOpcode;
            while ((tileInformationOpcode = Convert.ToInt32(reader.ReadBits(4), 2)) != 15)
            {
                reader.ReadBits(16);
            }

            // Read the the tile content
            int contentTileOpcode;
            while ((contentTileOpcode = Convert.ToInt32(reader.ReadBits(2), 2)) != 3)
            {
                // Fill opcode
                if (contentTileOpcode == 0)
                {
                    // Read the ground type
                    if (reader.ReadBit() == "0")
                    {
                        // Read the fill type
                        fillGround = Convert.ToInt32(reader.ReadBits(4), 2);
                    }
                }
                // Object opcode
                else if (contentTileOpcode == 1)
                {
                    // Read the object type
                    int obj = Convert.ToInt32(reader.ReadBits(16), 2);
                    // Read the x and y position
                    int x = Convert.ToInt32(reader.ReadBits(4), 2);
                    int y = Convert.ToInt32(reader.ReadBits(4), 2);
                    // Add the object to the list
                    objects.Add(new TileObject(x, y, obj));
                }
                // Ground opcode
                else if (contentTileOpcode == 2)
                {
                    // Read the ground type
                    TileGroundTypes ground = Convert.ToInt32(reader.ReadBits(4), 2);
                    // Read the x and y position
                    int x = Convert.ToInt32(reader.ReadBits(4), 2);
                    int y = Convert.ToInt32(reader.ReadBits(4), 2);
                    // Add the ground to the list
                    this.ground.Add(new TileGround(x, y, ground));
                }
            }
        }

        #endregion

        #region "Static Methods"

        public static Tile GetTileFromBinaryData(BinaryStreamReader reader)
        {
            Tile t = new Tile();
            t.ParseDataBinary(reader);
            return t;
        }

        #endregion

        #region "Operators"



        #endregion
    }
}
