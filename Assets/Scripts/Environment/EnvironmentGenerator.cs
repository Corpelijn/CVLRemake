using Assets.Scripts.Environment.Tiles;
using Assets.Scripts.Game.Content;
using Assets.Scripts.Grid;
using Assets.Scripts.Grid.GridObjects;
using Assets.Scripts.Grid.Ground;
using Assets.Scripts.Grid.Other;
using Assets.Scripts.Input;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Environment
{
    class EnvironmentGenerator : MonoBaseClass<EnvironmentGenerator>
    {
        #region "Fields"

        private Grid.Grid groundGrid;
        private Grid.Grid objectGrid;
        private Tile zeroTile;
        private List<Tile> tiles;

        public float DrawingDistance = 40f;

        #endregion

        #region "Constructors"



        #endregion

        #region "Properties"

        public Tile ZeroTile
        {
            set { zeroTile = value; }
        }

        #endregion

        #region "Methods"

        //private void ParseGridFile(string data)
        //{
        //    string[] lines = data.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

        //    foreach (string line in lines)
        //    {
        //        string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //        if (line.StartsWith("#"))
        //        { continue; }
        //        else if (words[0] == "set")
        //        {
        //            if (words[1] == "zero")
        //            {
        //                zeroTile = tiles.FirstOrDefault(x => x.Id == words[2]);
        //            }
        //            else
        //            {
        //                Tile tile = tiles.FirstOrDefault(x => x.Id == words[4]);
        //                Direction dir = words[2];
        //                Tile neighbour = tiles.FirstOrDefault(x => x.Id == words[1]);

        //                tile.AddNeighbour(dir, neighbour);
        //            }
        //        }
        //    }
        //}

        private void GenerateGrid(Tile currentTile, int x, int y)
        {
            // Add the current tile
            groundGrid.AddObject(new SubGrid(groundGrid, currentTile.GroundGrid, x, y));
            objectGrid.AddObject(new SubGrid(objectGrid, currentTile.ObjectGrid, x, y));

            // Add the neighbours (recursive)
            foreach (KeyValuePair<Direction, Tile> neighbour in currentTile.Neighbours)
            {
                int nextX = x;
                int nextY = y;
                if (neighbour.Key == Direction.North)
                {
                    nextY += currentTile.GroundGrid.Height;
                }
                else if (neighbour.Key == Direction.East)
                {
                    nextX += currentTile.GroundGrid.Width;
                }
                else if (neighbour.Key == Direction.South)
                {
                    nextY -= neighbour.Value.GroundGrid.Height;
                }
                else if (neighbour.Key == Direction.West)
                {
                    nextX -= neighbour.Value.GroundGrid.Width;
                }
                GenerateGrid(neighbour.Value, nextX, nextY);
            }
        }

        //private void ParseFolderForTiles(string folder)
        //{
        //    // Find all the tile files in the folder
        //    string[] files = System.IO.Directory.GetFiles(folder, "*.tile");
        //    foreach (string file in files)
        //    {
        //        tiles.Add(new Tile(System.IO.File.ReadAllText(file)));
        //    }

        //    // Find a grid file in the folder
        //    files = System.IO.Directory.GetFiles(folder, "*.grid");

        //    // Parse the found files
        //    foreach (string file in files)
        //    {
        //        ParseGridFile(System.IO.File.ReadAllText(file));
        //    }
        //}

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void Start()
        {
            tiles = new List<Tile>();
            //ParseFolderForTiles(@"D:\Users\Bas\Afbeeldingen\cvl\content\tiles");

            groundGrid = new Grid.Grid("Ground");
            objectGrid = new Grid.Grid("Objects");

            zeroTile.Fog = false;
            GenerateGrid(zeroTile, 0, 0);

            //Grid.Grid surroundingGrid = new Grid.Grid("Surrounding-Mist");

            //surroundingGrid.AddObject(new Fog(surroundingGrid, 0 , -10, 30, 30));

            //surroundingGrid.Draw(0, 0, transform);

            //groundGrid = new Grid.Grid("demo-terrain");
            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 7; j++)
            //    {
            //        if (i == 1 && j == 1)
            //            groundGrid.AddObject(new Water(groundGrid, i, j));
            //        else
            //            groundGrid.AddObject(new Grass(groundGrid, i, j));
            //    }
            //}
            //PRNG.ChangeSeed(0);

            //for (int i = 0; i < 5; i++)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        //    if (i == 10)
            //        //    {
            //        //        grid.AddObject(new Water(grid, i, j));
            //        //    }
            //        //else
            //        grid.AddObject(new Grass(grid, i, j));
            //        grid.AddObject(new Grid.Other.Tree(grid, i, j));

            //        //if (i == 14)
            //        //{
            //        //    grid.AddObject(new Grid.Other.Tree(grid, i, j));
            //        //}
            //    }
            //}
        }

        public override void Update()
        {
            groundGrid.Draw(0, 0, transform);
            objectGrid.Draw(0, 0, transform);
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
