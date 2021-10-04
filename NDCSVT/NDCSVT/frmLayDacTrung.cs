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
using Emgu.CV.CvEnum;
using Emgu.CV.UI;
using Emgu.CV.Structure;
using Emgu.CV.Util;

using Emgu.Util;
using Emgu.CV.Features2D;
using Emgu.CV.XFeatures2D;

using Emgu.CV.ML;

namespace NDCSVT
{
    public partial class frmLayDacTrung : Form
    {
        public frmLayDacTrung()
        {
            InitializeComponent();
        }

        Image<Bgr, byte> imgInput; 


        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
