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
    public partial class HOG : Form
    {
        public HOG()
        {
            InitializeComponent();
        }

        Image<Bgr, byte> imgInput;


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                imgInput = new Image<Bgr, byte>(ofd.FileName);

                imageBox1.Image = imgInput;
                richTextBox1.Clear();
            }

        }

        private void hOG36FeatureValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showHOGFeature(imgInput, 36);
        }


        private void printVector(float[] hog)
        {
            string arrtext = string.Join(" ", hog);
            int arrnum = hog.Length;
            button2.Text = arrnum.ToString();
            richTextBox1.Text = arrtext;
        }

        private void showHOGFeature(Image<Bgr, Byte> im, int numberValues)
        {

            printVector(getHOGFeature(im, numberValues));
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

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var img = imgInput;
            var gray = img.Convert<Gray, byte>();


            var edges = img;
            CvInvoke.Canny(img, edges, 100, 200);

            //var  vertical_sum = np.sum(edges, axis = 0)


            //vertical_sum = vertical_sum != 0
            //changes = np.logical_xor(vertical_sum[1:], vertical_sum[:-1])
            //change_pts = np.nonzero(changes)[0]

            //plt.imshow(img)
            //for change in change_pts:
            //    plt.axvline(change + 1)
            //plt.show()
        }
    }
}
