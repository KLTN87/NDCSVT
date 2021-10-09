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
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace Grabcut
{
    public partial class FormCropCC : Form
    {

        Image<Bgr, byte> imgInput;
        Image<Gray, byte> CC;

        public FormCropCC()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                imgInput = new Image<Bgr, byte>(dialog.FileName);
                pictureBox1.Image = imgInput.AsBitmap();
            }
        }



        private void processToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (imgInput == null)
            {
                return;
            }

            try
            {
                imgInput = GrabcutImg(IResize(imgInput, 128, 128));




                var temp = imgInput.Convert<Gray, byte>().ThresholdBinary(new Gray(100), new Gray(255))
                                    .Dilate(1).Erode(1);
                Mat labels = new Mat();
                int nLabels = CvInvoke.ConnectedComponents(temp, labels);
                CC = labels.ToImage<Gray, byte>();
                pictureBox2.Image = temp.AsBitmap();

            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
            }
        }



        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = imgInput.Convert<Gray, byte>();
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

                //CvInvoke.Imshow("image 1", img);

                return img;
            }

            catch
            {

            }
            return img;

        }

        private void cropToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var temp = CC;
            imgInput = GrabcutImg(IResize(imgInput, 128, 128));


            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            Mat m = new Mat();

            CvInvoke.FindContours(temp, contours, m, Emgu.CV.CvEnum.RetrType.External,
                Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);

            if (contours.Size > 0)
            {
                Rectangle bbox = CvInvoke.BoundingRectangle(contours[0]);

                imgInput.ROI = bbox;
                var img = imgInput.Copy();

                imgInput.ROI = Rectangle.Empty;

                //CvInvoke.Imshow("image 2", img);

                pictureBox2.Image = img.AsBitmap();
            }
        }

        private Image<Bgr, Byte> IResize(Image<Bgr, Byte> im, int w, int h)
        {
            return im.Resize(w, h, Emgu.CV.CvEnum.Inter.Linear);
        }


    }
}