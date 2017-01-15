using Assets.Scripts.Grid;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Ground;
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
        private string fillGround;
        private List<TileGround> ground;
        private List<TileObject> objects;
        private Dictionary<Direction, Tile> neighbours;

        #endregion

        #region "Constructors"

        public Tile(TextAsset data)
        {
            objects = new List<TileObject>();
            neighbours = new Dictionary<Direction, Tile>();
            ground = new List<TileGround>();

            string[] lines = data.text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            ParseFile(lines);
            GenerateGrid();
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

        #endregion

        #region "Methods"

        public void AddNeighbour(Direction direction, Tile tile)
        {
            neighbours.Add(direction, tile);
        }

        private void ParseFile(string[] lines)
        {
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

        private void GenerateGrid()
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
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override string ToString()
        {
            return name + " (" + id + ")";
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
