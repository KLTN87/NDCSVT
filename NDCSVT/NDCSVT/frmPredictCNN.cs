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
using TensorFlow;
using System.Collections;
using Emgu.CV.Dnn;

namespace Grabcut
{
    public partial class frmPredictCNN : Form
    {
        public frmPredictCNN()
        {
            InitializeComponent();
        }

        string inputfile;
        Image<Bgr, byte> imgInput;
        List<String> vectorList = new List<string>();
        string pathModel = @"../../../pb/CNN-mnist.pb";


        private void loadModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                //dialog.Filter = "PB Files (*.pb;)|*.pb;";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathModel = dialog.FileName;
                    MessageBox.Show("Model loaded");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog opf = new OpenFileDialog();
                opf.Title = "Select images";
                opf.Filter = "Image Files | *.jpg; *.jpeg; *.png";

                if (opf.ShowDialog() == DialogResult.OK)
                {
                    inputfile = opf.FileName;
                    imgInput = new Image<Bgr, byte>(inputfile);
                    pictureBox1.Image = imgInput.ToBitmap();
                }




            }
            catch
            {
                //không mở ảnh

            }
        }

        private void predictToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Please open images");
                return;
            }

            if (pathModel == null)
            {
                MessageBox.Show("Please load model");
                return;
            }


            int kq = startPredict(pathModel, imgInput);
            string stringKq = frmPredict.getStringPredict(kq);
            richTextBox_KQ.Text = stringKq;

        }


        public int startPredict(string pathModel, Image<Bgr, byte> imgInput)
        {
            Net model = DnnInvoke.ReadNetFromTensorflow(pathModel);
            var img1 = frmGetFeature.IResize(imgInput, 128, 128);
            img1 = GrabcutImg(img1);

            pictureBox1.Image = img1.ToBitmap();
            var img = img1.Convert<Gray, Byte>()
                                .SmoothGaussian(3)
                                .Resize(28, 28, Emgu.CV.CvEnum.Inter.Cubic)
                                .Mul(1 / 255.0f);


            var input = DnnInvoke.BlobFromImage(img);
            model.SetInput(input);
            var output = model.Forward();

            float[] array = new float[100];
            output.CopyTo(array);

            var prob = SoftMax(array);
            int index = Array.IndexOf(prob, prob.Max());
            return index;
        }

        private float[] SoftMax(float[] arr)
        {
            var exp = (from a in arr
                       select (float)Math.Exp(a))
                      .ToArray();
            var sum = exp.Sum();
            return exp.Select(x => x / sum).ToArray();
        }

        private void frmPredictCNN_Load(object sender, EventArgs e)
        {
            richTextBox_KQ.SelectAll();
            richTextBox_KQ.SelectionAlignment = HorizontalAlignment.Center;
        }

        public static Image<Bgr, Byte> GrabcutImg(Image<Bgr, Byte> img)
        {
            try
            {
                Matrix<double> bg = new Matrix<double>(1, 65);
                bg.SetZero();
                Matrix<double> fg = new Matrix<double>(1, 65);
                fg.SetZero();
                Image<Gray, byte> mask = new Image<Gray, byte>(img.Size);
                Rectangle rect = new Rectangle(img.Cols / 10, 2, (int)((double)img.Width / (0.75)), img.Height);
                CvInvoke.GrabCut(img, mask, rect,
                   bg, fg, 5, Emgu.CV.CvEnum.GrabcutInitType.InitWithRect);
                Image<Gray, byte> mask2 = new Image<Gray, byte>(img.Size);

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




        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
