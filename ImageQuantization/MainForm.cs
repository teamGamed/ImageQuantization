using System;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;
        static string  testName;
        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                testName = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            Data.clear();
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            ConstructGraph.Diffcolors(ImageMatrix);
            MST.getMST();
            //ConstructGraph.CalcDist();
            txtGaussSigma.Text = ExtractClusters.getK().ToString();
            ExtractClusters.extractClusters(maskSize);
            ExtractClusters.getClustersColors();
            // outPut
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            timeTxt.Text = elapsedMs.ToString();
            MSTSum.Text = Math.Round(Data.sum, 3).ToString();
            TimeM.Text = ((double)elapsedMs/(60*1000)).ToString();
            txtDiffColors.Text = Data.colorsNum.ToString();
            ImageOperations.DisplayImage(ColorMapping.NewColors(ImageMatrix), pictureBox2);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            
        }

        private void label11_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
            
        }
    }
}