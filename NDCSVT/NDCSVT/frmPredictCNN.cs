﻿using System;
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
                }


                pictureBox1.Image = imgInput.ToBitmap();

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
            string stringKq = getStringPredict(kq);
            richTextBox_KQ.Text = stringKq;

        }

        public static string getStringPredict(int num)
        {
            if (num < 10) //0-9
            {
                return num.ToString();
            }

            string[] arr = { "0","1", "2", "3", "4", "5", "6", "7", "8", "9",
                "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l",
                "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z",
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M",
                "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            string kq = arr[num];
            return kq;
        }


        public int startPredict(string pathModel, Image<Bgr, byte> imgInput)
        {
            Net model = DnnInvoke.ReadNetFromTensorflow(pathModel);
            Bitmap bm = imgInput.ToBitmap();

            var img = bm.ToImage<Gray, byte>()
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
    }
}
