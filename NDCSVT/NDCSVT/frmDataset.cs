using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Grabcut
{
    public partial class frmDataset : Form
    {
        public frmDataset()
        {
            InitializeComponent();
        }

        private string pathText = null;

        private List<String> Get_Train = new List<string>();
        private List<String> Get_Test = new List<string>();

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Text file | *.txt";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathText = dialog.FileName;
                    MessageBox.Show("Text loaded");
                }

                int countLine = File.ReadLines(pathText).Count();
                label1.Text = label1.Text + " " + (countLine / 2).ToString();
                richTextBox1.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pathText == null)
            {
                MessageBox.Show("Please open text file");
            }
            else
            {
                var labelFormText = frmTrain.txt2ArrLabel(pathText);
                var temptLa = labelFormText.Distinct().ToArray();
                List<String> stringLabel = new List<String>();

                foreach (int la in temptLa)
                {
                    string tempString = frmPredict.getStringPredict(la);
                    stringLabel.Add(tempString);
                }

                richTextBox1.Text = string.Join(", ", stringLabel);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (pathText == null)
            {
                MessageBox.Show("Please open text file");
            }
            else
            {
                readtxtfile(pathText);
                int nolabel = getFirstLabelFormText(pathText);
                string path0 = SaveFile70(nolabel);
                writeTextFile(path0, Get_Train);
                string path1 = SaveFile30(nolabel);
                writeTextFile(path1, Get_Test);
                Get_Test.Clear();
                Get_Train.Clear();
            }
        }

        public static void writeTextFile(string path, List<string> arrString)
        {
            using (TextWriter writer = File.CreateText(path))
            {
                foreach (string item in arrString)
                {
                    writer.WriteLine(item);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new frmMenu();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmGetFeature.MergeFile(frmGetFeature.OpenFile(), SaveFile());
        }

        private string SaveFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text|*.txt";
            save.FilterIndex = 1;

            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                return path;
            }
            return null;
        }

        private void readtxtfile(string inputtxt)
        {
            FileStream fs = new FileStream(inputtxt, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader(fs);
            sr.BaseStream.Seek(0, SeekOrigin.Begin);
            string[] lines = File.ReadLines(inputtxt).ToArray();
            int sodong = 0;
            int tongsodong = lines.Count();
            int lay70 = tongsodong * 70 / 100;
            int dem70 = 0, dem30 = 0;
            if (lay70 % 2 == 0)
            {
                foreach (var line in lines)
                {
                    sodong++;

                    if (sodong <= lay70)
                    {
                        Get_Train.Add(line);
                        dem70++;
                    }
                    else
                    {
                        Get_Test.Add(line);
                        dem30++;
                    }
                }
            }
            else
            {
                foreach (var line in lines)
                {
                    sodong++;

                    if (sodong < lay70)
                    {
                        Get_Train.Add(line);
                        dem70++;
                    }
                    else
                    {
                        Get_Test.Add(line);
                        dem30++;
                    }
                }
            }
        }

        private string SaveFile70(int nolabel)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text|*.txt";
            save.FilterIndex = 1;

            string nolabel1 = nolabel + "-training";
            save.FileName = nolabel1;

            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                return path;
            }

            return null;
        }

        private string SaveFile30(int nolabel)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text|*.txt";
            save.FilterIndex = 1;
            string nolabel1 = nolabel + "-testing";
            save.FileName = nolabel1;
            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                return path;
            }
            return null;
        }

        public static int getFirstLabelFormText(string path)
        {
            foreach (string line in System.IO.File.ReadLines(path))
            {
                if (line.Contains("<label>") == true)
                {
                    int tempint = int.Parse(line.Trim().Replace("<label>", "").Replace("</label>", "").Trim());
                    return tempint;
                }
            }
            return 0;
        }
    }
}