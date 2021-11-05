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
using Numpy;
using TensorFlow;
using System.Collections;

namespace Grabcut
{
    public partial class frmPredict : Form
    {
        string inputfile;
        Image<Bgr, byte> imgInput;
        List<String> vectorList = new List<string>();
        string pathModel = @"../../../pb/save.pb";


        public frmPredict()
        {
            InitializeComponent();
            richTextBox_KQ.SelectAll();
            richTextBox_KQ.SelectionAlignment = HorizontalAlignment.Center;
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

            richTextBox1.Clear();
            richTextBox_KQ.Clear();

            Image<Bgr, byte> tempimg = new Image<Bgr, byte>(inputfile);

            var chuanhoadactrung = getFeaturesFormImage(tempimg);
            List<String> stringdactrung = frmGetFeature.convertDoubleArrayToStringArray(chuanhoadactrung);

            string textDT = string.Join(" ", stringdactrung);
            vectorList.Add(textDT);
            richTextBox1.Text = textDT;
            //print10FirstVector(vectorList);

            int kq = getPredictFormModel(chuanhoadactrung);
            string stringKq = getStringPredict(kq);

            richTextBox_KQ.Text = stringKq;


        }

        public double[] getFeaturesFormImage(Image<Bgr, byte> tempimg)
        {

            int tempimgwsize = int.Parse(tempimg.Size.Width.ToString());
            tempimg = frmGetFeature.IResize(tempimg, 128, 128);
            var imggrabcut = frmGetFeature.GrabcutImg(tempimg);
            pictureBox1.Image = imggrabcut.ToBitmap();
            var tempimg2 = frmGetFeature.IResize(imggrabcut, 128, 128);
            var featureSIFT = getSIFTGray(tempimg2);
            var featureHOG = frmGetFeature.getHOGFeature(tempimg2, 2304);
            var gopdactrung = frmGetFeature.concatDoubleArray(featureSIFT, featureHOG);
            var chuanhoadactrung = frmGetFeature.normalizeDoubleArray(gopdactrung);
            return chuanhoadactrung;
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
        private void print10FirstVector(List<String> dsVectoc)
        {
            int dem = 0;
            richTextBox1.Clear();
            foreach (String vec in vectorList)
            {

                dem++;
                richTextBox1.Text = richTextBox1.Text + "<vector>" + vec + "</vector>";

                if (dem >= 10)
                    break;
            }

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


        private void loadModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "PB Files (*.pb;)|*.pb;";
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
            catch {}

            return -1;
        }



        private void frmPredict_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new frmMenu();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
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

    }
}
