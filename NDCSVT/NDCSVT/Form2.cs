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
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;
using Emgu.CV.CvEnum;
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

        //List<Image<Bgr, byte>> imgInputList = new List<Image<Bgr, byte>>();

        List<String> vectorList = new List<string>();


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();

        }
        private void openFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFolder();
        }

        private void open()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Select multiply images";
            opf.Multiselect = true;
            opf.Filter = "Image Files | *.jpg; *.jpeg; *.png";
            listBox1.Items.Clear();
            if (opf.ShowDialog() == DialogResult.OK)
            {
                inputFiles = opf.FileNames;
                foreach (string filename in inputFiles)
                {
                    //Image<Bgr, byte> tempImg = new Image<Bgr, byte>(filename);
                    //imgInputList.Add(tempImg);
                    this.listBox1.Items.Add(filename.ToString());

                }
                groupBox4.Text = "Danh sách file (" + inputFiles.Length + ")";

            }
        }
        private void openFolder()
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();


                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    inputFiles = Directory.GetFiles(fbd.SelectedPath);
                    foreach (string filename in inputFiles)
                    {
                        this.listBox1.Items.Add(filename.ToString());

                    }
                }
            }
        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string nolabel = comboBox_Label.SelectedValue.ToString();
            string path = SaveFile();
            using (TextWriter writer = File.CreateText(path))
            {

                foreach (string vt in vectorList)
                {
                    writer.WriteLine("<label>" + nolabel + "</label>");
                    writer.WriteLine("<vector>" + vt + "</vector>");
                }
            }
        }

        //private void saveFileString1()
        //{
        //    SaveFileDialog sfd = new SaveFileDialog();
        //    sfd.Filter = "Text File|*.txt";
        //    sfd.FileName = "DacTrung";
        //    sfd.Title = "Save Text File";
        //    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        string path = sfd.FileName;
        //        using (var fs = File.Create(path))
        //        using (StreamWriter bw = new StreamWriter(fs))
        //        {
        //            bw.Write(richTextBox1.Text);
        //            bw.Close();
        //        }
        //    }

        //}


        private void mergeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MergeFile(OpenFile(), SaveFile());

        }

        private List<string> OpenFile()
        {
            List<string> path = new List<string>();
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Text|*.txt";
            open.FilterIndex = 1;

            if (open.ShowDialog() == DialogResult.OK)
            {
                path = open.FileNames.ToList();
            }
            return path;
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

        private void MergeFile(List<string> listFile, string pathOutput)
        {
            using (var output = File.Create(pathOutput))
            {
                foreach (var file in listFile)
                {
                    using (var input = File.OpenRead(file))
                    {
                        input.CopyTo(output);
                    }
                }
            }
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


            comboBox_SIFT.DisplayMember = "Text";
            comboBox_SIFT.ValueMember = "Value";

            var itemsSIFT = new[] {
                new { Text = "Newton", Value = "0" },
                new { Text = "MPEG7", Value = "1" },
                new { Text = "Gray", Value = "2" },
                new { Text = "Red", Value = "3" },
                new { Text = "Green", Value = "4" },
                new { Text = "Blue", Value = "5" },
            };

            comboBox_SIFT.DataSource = itemsSIFT;
            comboBox_SIFT.SelectedIndex = 0;
        }






        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (inputFiles.Length < 1)
            {

                MessageBox.Show("Lỗi!, vui lòng nhập file hình ảnh");

            }

            else
            {

                int noLabel = int.Parse(comboBox_Label.SelectedValue.ToString());
                int noHOGvalue = int.Parse(comboBox_HOG.SelectedValue.ToString());
                int noSIFTchoose = int.Parse(comboBox_SIFT.SelectedValue.ToString());

                string thoigianthucthi = startProcessing(inputFiles, noLabel, noHOGvalue, noSIFTchoose);


                MessageBox.Show("Hoàn thành " + thoigianthucthi + "  ms");

            }


        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //imgInputList.Clear();
            Array.Clear(inputFiles, 0, inputFiles.Length);

            listBox1.Items.Clear();
            richTextBox1.Clear();
            vectorList.Clear();

        }

        private string startProcessing(string[] dsFile, int label, int HOGvalue, int SIFTchoose)
        {
            int pcbValue = 0;
            progressBar1.Minimum = pcbValue; //Đặt giá trị nhỏ nhất cho ProgressBar
            progressBar1.Maximum = dsFile.Length; //Đặt giá trị lớn nhất cho ProgressBar

            var watch = System.Diagnostics.Stopwatch.StartNew();



            foreach (string filename in inputFiles)
            {
                Image<Bgr, byte> tempimg = new Image<Bgr, byte>(filename);

                int tempimgwsize = int.Parse(tempimg.Size.Width.ToString());

                //if (tempimgwsize < 32) // khi ảnh quá nhỏ
                //{
                //    tempimg = IResize(tempimg, 32, 32);
                //}
                tempimg = IResize(tempimg, 128, 128);

                var imggrabcut = GrabcutImg(tempimg);
                var featureSIFT = getSIFTFeature(imggrabcut, SIFTchoose);
                var featureHOG = getHOGFeature(imggrabcut, HOGvalue);

                var gopdactrung = concatDoubleArray(featureSIFT, featureHOG);
                var chuanhoadactrung = normalizeDoubleArray(gopdactrung);


                //string textDT = string.Join(" ", chuanhoadactrung);

                string textDT = string.Join(" ", featureSIFT);// test sift

                vectorList.Add(textDT);

                pcbValue++;
                progressBar1.Value = pcbValue;

            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            print10FirstVector(vectorList, label);

            return elapsedMs.ToString();

        }

        private void print10FirstVector(List<String> dsVectoc, int label)
        {
            int dem = 0;
            richTextBox1.Clear();
            foreach (String vec in vectorList)
            {

                dem++;
                richTextBox1.Text = richTextBox1.Text + "\n<label>" + label + "</label>\n<vector>" + vec + "</vector>";

                if (dem >= 10)
                    break;
            }

        }





        private double normalizeDouble(double value, double min, double max)
        {
            if(value ==0 && min == 0 && max == 0)
            {
                return 0;
            }

            double temp = (value - min) / (max - min);
            return temp;
        }
        private double[] normalizeDoubleArray(double[] arr)
        {
            double[] terms = new double[arr.Length];
            for (int runs = 0; runs < arr.Length; runs++)
            {

                terms[runs] = normalizeDouble(arr[runs], arr.Min(), arr.Max());
            }

            return terms;
        }
        private double[] concatDoubleArray(double[] first, double[] second)
        {
            double[] result = first.Concat(second).ToArray();

            return result;
        }




        //Hàm của Grabcut
        private Image<Bgr, Byte> GrabcutImg(Image<Bgr, Byte> img)
        {
            try
            {
                //Image<Gray, byte> tempimg = new Image<Gray, byte>(img.Size);
                //img = new Image<Bgr, Byte>(tempimg.Size);



                //test//
                Matrix<double> bg = new Matrix<double>(1, 65);
                bg.SetZero();
                Matrix<double> fg = new Matrix<double>(1, 65);
                fg.SetZero();
                Image<Gray, byte> mask = new Image<Gray, byte>(img.Size);
                Rectangle rect = new Rectangle(img.Cols / 10, 2, (int)((double)img.Width / (0.75)), img.Height);
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


                //hiện ảnh sau khi grabcut
                CvInvoke.Imshow("image", img);
                CvInvoke.WaitKey(0);


                return img;
            }

            catch
            {

            }
            return img;

        }







        //hàmm của HOG
        private Image<Bgr, Byte> IResize(Image<Bgr, Byte> im, int w, int h)
        {
            return im.Resize(w, h, Emgu.CV.CvEnum.Inter.Linear);
        }
        private double[] GetVector(Image<Bgr, Byte> im, HOGDescriptor hog)
        {

            Image<Bgr, Byte> imageOfInterest = IResize(im, 512, 512);
            float[] temp = hog.Compute(imageOfInterest, Size.Empty, Size.Empty, null);
            double[] doubleArray = Array.ConvertAll(temp, x => (double)x);
            return doubleArray;

        }
        private double[] getHOGFeature(Image<Bgr, Byte> im, int numberValues)
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

            double[] hog = GetVector(im, des);
            return hog;
        }











        //hàm của SIFT


        private double[] getSIFTFeature(Image<Bgr, Byte> im, int numberChoose)
        {
            double[] sift = new double[0];

            if (numberChoose==0)
            {
                sift = getSIFTNewton(im);
            }
            else  if (numberChoose == 1)
            {
                sift = getSIFTMpeg7(im);
            }
            else if (numberChoose == 2)
            {
                sift = getSIFTGray(im);
            }
            else if (numberChoose == 3)
            {
                sift = getSIFTRed(im);
            }
            else if (numberChoose == 4)
            {
                sift = getSIFTGreen(im);
            }
            else if (numberChoose == 5)
            {
                sift = getSIFTBlue(im);
            }
            else
            {
                sift = getSIFTNewton(im); //0
            }

            return sift;

        }
        private double[] getSIFTNewton(Image<Bgr, Byte> im)
        {
            Bitmap a = convertNewton(im.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);


            MKeyPoint[] keyPoints = vkPoint.ToArray();

            keypoint key;
            List<keypoint> keypointsList = new List<keypoint>();
            double maxx = 0, maxy = 0;
            foreach (MKeyPoint keyPoint in keyPoints)
            {
                key = new keypoint(keyPoint.Point.X, keyPoint.Point.Y, keyPoint.Size);
                keypointsList.Add(key);
                maxx++;
                maxy++;
            }

            //string temp = " ";

            //foreach (keypoint item in keypointsList)
            //{

            //    if (item == null)
            //    {
            //        temp = "Khong co gia tri";
            //    }
            //    else
            //        temp = temp + item.X + " " + item.Y + " ";
            //}
            //MessageBox.Show(temp, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //richTextBox1.Text = temp;

            double[] b = tinhHistogramNewton(a, keyPoints, maxx, maxy);
            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);


            double[] tempArr = c.Take(6).ToArray();

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            //pictureBox2.Image = sift_feature.ToBitmap();
            CvInvoke.Imshow("image", sift_feature);
            CvInvoke.WaitKey(0);


            return tempArr;


        }
        private double[] getSIFTMpeg7(Image<Bgr, Byte> im)
        {
            Bitmap a = convertMpeg7(IResize(im, 512, 512).ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);


            MKeyPoint[] keyPoints = vkPoint.ToArray();

            keypoint key;
            List<keypoint> keypointsList = new List<keypoint>();
            double maxx = 0, maxy = 0;
            foreach (MKeyPoint keyPoint in keyPoints)
            {
                key = new keypoint(keyPoint.Point.X, keyPoint.Point.Y, keyPoint.Size);
                keypointsList.Add(key);
                maxx++;
                maxy++;
            }

            //string temp = " ";

            //foreach (keypoint item in keypointsList)
            //{

            //    if (item == null)
            //    {
            //        richTextBox1.Text = "Khong co gia tri";
            //    }
            //    else
            //        temp = temp + item.X + " " + item.Y + " ";
            //}
            //richTextBox1.Text = temp;


            double[] b = tinhHistogramMpeg7(a, keyPoints, maxx, maxy);
            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);
            double[] tempArr = c.Take(25).ToArray();

            //Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            ////pictureBox2.Image = sift_feature.ToBitmap();
            //CvInvoke.Imshow("image", sift_feature);
            //CvInvoke.WaitKey(0);
            return tempArr;



        }
        private double[] getSIFTGray(Image<Bgr, Byte> im)
        {

            Bitmap a = convertGrayScale(im.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            //Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            //pictureBox2.Image = sift_feature.ToBitmap();

            double[] b = tinhHistogramGray(a);

            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);
            return c;
        }
        private double[] getSIFTRed(Image<Bgr, Byte> im)
        {

            Bitmap a = convertRed(im.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            //Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(255, 0, 0), Features2DToolbox.KeypointDrawType.Default);
            //pictureBox2.Image = sift_feature.ToBitmap();

            double[] b = tinhHistogramRed(a);

            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);
            return c;
        }
        private double[] getSIFTGreen(Image<Bgr, Byte> im)
        {

            Bitmap a = convertGreen(im.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            //Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            //pictureBox2.Image = sift_feature.ToBitmap();

            double[] b = tinhHistogramGreen(a);

            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);

            return c;
        }
        private double[] getSIFTBlue(Image<Bgr, Byte> im)
        {

            Bitmap a = convertBlue(im.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            //Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(255, 0, 0), Features2DToolbox.KeypointDrawType.Default);
            //pictureBox2.Image = sift_feature.ToBitmap();

            double[] b = tinhHistogramBlue(a);

            double mx = b[0];
            double mn = b[0];

            for (int i = 1; i < b.Length; i++)
            {
                if (b[i] > mx)
                {
                    mx = b[i];
                }


                if (b[i] < mn)
                {
                    mn = b[i];
                }
            }
            double[] c = balanceHistogram(b, mx, mn);
            return c;
        }

        public Bitmap convertGrayScale(Bitmap img)
        {
            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    byte red = pixel.R;
                    byte green = pixel.G;
                    byte blue = pixel.B;
                    byte a = pixel.A;

                    byte gray = (byte)((red + green + blue) / 3);
                    gimg.SetPixel(x, y, Color.FromArgb(a, gray, gray, gray));
                }
            }
            return gimg;
        }
        public Bitmap convertRed(Bitmap img)
        {
            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    byte red = pixel.R;
                    byte a = pixel.A;
                    byte green = pixel.G;
                    byte blue = pixel.B;

                    gimg.SetPixel(x, y, Color.FromArgb(a, red, 0, 0));
                }
            }
            return gimg;
        }
        public Bitmap convertGreen(Bitmap img)
        {

            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    byte red = pixel.R;
                    byte a = pixel.A;
                    byte green = pixel.G;
                    byte blue = pixel.B;

                    gimg.SetPixel(x, y, Color.FromArgb(a, 0, green, 0));
                }
            }
            return gimg;
        }
        public Bitmap convertBlue(Bitmap img)
        {
            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    byte red = pixel.R;
                    byte a = pixel.A;
                    byte green = pixel.G;
                    byte blue = pixel.B;

                    gimg.SetPixel(x, y, Color.FromArgb(a, 0, 0, blue));
                }
            }
            return gimg;
        }
        public double[] balanceHistogram(double[] b, double max, double min)
        {
            double[] histogram = new double[256];
            for (int i = 0; i < b.Length; i++)
            {
                histogram[i] = ((b[i] - min) / (max - min));
            }
            return histogram;
        }
        public double[] tinhHistogramGray(Bitmap anhxam)
        {
            double[] histogram = new double[256];
            for (int i = 0; i < anhxam.Width; i++)
            {
                for (int j = 0; j < anhxam.Height; j++)
                {
                    Color color = anhxam.GetPixel(i, j);
                    byte gray = color.G; // chuyen sang anh don sac thi xam = r = g = b
                    histogram[gray]++;
                }
            }
            return histogram;
        }
        public double[] tinhHistogramRed(Bitmap anhxam)
        {
            double[] histogram = new double[256];
            for (int i = 0; i < anhxam.Width; i++)
            {
                for (int j = 0; j < anhxam.Height; j++)
                {
                    Color color = anhxam.GetPixel(i, j);
                    byte gray = color.R; // chuyen sang anh don sac thi xam = r = g = b
                    histogram[gray]++;
                }
            }
            return histogram;
        }
        public double[] tinhHistogramGreen(Bitmap anhxam)
        {
            double[] histogram = new double[256];
            for (int i = 0; i < anhxam.Width; i++)
            {
                for (int j = 0; j < anhxam.Height; j++)
                {
                    Color color = anhxam.GetPixel(i, j);
                    byte gray = color.G; // chuyen sang anh don sac thi xam = r = g = b
                    histogram[gray]++;
                }
            }
            return histogram;
        }
        public double[] tinhHistogramBlue(Bitmap anhxam)
        {
            double[] histogram = new double[256];
            for (int i = 0; i < anhxam.Width; i++)
            {
                for (int j = 0; j < anhxam.Height; j++)
                {
                    Color color = anhxam.GetPixel(i, j);
                    byte gray = color.B; // chuyen sang anh don sac thi xam = r = g = b
                    histogram[gray]++;
                }
            }
            return histogram;
        }
        public int getIndexNewtonColor(Color c)
        {
            int idx = 0;
            //int[] Red = {255, 0, 0};
            //int[] Yellow = {255, 255, 0};
            //int[] Blue = { 0, 0, 255 };
            //int[] Green = { 0, 255, 0 };
            //int[] Orange = {255, 128, 0 };
            //int[] Purple = { 128, 0, 255 };

            int[,] color = { { 255, 0, 0 }, { 255, 255, 0 }, { 0, 0, 255 }, { 0, 255, 0 }, { 255, 128, 0 }, { 128, 0, 255 } };
            double disMin = Math.Sqrt((c.R - 255) * (c.R - 255) + (c.G - 0) * (c.G - 0) + (c.B - 0) * (c.B - 0));
            for (int i = 1; i < 6; i++)
            {
                double dis = Math.Sqrt((c.R - color[i, 0]) * (c.R - color[i, 0]) + (c.G - color[i, 1]) * (c.G - color[i, 1]) + (c.B - color[i, 2]) * (c.B - color[i, 2]));
                if (disMin > dis)
                {
                    disMin = dis;
                    idx = i;
                }
            }
            return idx;
        }
        public double[] tinhHistogramNewton(Bitmap bmp, MKeyPoint[] key, double maxx, double maxy)
        {
            double[] histogram = new double[6];
            for (int i = 0; i < maxx; i++)
            {
                for (int j = 0; j < maxy; j++)
                {
                    if (i == j)
                    {
                        Color color = bmp.GetPixel(i,j);

                        byte c = (byte)getIndexNewtonColor(color);

                        histogram[c]++;
                    }
                }
            }
            return histogram;
        }
        public int getIndexMPEG7Color(Color c)
        {
            int idx = 0;
            //int[] Black = { 0, 0, 0 };
            //int[] SeaGreen = { 0, 182, 0};
            //int[] LightGreen = { 0, 255, 170 };
            //int[] OliveGreen = { 36, 73, 0 };
            //int[] Aqua = { 36, 16, 170 };
            //int[] BrightGreen = { 36, 255, 0 };
            //int[] Blue = { 73, 36, 170 };
            //int[] Green = { 73, 146, 0};
            //int[] Turquoise = { 73, 219, 170 };
            //int[] Brown = { 109, 36, 0 };
            //int[] BlueGray = { 109, 109, 170 };
            //int[] Lime = { 109, 219, 0 };
            //int[] Lavenda = { 146, 0, 170 };
            //int[] Plum = { 146, 109, 0 };
            //int[] Teal = { 146, 182, 170 };
            //int[] DarkRed = { 182, 0, 0 };
            //int[] Magenta = { 182, 73, 170 };
            //int[] YellowGreen = { 182, 182, 0 };
            //int[] FlouroGreen = { 182, 255, 170 };
            //int[] Red = { 219, 73, 0 };
            //int[] Rose = { 219, 146, 170 };
            //int[] Yellow = { 219, 255, 0 };
            //int[] Pink = { 255, 36, 170 };
            //int[] Orange = { 255, 146, 0 };
            //int[] White = { 255, 255, 255 };

            int[,] color = {    { 0, 0, 0 }, { 0, 182, 0 }, { 0, 255, 170 }, { 36, 73, 0 },
                                { 36, 16, 170 }, { 36, 255, 0 }, { 73, 36, 170 }, { 73, 146, 0 },
                                { 73, 219, 170 }, { 109, 36, 0 }, { 109, 109, 170 }, { 109, 219, 0 },
                                { 146, 0, 170 }, { 146, 109, 0 }, { 146, 182, 170 }, { 182, 0, 0 },
                                { 182, 73, 170 }, { 182, 182, 0 }, { 182, 255, 170 }, { 219, 73, 0 },
                                { 219, 146, 170 }, { 219, 255, 0 }, { 255, 36, 170 }, { 255, 146, 0 }, { 255, 255, 255 } };
            double disMin = Math.Sqrt((c.R - 0) * (c.R - 0) + (c.G - 0) * (c.G - 0) + (c.B - 0) * (c.B - 0));
            for (int i = 1; i < 25; i++)
            {
                double dis = Math.Sqrt((c.R - color[i, 0]) * (c.R - color[i, 0]) + (c.G - color[i, 1]) * (c.G - color[i, 1]) + (c.B - color[i, 2]) * (c.B - color[i, 2]));
                if (disMin > dis)
                {
                    disMin = dis;
                    idx = i;
                }
            }
            return idx;
        }
        public double[] tinhHistogramMpeg7(Bitmap bmp, MKeyPoint[] key, double maxx, double maxy)
        {
            double[] histogram = new double[25];
            for (int i = 0; i < maxx; i++)
            {
                for (int j = 0; j < maxy; j++)
                {
                    if (i == j)
                    {
                        Color color = bmp.GetPixel((int)key[i].Point.X, (int)key[j].Point.Y);

                        byte c = (byte)getIndexMPEG7Color(color);

                        histogram[c]++;
                    }
                }
            }
            return histogram;
        }
        public Color getNewtonColor(Color c)
        {
            int idx = 0;
            //int[] Red = {255, 0, 0};
            //int[] Yellow = {255, 255, 0};
            //int[] Blue = { 0, 0, 255 };
            //int[] Green = { 0, 128, 0 };
            //int[] Orange = {255, 165, 0 };
            //int[] Purple = { 128, 0, 128 };
            int[,] color = { { 255, 0, 0 }, { 255, 255, 0 }, { 0, 0, 255 }, { 0, 255, 0 }, { 255, 128, 0 }, { 128, 0, 255 } };
            double disMin = Math.Sqrt((c.R - 255) * (c.R - 255) + (c.G - 0) * (c.G - 0) + (c.B - 0) * (c.B - 0));
            for (int i = 1; i < 6; i++)
            {
                double dis = Math.Sqrt((c.R - color[i, 0]) * (c.R - color[i, 0]) + (c.G - color[i, 1]) * (c.G - color[i, 1]) + (c.B - color[i, 2]) * (c.B - color[i, 2]));
                if (disMin > dis)
                {
                    disMin = dis;
                    idx = i;
                }
            }
            Color newColor = Color.FromArgb(255, color[idx, 0], color[idx, 1], color[idx, 2]);
            return newColor;
        }
        public Color getMPEG7Color(Color c)
        {
            int idx = 0;
            //int[] Black = { 0, 0, 0 };
            //int[] SeaGreen = { 0, 182, 0};
            //int[] LightGreen = { 0, 255, 170 };
            //int[] OliveGreen = { 36, 73, 0 };
            //int[] Aqua = { 36, 16, 170 };
            //int[] BrightGreen = { 36, 255, 0 };
            //int[] Blue = { 73, 36, 170 };
            //int[] Green = { 73, 146, 0};
            //int[] Turquoise = { 73, 219, 170 };
            //int[] Brown = { 109, 36, 0 };
            //int[] BlueGray = { 109, 109, 170 };
            //int[] Lime = { 109, 219, 0 };
            //int[] Lavenda = { 146, 0, 170 };
            //int[] Plum = { 146, 109, 0 };
            //int[] Teal = { 146, 182, 170 };
            //int[] DarkRed = { 182, 0, 0 };
            //int[] Magenta = { 182, 73, 170 };
            //int[] YellowGreen = { 182, 182, 0 };
            //int[] FlouroGreen = { 182, 255, 170 };
            //int[] Red = { 219, 73, 0 };
            //int[] Rose = { 219, 146, 170 };
            //int[] Yellow = { 219, 255, 0 };
            //int[] Pink = { 255, 36, 170 };
            //int[] Orange = { 255, 146, 0 };
            //int[] White = { 255, 255, 255 };

            int[,] color = {    { 0, 0, 0 }, { 0, 182, 0 }, { 0, 255, 170 }, { 36, 73, 0 },
                                { 36, 16, 170 }, { 36, 255, 0 }, { 73, 36, 170 }, { 73, 146, 0 },
                                { 73, 219, 170 }, { 109, 36, 0 }, { 109, 109, 170 }, { 109, 219, 0 },
                                { 146, 0, 170 }, { 146, 109, 0 }, { 146, 182, 170 }, { 182, 0, 0 },
                                { 182, 73, 170 }, { 182, 182, 0 }, { 182, 255, 170 }, { 219, 73, 0 },
                                { 219, 146, 170 }, { 219, 255, 0 }, { 255, 36, 170 }, { 255, 146, 0 }, { 255, 255, 255 } };
            double disMin = Math.Sqrt((c.R - 0) * (c.R - 0) + (c.G - 0) * (c.G - 0) + (c.B - 0) * (c.B - 0));
            for (int i = 1; i < 25; i++)
            {
                double dis = Math.Sqrt((c.R - color[i, 0]) * (c.R - color[i, 0]) + (c.G - color[i, 1]) * (c.G - color[i, 1]) + (c.B - color[i, 2]) * (c.B - color[i, 2]));
                if (disMin > dis)
                {
                    disMin = dis;
                    idx = i;
                }
            }

            Color newColor = Color.FromArgb(255, color[idx, 0], color[idx, 1], color[idx, 2]);
            return newColor;
        }
        public Bitmap convertNewton(Bitmap img)
        {
            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    gimg.SetPixel(x, y, getNewtonColor(pixel));
                }
            }
            return gimg;
        }
        public Bitmap convertMpeg7(Bitmap img)
        {
            Bitmap gimg = new Bitmap(img.Width, img.Height);
            for (int x = 0; x < gimg.Width; x++)
            {
                for (int y = 0; y < gimg.Height; y++)
                {
                    Color pixel = img.GetPixel(x, y);
                    gimg.SetPixel(x, y, getMPEG7Color(pixel));
                }
            }
            return gimg;
        }

    }






}