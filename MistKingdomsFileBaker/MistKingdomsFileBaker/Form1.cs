using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MistKingdomsFileBaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            textBox1.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }

            textBox2.Text = saveFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(0);

            string[] filenames = System.IO.Directory.GetFiles(textBox1.Text);

            backgroundWorker1.ReportProgress(1);

            MKFile mkfile = new MKFile(textBox2.Text);

            int index = 0;
            foreach (string file in filenames)
            {
                mkfile.WriteObjectToFile(file);
                backgroundWorker1.ReportProgress(2, 100f / filenames.Length * index);
                index++;
            }

            mkfile.Close();

            backgroundWorker1.ReportProgress(3);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
                label3.Text = "Indexing files in folder...";
            }
            else if (e.ProgressPercentage == 1)
            {
                progressBar1.Style = ProgressBarStyle.Continuous;
                progressBar1.Value = 0;
                label3.Text = "Baking files...";
            }
            else if (e.ProgressPercentage == 2)
            {
                progressBar1.Value = Convert.ToInt32(e.UserState);
            }
            else if (e.ProgressPercentage == 3)
            {
                progressBar1.Value = 100;
                label3.Text = "Done";
                MessageBox.Show("Done baking files", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
