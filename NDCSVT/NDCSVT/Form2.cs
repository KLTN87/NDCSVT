using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.IO;

namespace Grabcut
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        string[] inputFiles;

        List<Image<Bgr, byte>> imgInputList = new List<Image<Bgr, byte>>();

        List<String> vectorList = new List<string>();


    private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();

        }






        private void open()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Select multiply images";
            opf.Multiselect = true;
            opf.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                inputFiles = opf.FileNames;
                foreach (string filename in inputFiles)
                {
                    //Image<Bgr, byte> tempImg = new Image<Bgr, byte>(filename);
                    //imgInputList.Add(tempImg);
                    this.listBox1.Items.Add(filename.ToString());

                }

            }


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            initComBoBox();
        }


        private void initComBoBox()
        {

            comboBox_Label.DisplayMember = "Text";
            comboBox_Label.ValueMember = "Value";

            var itemsLabel = new[] {
                new { Text = "0", Value = "0" },
                new { Text = "1", Value = "1" },
                new { Text = "2", Value = "2" },
                new { Text = "3", Value = "3" },
                new { Text = "4", Value = "4" },
                new { Text = "5", Value = "5" },
                new { Text = "6", Value = "6" },
                new { Text = "7", Value = "7" },
                new { Text = "8", Value = "8" },
                new { Text = "9", Value = "9" },
                new { Text = "10 (a)", Value = "10" },
                new { Text = "11 (b)", Value = "11" },
                new { Text = "12 (c)", Value = "12" },
                new { Text = "13 (d)", Value = "13" },
                new { Text = "14 (e)", Value = "14" },
                new { Text = "15 (f)", Value = "15" },
                new { Text = "16 (g)", Value = "16" },
                new { Text = "17 (h)", Value = "17" },
                new { Text = "18 (i)", Value = "18" },
                new { Text = "19 (j)", Value = "19" },
                new { Text = "20 (k)", Value = "20" },
                new { Text = "21 (l)", Value = "21" },
                new { Text = "22 (m)", Value = "22" },
                new { Text = "23 (n)", Value = "23" },
                new { Text = "24 (o)", Value = "24" },
                new { Text = "25 (p)", Value = "25" },
                new { Text = "26 (q)", Value = "26" },
                new { Text = "27 (r)", Value = "27" },
                new { Text = "28 (s)", Value = "28" },
                new { Text = "29 (t)", Value = "29" },
                new { Text = "30 (v)", Value = "30" },
                new { Text = "31 (u)", Value = "31" },
                new { Text = "32 (w)", Value = "32" },
                new { Text = "33 (x)", Value = "33" },
                new { Text = "34 (y)", Value = "34" },
                new { Text = "35 (z)", Value = "35" },
                new { Text = "36 (A)", Value = "36" },
                new { Text = "37 (B)", Value = "37" },
                new { Text = "38 (C)", Value = "38" },
                new { Text = "39 (D)", Value = "39" },
                new { Text = "40 (E)", Value = "40" },
                new { Text = "41 (F)", Value = "41" },
                new { Text = "42 (G)", Value = "42" },
                new { Text = "43 (H)", Value = "43" },
                new { Text = "44 (I)", Value = "44" },
                new { Text = "45 (J)", Value = "45" },
                new { Text = "46 (K)", Value = "46" },
                new { Text = "47 (L)", Value = "47" },
                new { Text = "48 (M)", Value = "48" },
                new { Text = "49 (N)", Value = "49" },
                new { Text = "50 (O)", Value = "50" },
                new { Text = "51 (P)", Value = "51" },
                new { Text = "52 (Q)", Value = "52" },
                new { Text = "53 (R)", Value = "53" },
                new { Text = "54 (S)", Value = "54" },
                new { Text = "55 (T)", Value = "55" },
                new { Text = "56 (U)", Value = "56" },
                new { Text = "57 (V)", Value = "57" },
                new { Text = "58 (W)", Value = "58" },
                new { Text = "59 (X)", Value = "59" },
                new { Text = "60 (Y)", Value = "60" },
                new { Text = "61 (Z)", Value = "61" }

            };

            comboBox_Label.DataSource = itemsLabel;
            comboBox_Label.SelectedIndex = 0;



            comboBox_HOG.DisplayMember = "Text";
            comboBox_HOG.ValueMember = "Value";

            var itemsHOG = new[] {
                new { Text = "36 feature values", Value = "36" },
                new { Text = "144 feature values", Value = "144" },
                new { Text = "576 feature values", Value = "576" },
                new { Text = "2304 feature values", Value = "2304" },
                new { Text = "5832 feature values", Value = "5832" },
                new { Text = "11664 feature values", Value = "11664" },
                new { Text = "22032 feature values", Value = "22032" }
            };

            comboBox_HOG.DataSource = itemsHOG;
            comboBox_HOG.SelectedIndex = 0;
        }





        private Image<Bgr, Byte> IResize(Image<Bgr, Byte> im, int w, int h)
        {
            return im.Resize(w, h, Emgu.CV.CvEnum.Inter.Linear);
        }


        private float[] GetVector(Image<Bgr, Byte> im, HOGDescriptor hog)
        {

            Image<Bgr, Byte> imageOfInterest = IResize(im, 512, 512);
            return hog.Compute(imageOfInterest, Size.Empty, Size.Empty, null);
        }


        private float[] getHOGFeature(Image<Bgr, Byte> im, int numberValues)
        {
            HOGDescriptor des;

            if (numberValues == 36)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(512, 512),
                new Size(32, 32), new Size(256, 256), 9);

            }
            else if (numberValues == 144)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(512, 512),
                     new Size(32, 32), new Size(128, 128), 9);

            }

            else if (numberValues == 576)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(512, 512),
                    new Size(32, 32), new Size(64, 64), 9);

            }

            else if (numberValues == 2304)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(512, 512),
                    new Size(32, 32), new Size(32, 32), 9);

            }

            else if (numberValues == 5832)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(256, 256),
                     new Size(32, 32), new Size(64, 128), 9);

            }
            else if (numberValues == 11664)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(256, 256),
                     new Size(32, 32), new Size(64, 64), 9);

            }
            else if (numberValues == 22032)
            {
                des = new HOGDescriptor(new Size(512, 512), new Size(256, 256),
                     new Size(16, 32), new Size(64, 64), 9);

            }
            else
            {
                //des = new HOGDescriptor();
                des = new HOGDescriptor(new Size(512, 512), new Size(512, 512),
                    new Size(32, 32), new Size(256, 256), 9); //36
            }

            float[] hog = GetVector(im, des);
            return hog;
        }



        private Image<Bgr, Byte> GrabcutImg(Image<Bgr, Byte> img)
        {
            try
            {
                
                //test//
                Matrix<double> bg = new Matrix<double>(1, 65);
                bg.SetZero();
                Matrix<double> fg = new Matrix<double>(1, 65);
                fg.SetZero();
                Image<Gray, byte> mask = new Image<Gray, byte>(img.Size);
                Rectangle rect = new Rectangle(img.Cols / 20, 1, (int)((double)img.Width / (0.75)), img.Height);
                CvInvoke.GrabCut(img, mask, rect,
                   bg, fg, 5, Emgu.CV.CvEnum.GrabcutInitType.InitWithRect);
                Image<Gray, byte> mask2 = new Image<Gray, byte>(img.Size);
                ////here i set the only white pixels (foreground object ) to 1 and 0 for else
                for (int x = 0; x < mask.Cols; x++)
                {
                    for (int y = 0; y < mask.Rows; y++)
                    {
                        if (mask2[y, x].Intensity > new Gray(200).Intensity)
                        {
                            mask[y, x] = new Gray(1);
                        }
                        else
                        {

                        }
                    }
                }
                CvInvoke.GrabCut(img, mask, rect,
                     bg, fg, 5, Emgu.CV.CvEnum.GrabcutInitType.InitWithMask);
                for (int x = 0; x < mask.Cols; x++)
                {
                    for (int y = 0; y < mask.Rows; y++)
                    {
                        if (mask[y, x].Intensity == new Gray(1).Intensity || mask[y, x].Intensity == new Gray(3).Intensity)
                        {
                            mask[y, x] = new Gray(1);
                        }
                        else
                        {
                            mask[y, x] = new Gray(0);
                        }
                    }
                }
                img = img.Mul(mask.Convert<Bgr, byte>());
                return img;
            }

            catch
            {

            }
            return img;

        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(inputFiles.Length<1)
            {

                MessageBox.Show("Lỗi!, vui lòng nhập file hình ảnh");

            }

            else
            {

                int noLabel = int.Parse(comboBox_Label.SelectedValue.ToString());
                int noHOGvalue = int.Parse(comboBox_Label.SelectedValue.ToString());

                startProcessing(inputFiles, noHOGvalue, noLabel);


                MessageBox.Show("Hoàn thành");

            }


        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imgInputList.Clear();
            listBox1.Items.Clear();

        }

        private void startProcessing(string[] dsFile, int label, int HOGvalue)
        {
            int pcbValue = 0;
            progressBar1.Minimum = pcbValue; //Đặt giá trị nhỏ nhất cho ProgressBar
            progressBar1.Maximum = dsFile.Length; //Đặt giá trị lớn nhất cho ProgressBar



            foreach (string filename in inputFiles)
            {
                Image<Bgr, byte> tempImg = new Image<Bgr, byte>(filename);

                var imgGrabCut = GrabcutImg(tempImg);

                var featureHOG = getHOGFeature(imgGrabCut,HOGvalue);

                var imgHOG = normalizeFloatArray(featureHOG);


                string textHOG = string.Join(" ", imgHOG);

                vectorList.Add(textHOG);

                pcbValue++;
                progressBar1.Value = pcbValue;

            }

            printVectorList(vectorList, label);

        }

        private void printVectorList(List<String>  dsVectoc, int label)
        {

            foreach (String vec in vectorList)
            {

                richTextBox1.Text = richTextBox1.Text + "\n<label>" + label + "</label>\n<vector>" + vec + "</vector>";
            }

        }



        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Text File|*.txt";
            sfd.FileName = "DacTrung";
            sfd.Title = "Save Text File";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = sfd.FileName;
                using (var fs = File.Create(path))
                using (StreamWriter bw = new StreamWriter(fs))
                {
                    bw.Write(richTextBox1.Text);
                    bw.Close();
                }
            }
        }

        private float normalizeFloat(float value, float min, float max)
        {
            float temp = (value - min) / (max - min);
            return temp;
        }

        private float[] normalizeFloatArray(float[] arr)
        {
            float[] terms = new float[arr.Length];
            for (int runs = 0; runs < arr.Length; runs++)
            {

                terms[runs] = normalizeFloat( arr[runs], arr.Min(), arr.Max() );
            }

            return terms;
        }


    }






}