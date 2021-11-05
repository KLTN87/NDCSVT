using System;
using System.Windows.Forms;

namespace Grabcut
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new frmMenu());
=======
            Application.Run(new frmPredictCNN());
>>>>>>> 2a04506cf976f2622eee29d6cc91152f532c13ae
        }
    }
}