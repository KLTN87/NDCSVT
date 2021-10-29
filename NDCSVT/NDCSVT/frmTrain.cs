using Keras.Layers;
using Keras.Models;
using Keras.Optimizers;
using Keras.Utils;
using Numpy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Grabcut
{
    public partial class frmTrain : Form
    {
        private string pathTextTraining = null;
        private string pathTextTesting = null;

        public frmTrain()
        {
            InitializeComponent();

            textBox1.Text = "10";
        }

        private void openTextToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextTraining = dialog.FileName;
                    MessageBox.Show("Text Training loaded");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void openTextTestingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    pathTextTesting = dialog.FileName;
                    MessageBox.Show("Text Testing loaded");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void BuildAndTrain(NDarray train_x, NDarray train_y, NDarray test_x, NDarray test_y, int nb_classes)
        {
            //Model to hold the neural network architecture which in this case is WaveNet
            var model = new Sequential();
            // Starts with embedding layer

            model.Add(new Flatten());
            model.Add(new Dense(256));
            model.Add(new Activation("softmax"));
            model.Add(new Dense(nb_classes));
            model.Add(new Activation("softmax"));

            // Compile with Adam optimizer
            model.Compile(optimizer: new Adam(), loss: "categorical_crossentropy", metrics: new string[] { "accuracy" });

            model.Fit(train_x, train_y, batch_size: 128, epochs: 20, verbose: 1);

            //Score the model for performance

            var score = model.Evaluate(test_x, test_y, batch_size: 10, verbose: 0);
            Console.WriteLine("Test loss:" + score[0]);
            Console.WriteLine("Test accuracy:" + score[1]);

            // Save the final trained model which we are going to use for prediction
            model.Save("last_epoch.h5");
            model.Save("save");

            model.Summary();

            MessageBox.Show("Test loss: " + score[0] + "\nTest accuracy: " + score[1]);
        }

        public int[] txt2ArrLabel(string path)
        {
            List<int> tempArrIint = new List<int>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                if (line.Contains("<label>") == true)
                {
                    int tempint = int.Parse(line.Trim().Replace("<label>", "").Replace("</label>", "").Trim());
                    tempArrIint.Add(tempint);
                }
            }
            return tempArrIint.ToArray();
        }

        public float[,] txt2ArrVector(string path)
        {
            List<float[]> tempArrVector = new List<float[]>();
            foreach (string line in System.IO.File.ReadLines(path))
            {
                List<float> tempfloatarr = new List<float>();
                if (line.Contains("<vector>") == true)
                {
                    String tempvector = line.Replace("<vector>", "").Replace("</vector>", "");
                    var fooArray = tempvector.Split(' ');
                    foreach (string tu in fooArray)
                    {
                        float tt = float.Parse(tu);
                        tempfloatarr.Add(tt);
                    }
                    tempArrVector.Add(tempfloatarr.ToArray());
                }
            }
            var tempArr = CreateRectangularArray(tempArrVector.ToArray());
            return tempArr;
        }

        //vì không thể convert mảng răng cưa sang numpy, nên phải chuyển sang mảng 2 chiều
        public static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            int minorLength = arrays[0].Length;
            T[,] ret = new T[arrays.Count, minorLength];
            for (int i = 0; i < arrays.Count; i++)
            {
                var array = arrays[i];
                if (array.Length != minorLength)
                {
                    throw new ArgumentException
                        ("All arrays must be the same length");
                }
                for (int j = 0; j < minorLength; j++)
                {
                    ret[i, j] = array[j];
                }
            }
            return ret;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string pathDicrectoryDebug = Path.GetDirectoryName(Application.ExecutablePath);
            string pathModelTrained = pathDicrectoryDebug + "\\last_epoch.h5";
            string pathSaveFile = SaveModelFile();

            if (pathSaveFile == null)
            {
                return;
            }

            try
            {
                File.Copy(pathModelTrained, pathSaveFile);
                MessageBox.Show("Saved");
            }
            catch
            {
                MessageBox.Show("Error! No Model Trained");
            }
        }

        private string SaveModelFile()
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "H5|*.h5";
            save.FilterIndex = 1;

            save.FileName = "Model.h5";

            if (save.ShowDialog() == DialogResult.OK)
            {
                string path = save.FileName;
                return path;
            }
            return null;
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pathTextTraining == null)
            {
                MessageBox.Show("Please load Text file");
                return;
            }
            if (pathTextTesting == null)
            {
                MessageBox.Show("Please load Text file");
                return;
            }

            var labelTrain = txt2ArrLabel(pathTextTraining);
            var vectorTrain = txt2ArrVector(pathTextTraining);
            var trainY = np.array(labelTrain);
            var trainX = np.array(vectorTrain);
            var trainY_one_hot = Util.ToCategorical(trainY);

            var labelTest = txt2ArrLabel(pathTextTesting);
            var vectorTest = txt2ArrVector(pathTextTesting);
            var testY = np.array(labelTest);
            var testX = np.array(vectorTest);
            var testY_one_hot = Util.ToCategorical(testY);

            int nb_classes = int.Parse(textBox1.Text);

            try
            {
                BuildAndTrain(trainX, trainY_one_hot, testX, testY_one_hot, nb_classes);
            }
            catch
            {
                MessageBox.Show("Something error!");
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form2 = new frmMenu();
            form2.Closed += (s, args) => this.Close();
            form2.Show();
        }

        private void frmTrain_Load(object sender, EventArgs e)
        {
        }
    }
}