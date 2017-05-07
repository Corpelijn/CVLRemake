using Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistKingdomsUpdater
{
    public partial class Form1 : Form
    {
        private bool downloadCompleted;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(10, "Initializing");
            string directory = Path.GetFullPath(".");
            string[] argv = Environment.GetCommandLineArgs();
            int argc = argv.Length;
            string currentVersion = "";

            for (int i = 1; i < argc; i++)
            {
                if (argv[i].StartsWith("-current="))
                    currentVersion = argv[i].Replace("-current=", "");
                else if (argv[i].StartsWith("-installdir="))
                    directory = argv[i].Replace("-installdir=", "");
            }

            Database.DataTable results = null;
            using (MySqlDatabaseConnection connection = MySqlDatabaseConnection.GetConnection())
            {
                connection.Open();

                results = connection.ExecuteQuery("select * from game");
            }

            if (results == null)
                Application.Exit();

            string newVersion = results.GetDataFromRow(0, "version").ToString();

            if (newVersion != currentVersion)
            {
                try
                {
                    File.Delete("mk.exe");
                    Directory.Delete("mk_Data", true);
                }
                catch (Exception ex)
                {
                }

                try
                {
                    downloadCompleted = false;
                    backgroundWorker1.ReportProgress(10, "Downloading");
                    string packageLocation = results.GetDataFromRow(0, "file").ToString();
                    WebClient client = new WebClient();
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadFileAsync(new Uri(packageLocation), "mk.zip");

                    while (!downloadCompleted)
                    {
                        Thread.Sleep(10);
                    }

                    backgroundWorker1.ReportProgress(10, "Copying new files");
                    ZipArchive zip = new ZipArchive(new FileStream("mk.zip", FileMode.Open));

                    backgroundWorker1.ReportProgress(0, zip.Entries.Count);

                    for (int i = 0; i < zip.Entries.Count; i++)
                    {
                        if (zip.Entries[i].FullName.EndsWith("/"))
                        {
                            Directory.CreateDirectory(Path.GetFullPath(zip.Entries[i].FullName));
                            backgroundWorker1.ReportProgress(1);
                            continue;
                        }
                        byte[] buffer = new byte[zip.Entries[i].Open().Length];
                        zip.Entries[i].Open().Read(buffer, 0, buffer.Length);

                        File.WriteAllBytes(Path.GetFullPath(zip.Entries[i].FullName), buffer);
                        backgroundWorker1.ReportProgress(1);
                    }

                    zip.Dispose();
                }
                catch (Exception ex)
                {
                    backgroundWorker1.ReportProgress(2, ex.ToString());
                }

                File.Delete("mk.zip");

                if (File.Exists("updater_new.exe"))
                {
                    File.WriteAllLines("update.bat", new string[] {
                        "@echo off",
                        "echo Installing update . . . Please wait . . .",
                        "if exist updater_new.exe goto DEL_OLD",
                        ":NEXT1",
                        "if exist extra_update.bat goto RUN_EXTRA",
                        ":NEXT2",
                        "start mk.exe > NUL",
                        "del update.bat > NUL",
                        "exit",

                        ":DEL_OLD",
                        "del updater.exe > NUL",
                        "ren updater_new.exe updater.exe > NUL",
                        "goto NEXT1",

                        ":RUN_EXTRA",
                        "start extra_update.bat",
                        "goto NEXT2"
                    });

                    Process.Start("update.bat");
                }
                else
                {
                    Process.Start("mk.exe");
                }

                Application.Exit();
            }
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            downloadCompleted = true;
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            backgroundWorker1.ReportProgress(10, "Downloading (" + e.ProgressPercentage + "%)");
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                progressBar1.Maximum = (int)e.UserState;
            }
            else if (e.ProgressPercentage == 1)
                progressBar1.Value++;
            else if (e.ProgressPercentage == 10)
            {
                label1.Text = e.UserState.ToString() + " - Please wait...";
            }
        }
    }
}
