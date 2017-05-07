using Assets.Scripts.Data.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Data
{
    class LocalFile
    {
        #region "Fields"

        private string filename;
        private string baseDirectory;
        private string checksum;
        private bool networkFile;
        private bool needsUpdate;

        #endregion

        #region "Constructors"

        public LocalFile(string filename, string baseDirectory, string checksum)
        {
            this.filename = filename;
            this.baseDirectory = baseDirectory;
            this.checksum = checksum;
            needsUpdate = false;
            networkFile = false;
        }

        public LocalFile(string filename, string checksum)
        {
            this.filename = filename;
            this.checksum = checksum;
            networkFile = true;
            needsUpdate = true;
        }

        #endregion

        #region "Properties"

        public string Filename
        {
            get { return filename; }
        }

        public string Checksum
        {
            get { return checksum; }
        }

        public string BaseDirectory
        {
            get { return baseDirectory; }
            set { baseDirectory = value; }
        }

        public bool NeedsUpdate
        {
            get { return needsUpdate; }
            set { needsUpdate = value; }
        }

        public string FullPath
        {
            get
            {
                if(baseDirectory == null)
                {
                    baseDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + @"\MistKingdoms\";
                    Directory.CreateDirectory(baseDirectory);
                }
                return Path.GetFullPath(baseDirectory + filename);
            }
        }

        public bool NetworkFile
        {
            get { return networkFile; }
        }

        #endregion

        #region "Methods"

        public void GetFromDatabase()
        {
            DataTable results;
            using (MySqlDatabaseConnection connection = MySqlDatabaseConnection.GetConnection())
            {
                connection.Open();
                results = connection.ExecuteQuery("select file from file where checksum=?", checksum);
            }

            byte[] data = (byte[])results.GetDataFromRow(0, "file");
            Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
            using (FileStream stream = new FileStream(FullPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                stream.Write(data, 0, data.Length);
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
