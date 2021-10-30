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
using TensorFlow;
using System.IO;
using System.Collections;

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
        List<String> vectorList = new List<string>();
        string pathModel = @"../../../pb/save.pb";


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


                //Image<Bgr, byte> tempimg = new Image<Bgr, byte>(tr);

                var chuanhoadactrung = getFeaturesFormImage(tr);
                List<String> stringdactrung = frmGetFeature.convertDoubleArrayToStringArray(chuanhoadactrung);

                string textDT = string.Join(" ", stringdactrung);
                vectorList.Add(textDT);
                //print10FirstVector(vectorList);
                int a = getPredictFormModel(chuanhoadactrung);
                int kq = getPredictFormModel(chuanhoadactrung);
                string stringKq = " ";
                stringKq = stringKq + getStringPredict(kq);

                richTextBox3.Text = stringKq;

            }
            MessageBox.Show(countA.ToString());




        }

        private string getStringPredict(int num)
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
        public float[] toFloatArray(double[] arr)
        {
            if (arr == null) return null;
            int n = arr.Length;
            float[] ret = new float[n];
            for (int i = 0; i < n; i++)
            {
                ret[i] = (float)arr[i];
            }
            return ret;
        }
        public int getPredictFormModel(double[] vec)
        {
            float[] vector = toFloatArray(vec);
            List<float[]> temptInput = new List<float[]>();
            temptInput.Add(vector);
            using (var graph = new TFGraph())
            {

                try
                {

                    var file = File.ReadAllBytes(pathModel);
                    graph.Import(file);
                    //List<TFOperation> op_list = new List<TFOperation>(graph.GetEnumerator());

                    var tf = new TFSession(graph);
                    var runner = tf.GetRunner();
                    if (vector != null)
                    {
                        runner.AddInput(graph["x"][0], temptInput.ToArray());
                        runner.Fetch(graph["Identity"][0]);
                        var output = runner.Run();
                        TFTensor result = output[0];
                        var resultValue = result.GetValue();
                        var listresultValue = resultValue as IEnumerable;
                        int index = getMaxIndexIEnumerable2D(listresultValue);
                        return index;
                    }
                }
                catch { }
            }
            return -1;
        }
        private int getMaxIndexIEnumerable2D(IEnumerable myList)
        {
            try
            {
                List<float> temp1D = new List<float>();
                if (myList != null)
                {
                    foreach (var element in myList)
                    {
                        temp1D.Add((float)element);

                    }
                }

                int maxIndex = temp1D.IndexOf(temp1D.Max());
                return maxIndex;
            }
            catch { }

            return -1;
        }
        public double[] getFeaturesFormImage(Image<Bgr, byte> tempimg)
        {
            int tempimgwsize = int.Parse(tempimg.Size.Width.ToString());
            tempimg = frmGetFeature.IResize(tempimg, 128, 128);
            var imggrabcut = frmGetFeature.GrabcutImg(tempimg);
            var tempimg2 = frmGetFeature.IResize(imggrabcut, 128, 128);
            var featureSIFT = getSIFTGray(tempimg2);
            var featureHOG = frmGetFeature.getHOGFeature(tempimg2, 2304);
            var gopdactrung = frmGetFeature.concatDoubleArray(featureSIFT, featureHOG);
            var chuanhoadactrung = frmGetFeature.normalizeDoubleArray(gopdactrung);
            return chuanhoadactrung;
        }

        private double[] getSIFTGray(Image<Bgr, Byte> im)
        {

            Bitmap a = frmGetFeature.convertGrayScale(im.ToBitmap());

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


            double[] b = frmGetFeature.tinhHistogramGray(a, keyPoints, maxx, maxy);

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
            double[] c = frmGetFeature.balanceHistogram(b, mx, mn);
            return c;
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