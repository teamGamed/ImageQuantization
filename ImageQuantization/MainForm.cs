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
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value;
            ConstructGraph.diffcolors(ImageMatrix);
            ConstructGraph.CalcDist();
            MST.getMST();
            ExtractClusters.extractClusters(maskSize);
            ExtractClusters.getClustersColors();
            ImageOperations.DisplayImage(ColorMapping.NewColors(ImageMatrix), pictureBox2);
            txtDiffColors.Text = Data.colorsNum.ToString();
            printOutput();
        }
        private void printOutput()
        {
            Console.WriteLine(" Case ::  " + testName);
            double sum = 0;
            for(int i = 0; i < Data.MSTList.Length; i++)
            {
                for (int j = 0; j < Data.MSTList[i].Count; j++)
                {
                    sum += Data.distances[i,Data.MSTList[i][j]];
                } 
            }
            MSTSum.Text = Math.Round(sum, 3).ToString();
            Console.WriteLine(" MSTSUM ::  "+Math.Round(sum , 3));
        }
    }
}