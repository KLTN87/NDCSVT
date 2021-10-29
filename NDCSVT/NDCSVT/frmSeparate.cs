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


namespace Grabcut
{
    public partial class frmTesst : Form
    {
        public frmTesst()
        {
            InitializeComponent();
        }
        string inputfile;
        Image<Bgr, byte> imgInput;


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            separateImages(imgInput);
            int countA = 0;

            foreach(var tr in listImages)
            {
                countA++;

                int num = rnd.Next(1, 10000);

                CvInvoke.Imshow("image " + num.ToString(), tr);


            }
            MessageBox.Show(countA.ToString());




        }


        List<Image<Bgr, Byte>> listImages = new List<Image<Bgr, byte>>();

        public void separateImages(Image<Bgr, Byte> img)
        {


            while (true)
            {


                try
                {
                    var cropNgoai = frmGetFeature.cropNoUseBlackAreaImg(img);
                    var crop1 = cropOneNotBlack(cropNgoai);

                    listImages.Add(crop1[0]);
                    img = crop1[1];

                }
                catch(Exception)
                {
                    return;
                }
            }


        }



        public static List<Image<Bgr, Byte>> cropOneNotBlack(Image<Bgr, Byte> img)
        {

            List<Image<Bgr, Byte>> tempImg = new List<Image<Bgr, byte>>();

            int toaDoX = getBlackAreaX(img);

            try
            {
                Mat ROI = new Mat(img.Mat, new Range(0, img.Height), new Range(0, toaDoX)).Clone();
                Image<Bgr, Byte> temp0 = ROI.ToImage<Bgr, Byte>();

                Mat ROI1 = new Mat(img.Mat, new Range(0, img.Height), new Range(toaDoX, img.Width)).Clone();
                Image<Bgr, Byte> temp1 = ROI1.ToImage<Bgr, Byte>();
                tempImg.Add(temp0);
                tempImg.Add(temp1);


            }
            catch { }

            return tempImg;

        }


        public static int getBlackAreaX(Image<Bgr, Byte> imgBlack)
        {
            Bitmap img = imgBlack.AsBitmap();
            Color pixelColor;

            try
            {
                for (int x = 0; x < img.Width; x++)
                {
                    int count = 0;
                    for (int y = 0; y < img.Height; y++)
                    {
                        pixelColor = img.GetPixel(x, y);
                        if (pixelColor.R == 0 && pixelColor.G == 0 && pixelColor.B == 0)
                        {
                            count++;
                        }

                        if (count == img.Height)
                        {

                            return x;
                        }

                    }
                }
            }
            catch
            {
            }
            return 0;

        }





        private void button2_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = @"PNG|*.png" })
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var cropNgoai = frmGetFeature.cropNoUseBlackAreaImg(imgInput);



                    cropNgoai.Save(saveFileDialog.FileName);
                }
            }
        }
    }






}