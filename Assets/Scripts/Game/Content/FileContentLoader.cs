using Assets.Scripts.Environment.Tiles;
using Assets.Scripts.Game.Content.FileParsers;
using Assets.Scripts.Grid;
using Assets.Scripts.Other;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Game.Content
{
    class FileContentLoader : ContentLoader
    {
        #region "Fields"

        private List<string> filesToRead;
        private List<Tile> tiles;
        private List<TileGrid> gridInformation;

        #endregion

        #region "Constructors"

        public FileContentLoader()
        {
            filesToRead = new List<string>();
            tiles = new List<Tile>();
            gridInformation = new List<TileGrid>();
        }

        #endregion

        #region "Properties"

        

        #endregion

        #region "Methods"

        public void ParseDirectory(string directory)
        {
            filesToRead.AddRange(Directory.GetFiles(directory, "*", SearchOption.AllDirectories));
        }

        public void AddFiles(string[] files)
        {
            filesToRead.AddRange(files);
        }

        private void ParseFile(string filename)
        {
            BinaryStreamReader reader = new BinaryStreamReader(filename);

            // Check the file header
            if (reader.ReadCharacters(2) != "MK")
            {
                // The file does not have a MK header
                return;
            }

            // Check the file version
            if (Convert.ToInt32(reader.ReadBits(4), 2) == 0)
            {
                // The file version is 0, the current correct version
                //System.Threading.ThreadPool.QueueUserWorkItem(StartParsingFile, reader);
                StartParsingFile(reader);
            }

            reader.Close();
        }

        private void StartParsingFile(object reader)
        {
            MKFile file = new MKFile(reader as BinaryStreamReader);
            file.ParseFile();

            tiles.AddRange(file.Tiles);
            gridInformation.AddRange(file.GridInformation);
        }

        #endregion

        #region "Abstract/Virtual Methods"



        #endregion

        #region "Inherited Methods"

        public override void ParseContent()
        {
            // Parse all the found files
            foreach (string file in filesToRead)
            {
                ParseFile(file);
            }

            // Stitch some information together
            foreach (TileGrid gridInfo in gridInformation)
            {
                foreach (object[] connection in gridInfo.Connections)
                {
                    Tile source = tiles.FirstOrDefault(x => x.Id == connection[0].ToString());
                    if (source == null)
                    {
                        continue;
                    }
                    Tile nextTile = tiles.FirstOrDefault(x => x.Id == connection[2].ToString());
                    if (nextTile == null)
                    {
                        continue;
                    }
                    source.AddNeighbour((Direction)connection[1], nextTile);
                }

                if(gridInfo.ZeroTile != -1)
                {
                    zeroTile = tiles.FirstOrDefault(x => x.Id == gridInfo.ZeroTile.ToString());
                }
            }
        }

        #endregion

        #region "Static Methods"



        #endregion

        #region "Operators"



        #endregion
    }
}
