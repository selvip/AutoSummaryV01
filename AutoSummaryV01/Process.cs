using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.Shape;
using Emgu.Util;
using Emgu.CV.Features2D;
using Emgu.CV.Cuda;

namespace AutoSummaryV01
{
    public partial class Process : Form
    {
        private Image<Gray, float> tampilkan = null;
        private Mat src, cvtBlack, cvtBlackST;
        private StreamWriter SW;
        // src = source;
        // cvtBlack = only black in this mat;
        // cvtBlackST = shi tomasi applied on this black mat;
        private Mat cvtBlue, cvtBlueDT;
        //cvtBlue = only blue in this mat;
        // cvtBlueDT = detect value on this mat(grayscale);

        private int maxCorner = 1000;
        private int TotalRow = GlobalClass.NumOfQues;
        private int TotalCol = GlobalClass.NumOfAns;
        private float[] Col;
        private float[] Row;
        private int[][] answer;
        private string wl1;

        public Process()
        {
            InitializeComponent();
        }

        private void button_p_s_Click(object sender, EventArgs e)
        {
            button_p_s.Enabled = false;
            
            // write first header for the file asum
            string dir = GlobalClass.ASUMDir;
            if(dir != null & dir != "")
            {
                wl1 = "";
                SW = new StreamWriter(dir);
                SW.WriteLine((TotalRow - 1).ToString() + (TotalCol - 1).ToString());

                foreach (string filedir in GlobalClass.FileDir)
                {
                    textBox1.Text = textBox1.Text + Environment.NewLine + filedir;
                    checkScannedOne(filedir);

                    gathAllAnswer();
                    SW.WriteLine(wl1);
                    wl1 = "";
                }
                SW.Dispose();
            }
            else
            {
                MessageBox.Show("Please Set Basic Form first.");
                this.Close();
            }
            
        }
        
        private void gathAllAnswer()
        {
            for (int m = 0; m <= answer.Length - 1; m++)
            {
                wl1 = wl1 + (m).ToString();
                for (int n = 0; n <= answer[0].Length - 1; n++)
                {
                    wl1 = wl1 + answer[m][n].ToString();
                }
            }
        }

        //--------------------------------------------------------------------------------------------
        private void addToArrayCol(ref float[] arrayA, VectorOfKeyPoint corners, int l)
        {
            float p, j;
            arrayA = new float[l + 1];
            // int l >> input total column
            p = corners.Size;
            int m = -1;



            for (int i = 0; i <= p - 1; i++)
            {
                j = corners[i].Point.X;
                if (checkValueArray(arrayA, j, l + 1) == true)
                {
                    m = m + 1;
                    arrayA[m] = j;
                    //MessageBox.Show((j).ToString());
                }
            }

            Array.Sort(arrayA);
        }

        //------------------------------------------------------------------------------------------
        private void addToArrayRow(ref float[] arrayA, VectorOfKeyPoint corners, int l)
        {
            float p, j;
            arrayA = new float[l + 1];
            // int l >> input total column
            p = corners.Size;
            int m = -1;


            for (int i = 0; i <= p - 1; i++)
            {
                j = corners[i].Point.Y;
                if (checkValueArray(arrayA, j, l + 1) == true)
                {
                    m = m + 1;
                    arrayA[m] = j;
                    //MessageBox.Show((j).ToString());
                }
            }

            Array.Sort(arrayA);
        }

        //------------------------------------------------------------------------------------------
        private void checkScannedOne(string fileDir)
        {
            try
            {
                //initialization
                //setting up src, cvtBlack
                initImage(fileDir); //(1)
                progressBar1.Value = 10;

                //start detecting color (black)
                if(GlobalClass.BlueCross == true)
                {
                    detectColorBlack(src, cvtBlack); //(2)
                }
                else
                {
                    detectColorBlue(src, cvtBlack); //(2)
                }
                progressBar1.Value = 20;

                //convert to gray
                cvtBlackST = new Mat();
                convertToGrayscale(cvtBlack, cvtBlackST); //(3)
                imageBox1.Image = cvtBlackST;

                initAnswerArray(); //(5)
                progressBar1.Value = 30;

                //start detecting corner (answer)
                VectorOfKeyPoint sudut = new VectorOfKeyPoint();
                try
                {
                    GFTT(sudut, cvtBlackST, src, 0.01); //(4)
                    progressBar1.Value = 30;
                    //imageBox1.Image = tampilkan;
                    MessageBox.Show("Number of corners = " + sudut.Size);
                    imageBox1.Image = tampilkan;
                    if (sudut.Size == ((TotalCol + 1) * (TotalRow + 1)))
                    {
                        addToArrayCol(ref Col, sudut, TotalCol);
                       // MessageBox.Show("Ada");
                        addToArrayRow(ref Row, sudut, TotalRow);
                       // MessageBox.Show("Ada");
                    }

                    progressBar1.Value = 50;

                    //detect blue color
                    cvtBlue = new Mat();
                    if (GlobalClass.BlueCross == false)
                    {
                        detectColorBlack(src, cvtBlue); 
                    }
                    else
                    {
                        detectColorBlue(src, cvtBlue); 
                    } //(6)
                    progressBar1.Value = 60;

                    //convert mat to grayscale
                    cvtBlueDT = new Mat();
                    convertToGrayscale(cvtBlue, cvtBlueDT); //(7)
                    progressBar1.Value = 70;

                    VectorOfKeyPoint sudut2 = new VectorOfKeyPoint();
                    try
                    {
                        GFTT(sudut2, cvtBlueDT, src, 0.50); //(8)
                        progressBar1.Value = 80;
                        MessageBox.Show("Number of corners = " + sudut2.Size);
                        imageBox1.Image = tampilkan;

                        // set answer to 2d-array
                        int kol, row, k;
                        kol = 0;
                        row = 0;
                        progressBar1.Value = 90;
                        for (k = 0; k <= sudut2.Size - 1; k++)
                        {
                            SetW_Ans(Col, sudut2[k].Point.X, ref kol);
                            //MessageBox.Show(kol.ToString());
                            SetH_Ans(Row, sudut2[k].Point.Y, ref row);
                            //MessageBox.Show(row.ToString());

                            //get entry
                            answer[row][kol] = 1;
                        } //(9)
                        
                        progressBar1.Value = 100;
                    }
                    catch
                    {
                        MessageBox.Show("Failed to find corner blue. Please check " + fileDir +". This form will be closed.");
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Failed to find corner black. Please check " + fileDir + ". This form will be closed.");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "This form will be closed shortly.");
                this.Close();
            }

            MessageBox.Show("Process has been completed. Please select View Result in the main form.");
            this.Close();
        }

        //------------------------------------------------------------------------------------------
        private bool checkValueArray(float[] A, float j, int k)
        {
            int r = 2;
            if ((k > 5) & (k > 7))
            {
                r = k - 5;
            }
            else if(k==2)
            {
                r = 2;
            }
            else //k > 2 & k < 5
            {
                r = 3;
            }

            bool flagg = true;


            for (int i = 0; i <= r; i++)
            {
                if (A.Contains(j + i)) flagg = false;
                if (A.Contains(j - i)) flagg = false;
            }

            return flagg;
        }

        //--------------------------------------------------------------------------------------------
        private void convertToGrayscale(Mat A, Mat B)
        {
            CvInvoke.Threshold(A, B, 200, 255, ThresholdType.Binary);
        }


        //--------------------------------------------------------------------------------------------
        private void detectColorBlack(Mat A, Mat B)
        {
            CvInvoke.InRange(A,
                new ScalarArray(new MCvScalar(0, 0, 0, 0)),
                new ScalarArray(new MCvScalar(180, 255, 30, 0)),
                B);
        }

        //--------------------------------------------------------------------------------------------
        private void detectColorBlue(Mat A, Mat B)
        {
            CvInvoke.InRange(A,
                new ScalarArray(new MCvScalar(130, 0, 0, 0)),
                new ScalarArray(new MCvScalar(255, 110, 140, 0)),
                B);
        }

        //--------------------------------------------------------------------------------------------
        private void GFTT(VectorOfKeyPoint corners, Mat A, Mat Ori, Double q)
        {
            Double qualityLevel = q;
            Double minDistance = 10;
            int blockSize = 3;
            bool useHarrisDetector = false;
            Double k = 0.04;

            GFTTDetector keyPointGen = new GFTTDetector(maxCorner, qualityLevel, minDistance, blockSize, useHarrisDetector, k);
            //keyPointGen.Detect(cvtBlack, null);
            keyPointGen.DetectRaw(A, corners, null);

            int y = corners.Size;
            tampilkan = Ori.ToImage<Gray, float>();
            CircleF cir = new CircleF();
            PointF pointz = new PointF();
            for (int i = 0; i <= y - 1; i++)
            {
                cir.Radius = 6;
                pointz.X = corners[i].Point.X;
                pointz.Y = corners[i].Point.Y;
                cir.Center = pointz;
                tampilkan.Draw(cir, new Gray(12.0), 1);
            }
        }

        //--------------------------------------------------------------------------------------------
        private void initAnswerArray()
        {
            answer = new int[TotalRow][];
            for (int m = 0; m < answer.Length; m++)
            {
                answer[m] = new int[TotalCol];
                for (int n = 0; n <= TotalCol - 1; n++)
                {
                    answer[m][n] = 0;
                }
            }
        }

        private void Process_Load(object sender, EventArgs e)
        {
            button_p_s.BackColor = Color.Black;
            button_p_s.ForeColor = Color.Gold;
        }

        //--------------------------------------------------------------------------------------------
        private void initImage(string a)
        {
            src = new Mat();
            src = CvInvoke.Imread(a, LoadImageType.AnyColor);
            cvtBlack = new Mat();
        }

        private void SetH_Ans(float[] fw, float p, ref int ans)
        {
            float f, g;
            if (fw.Length > 0)
            {
                int i = 0;
                bool flg = false;

                while ((i >= 0) & (flg == false))
                {
                    f = fw[i];
                    g = fw[i + 1];
                    if ((p >= f) & (p <= g))
                    {
                        flg = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                ans = i;

            }
        }

        //-------------------------------------------------
        private void SetW_Ans(float[] fw, float p, ref int ans)
        {
            float f, g;
            if (fw.Length > 0)
            {
                int i = 0;
                bool flg = false;

                while ((i >= 0) & (flg == false))
                {
                    f = fw[i];
                    g = fw[i + 1];
                    if ((p >= f) & (p <= g))
                    {
                        flg = true;
                    }
                    else
                    {
                        i++;
                    }
                }
                ans = i;

            }
        }
        
    }
}
