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
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using Emgu.CV.XFeatures2D;
using Emgu.CV.CvEnum;
using Emgu.Util;
using Emgu.CV.UI;
using Emgu.CV.Util;

namespace Grabcut
{
    public partial class frmSIFT : Form
    {
        Image<Bgr, byte> imgInput;
        public frmSIFT()
        {
            InitializeComponent();
        }

        private void loadImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgInput = new Image<Bgr, byte>(ofd.FileName);
                pictureBox1.Image = imgInput.AsBitmap();
            }
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
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";

            Bitmap a = convertGrayScale(imgInput.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();

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
            string temp = " ";
            for (int i = 0; i < c.Length; i++)
            {
                temp = temp + " " + c[i];
            }
            richTextBox1.Text = temp;
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

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";

            Bitmap a = convertRed(imgInput.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(255, 0, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();

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
            string temp = " ";
            for (int i = 0; i < c.Length; i++)
            {
                temp = temp + " " + c[i];
            }
            richTextBox1.Text = temp;
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";

            Bitmap a = convertGreen(imgInput.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();

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

            string temp = " ";

            for (int i = 0; i < c.Length; i++)
            {
                temp = temp + " " + c[i];
            }
            richTextBox1.Text = temp;
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";

            Bitmap a = convertBlue(imgInput.ToBitmap());

            Mat src1 = a.ToMat();

            SIFT sift = new SIFT();
            MKeyPoint[] mKeyPoints = sift.Detect(src1, null);

            Mat sift_feature = new Mat();

            VectorOfKeyPoint vkPoint = new VectorOfKeyPoint(mKeyPoints);

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(255, 0, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();

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
            string temp = " ";
            for (int i = 0; i < c.Length; i++)
            {
                temp = temp + " " + c[i];
            }
            richTextBox1.Text = temp;
        }

        public int getIndexNewtonColor(Color c)
        {
            int idx = 0;
            //int[] Red = {255, 0, 0};
            //int[] Yellow = {255, 255, 0};
            //int[] Blue = { 0, 0, 255 };
            //int[] Green = { 0, 128, 0 };
            //int[] Orange = {255, 165, 0 };
            //int[] Purple = { 128, 0, 128 };
            int[,] color = { { 255, 0, 0 }, { 255, 255, 0 }, { 0, 0, 255 }, { 0, 128, 0 }, { 255, 165, 0 }, { 128, 0, 128 } };
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
                        Color color = bmp.GetPixel((int)key[i].Point.X, (int)key[j].Point.Y);

                        byte c = (byte)getIndexNewtonColor(color);

                        histogram[c]++;
                    }
                }
            }
            return histogram;
        }

        private void newtonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";
            Bitmap a = (imgInput.ToBitmap());

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
            string ghi = " ";
            for (int i = 0; i < 6; i++)
            {
                ghi = ghi + c[i] + " ";
            }
            //foreach (double value in c)
            //{
            //    ghi = ghi + value + "  ";
            //}
            richTextBox1.Text = ghi;

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();
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

        private void mpeg7ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = " ";
            Bitmap a = (imgInput.ToBitmap());

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
            string ghi = " ";
            for (int i = 0; i < 25; i++)
            {
                ghi = ghi + c[i] + " ";
            }
            //foreach (double value in c)
            //{
            //    ghi = ghi + value + "  ";
            //}
            richTextBox1.Text = ghi;

            Features2DToolbox.DrawKeypoints(src1, vkPoint, sift_feature, new Bgr(0, 255, 0), Features2DToolbox.KeypointDrawType.Default);
            pictureBox2.Image = sift_feature.ToBitmap();
        }
    }
}
