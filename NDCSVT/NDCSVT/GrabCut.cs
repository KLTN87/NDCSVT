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

namespace Grabcut
{
    public partial class GrabCut : Form
    {
        public GrabCut()
        {
            InitializeComponent();
        }
        Rectangle rect;
        Image<Bgr, byte> imgInput;
        Image<Bgr, byte> imgOutput;

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open();  
            lbCurrentBrightness.Text = ((float)trackBar2.Value/100).ToString();
            lbMinBrightness.Text = ((float)trackBar2.Minimum / 100).ToString();
            lbMaxBrightness.Text = ((float)trackBar2.Maximum / 100).ToString();


            lbMinConstast.Text = ((float)trackBar1.Minimum / 100).ToString();
            lbMaxContrast.Text = ((float)trackBar1.Maximum / 100).ToString();
            lbCurrentContrast.Text = ((float)trackBar1.Value / 100).ToString();
        }
        private void open()
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Title = "Select multiply images";
            opf.Multiselect = true;
            opf.Filter = "JPG|*.jpg|JPEG|*.jpeg|PNG|*.png";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                string []files = opf.FileNames;
                int x = 20;
                int y = 20;
                int Maxheight = -1;
                foreach(string filename in files)
                {
                    imgInput = new Image<Bgr, byte>(filename);
                    PictureBox pic = new PictureBox();
                    //pictureBox1.Image = imgInput.ToBitmap();
                    pic.Image = imgInput.ToBitmap();
                    //pic.Image = Image.FromFile(filename);
                    pic.Location = new Point(x, y);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    x += pic.Width + 10;
                    Maxheight = Math.Max(pic.Height, Maxheight);
                    if (x > this.ClientSize.Width-100)
                    {
                        x = 20;
                        y += Maxheight + 10;
                    }
                    this.listBox1.Items.Add(filename.ToString());
                    this.panel1.Controls.Add(pic);
                }
                
            }
            //pictureBox1.Image = imgInput.AsBitmap();
            
           
        }
        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            grayscale();
        }
        private void grayscale()
        {
            //var img = new Bitmap((Image)imageBox1.Image.ToString()).ToImage<Bgr, byte>();
            var img = new Bitmap(pictureBox1.Image).ToImage<Bgr, byte>();
            var gray = img.Convert<Gray, byte>();

            //var cut = img.GrabCut(rect, 1);
            pictureBox1.Image = gray.AsBitmap();
        }

        private void grabCutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grabcut();

            //phan lam xong

            //Matrix<double> bg = new Matrix<double>(1, 65);
            //bg.SetZero();
            //Matrix<double> fg = new Matrix<double>(1, 65);
            //fg.SetZero();
            //Image<Gray, byte> mask = new Image<Gray, byte>(img.Size);
            //Rectangle rect = new Rectangle(img.Cols / 10, 2, (int)((double)img.Width / (0.75)), img.Height);

            //CvInvoke.GrabCut(img, mask, rect,
            //   bg, fg, 5, Emgu.CV.CvEnum.GrabcutInitType.InitWithRect);
            //for (int x = 0; x < mask.Cols; x++)
            //{
            //    for (int y = 0; y < mask.Rows; y++)
            //    {
            //        if (mask[y, x].Intensity == new Gray(1).Intensity || mask[y, x].Intensity == new Gray(3).Intensity)
            //        {
            //            mask[y, x] = new Gray(1);
            //        }
            //        else
            //        {
            //            mask[y, x] = new Gray(0);
            //        }
            //    }
            //}
          
        }
        private void Grabcut()
        {
            try
            {
                var img = new Bitmap(pictureBox1.Image).ToImage<Bgr, byte>();
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
                pictureBox1.Image = img.ToBitmap();
            }

            catch
            {

            }


        }
        private void findContoursToolStripMenuItem_Click(object sender, EventArgs e)
        {
            findContours();
            //var img = new Bitmap(pictureBox1.Image).ToImage<Bgr, byte>();
            //Image<Gray, byte> imgOutput = imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255));
            //Emgu.CV.Util.VectorOfVectorOfPoint contours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            //Mat hier = new Mat();
            ////Image<Gray, byte> imgOut = new Image<Gray, byte>(imgInput.Width, imgInput.Height);
            //CvInvoke.FindContours(imgOutput, contours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            //CvInvoke.DrawContours(imgInput, contours, -1, new MCvScalar(0, 0, 255));
            //pictureBox2.Image = imgInput.ToBitmap();
            //
            

        }
        private void findContours()
        {
            Image<Gray, byte> imgOutput = imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255));
            Emgu.CV.Util.VectorOfVectorOfPoint countours = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Image<Gray, byte> imgOut = new Image<Gray, byte>(imgOutput.Width, imgOutput.Height, new Gray(0));
            Mat hier = new Mat();
            CvInvoke.FindContours(imgOutput, countours, hier, Emgu.CV.CvEnum.RetrType.External, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            CvInvoke.DrawContours(imgOut, countours, -1, new MCvScalar(255, 0, 0));
            pictureBox2.Image = imgOut.ToBitmap();
        }

        private void hOGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = new Bitmap(pictureBox1.Image).ToImage<Bgr, byte>();
            GetVector(img);
        }
        public Image<Bgr, Byte> Resize(Image<Bgr, Byte> im)
        {
            return im.Resize(64, 128, Emgu.CV.CvEnum.Inter.Linear);
            //return im.Resize(64, 128, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR);
        }
        public float[] GetVector(Image<Bgr, Byte> im)
        {
            HOGDescriptor hog = new HOGDescriptor();    // with defaults values
            Image<Bgr, Byte> imageOfInterest = Resize(im);
            System.Drawing.Point[] p = new System.Drawing.Point[imageOfInterest.Width * imageOfInterest.Height];
            int k = 0;
            for (int i = 0; i < imageOfInterest.Width; i++)
            {
                for (int j = 0; j < imageOfInterest.Height; j++)
                {
                    System.Drawing.Point p1 = new System.Drawing.Point(i, j);
                    p[k++] = p1;
                }
            }
            float[] result = hog.Compute(imageOfInterest, new System.Drawing.Size(16, 16), new System.Drawing.Size(0, 0),null);
            return result;
        }

        private void brightnessAndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Image = imgInput.ToBitmap();
                lbCurrentBrightness.Text = ((float)trackBar2.Value / 100).ToString();
                lbMinBrightness.Text = ((float)trackBar2.Minimum / 100).ToString();
                lbMaxBrightness.Text = ((float)trackBar2.Maximum / 100).ToString();
                //lbCurrentBrightness.Text = trackBar2.Value.ToString();
                //lbMinBrightness.Text = trackBar2.Minimum.ToString();
                //lbMaxBrightness.Text = trackBar2.Maximum.ToString();


                lbMinConstast.Text = ((float)trackBar1.Minimum/100).ToString();
                lbMaxContrast.Text = ((float)trackBar1.Maximum/100).ToString();
                lbCurrentContrast.Text = ((float)trackBar1.Value / 100).ToString();
            }
            catch
            {

            }
        }
        private void BrightnessContrastAdjust()
        {
            try
            {
                //lbCurrentBrightness.Text = ((float)trackBar2.Value / 100).ToString();
                lbCurrentBrightness.Text = trackBar2.Value.ToString();
                lbCurrentContrast.Text = ((float)trackBar1.Value / 100).ToString();
                imgOutput= imgInput.Mul(double.Parse(lbCurrentContrast.Text) + trackBar2.Value);
                pictureBox1.Image = imgOutput.ToBitmap();
            }
            catch
            {

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            try
            {
                BrightnessContrastAdjust();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            try
            {
                BrightnessContrastAdjust();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
