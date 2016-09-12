using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace AutoSummaryV01
{
    public partial class GraphAndTable : Form
    {
        public GraphAndTable()
        {
            InitializeComponent();
        }

        private int[][] k; //this is for survey
        private List<int> h = new List<int>(); //this is for grading

        private void ViewResult_Load(object sender, EventArgs e)
        {
            button_vr_s.BackColor = Color.Black;
            button_vr_s.ForeColor = Color.Gold;

            textBox1.Text = GlobalClass.ASUMDir;
            textBox2.Text = GlobalClass.ASUMDir;
        }

        private void insertLineToArray(string input1)
        {
            int rowNumber = (int)char.GetNumericValue(input1[0]);
            int a;

            for(int i=1;i<=GlobalClass.VR_col+1 ;i++)
            {
                a = (int)char.GetNumericValue(input1[i]);
                k[rowNumber][i-1] = k[rowNumber][i-1]+ a;
            }
        }

        private bool flagLineLength(string a, int k)
        {
            return (a.Length == k);
        }

        private void initDataGridView()
        {
            dataGridView1.RowCount = GlobalClass.VR_row + 1;
            dataGridView1.ColumnCount = GlobalClass.VR_col + 1;

            for (int i = 0; i <= GlobalClass.VR_row; i++)
            {
                for (int j=0;j<= GlobalClass.VR_col; j++)
                {
                    dataGridView1.Rows[i].Cells[j].ValueType = typeof(int);
                    dataGridView1.Rows[i].Cells[j].Value = k[i][j];
                    
                }
            }

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {

                column.HeaderText = String.Concat("Answer ",
                    column.Index.ToString());
            }

            foreach(DataGridViewRow row in dataGridView1.Rows)
            {
                row.HeaderCell.Value = String.Concat("Question ", 
                    row.Index.ToString());
            }
            
        }

        private void button_vr_s_Click(object sender, EventArgs e)
        {
            if(GlobalClass.ASUMDir == null || GlobalClass.ASUMDir == "")
            {
                MessageBox.Show("Please select a file.");
            }
            else
            {
                string line1, line2, subline1;
                StreamReader SR = new StreamReader(GlobalClass.ASUMDir);

                int col, row;
                try
                {
                    line1 = SR.ReadLine();
                    if (char.IsNumber(line1[0]) & char.IsNumber(line1[1]))
                    {
                        row = (int)char.GetNumericValue(line1[0]);
                        col = (int)char.GetNumericValue(line1[1]);

                        GlobalClass.VR_col = col;
                        GlobalClass.VR_row = row;


                        initArrayAns();

                        line2 = SR.ReadLine();
                        int lineLength = (row + 1) * (col + 2);
                        bool glaf = flagLineLength(line2, lineLength);
                        while ((line2 != null) & glaf) //read per line
                        {
                            for (int i = 0; i <= row; i++)
                            {
                                int p = i * (col + 2);
                                subline1 = line2.Substring(p, col + 2);
                                insertLineToArray(subline1);
                            }
                            line2 = SR.ReadLine();

                            if (line2 == null)
                            {
                                glaf = false;
                            }
                            else
                            {
                                glaf = flagLineLength(line2, lineLength);
                            }
                        }


                        initDataGridView();
                    }
                    else
                    {
                        MessageBox.Show("Line 1 is not number or does not match criteria.");
                    }


                    if (GlobalClass.VR_row > 0)
                    {
                        setComboBox();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void setComboBox()
        {
            comboBox1.Text = "Choose 1";
            for (int i =0; i<=GlobalClass.VR_row;i++)
            {
                comboBox1.Items.Add("Question "+i.ToString());
            }
            comboBox1.SelectedIndex = 0;
        }

        private void initArrayAns()
        {
            k = new int[GlobalClass.VR_row+1][];
            for (int m = 0; m < k.Length; m++)
            {
                k[m] = new int[GlobalClass.VR_col+1];
                for (int n = 0; n <= GlobalClass.VR_col; n++)
                {
                    k[m][n] = 0;
                }
            }
        }

        

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int v = comboBox1.SelectedIndex;

                chart1.Series.Clear();

                chart1.Series.Add("Series1");
                chart1.Series["Series1"].ChartType = SeriesChartType.Bar;
                int i = 0;
                for (i = 0; i <= GlobalClass.VR_col; i++)
                {
                    int a = int.Parse(dataGridView1.Rows[v].Cells[i].Value.ToString());
                    chart1.Series["Series1"].Points.AddXY(i, a);
                    chart1.Series["Series1"].Points[i].AxisLabel = "Answer " + i.ToString();
                    DataSet dataSet1 = chart1.DataManipulator.ExportSeriesValues("Series1");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            changeFile();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(GlobalClass.grading == null)
            {
                MessageBox.Show("Answer key is not filled yet.");
            }
            else if(GlobalClass.ASUMDir == null || GlobalClass.ASUMDir == "")
            {
                MessageBox.Show("Please choose ASUM File.");
            }
            else
            {
                
                    string address1 = GlobalClass.ASUMDir;
                    StreamReader SR = new StreamReader(address1);
                    string line1, line2, subline1;

                    int col, row, lini;
                    try
                    {
                        line1 = SR.ReadLine();
                        if (char.IsNumber(line1[0]) & char.IsNumber(line1[1]))
                        {
                            row = (int)char.GetNumericValue(line1[0]);
                            col = (int)char.GetNumericValue(line1[1]);

                            GlobalClass.VR_col = col;
                            GlobalClass.VR_row = row;

                            //initArrayAnsforGrading();
                            int lineCount = -1;
                            int grade = 0;
                            line2 = SR.ReadLine();
                            int lineLength = (row + 1) * (col + 2);
                            bool glaf = flagLineLength(line2, lineLength);

                            while ((line2 != null) & glaf) //read per line
                            {
                                grade = 0;
                                lineCount = lineCount + 1;
                                for (int i = 0; i <= row; i++)
                                {
                                    int p = i * (col + 2);
                                    subline1 = line2.Substring(p, col + 2);
                                    //function to change subline into int (int)
                                    lini = ChangeSublineToInt(subline1);
                                    //function to compare int to int ans (bool)
                                    if(compareIntToAns(lini, GlobalClass.grading[i]) == true)
                                    {
                                        grade = grade + 1; //grade + (100 / (GlobalClass.VR_row+1));
                                    }
                                    // if true then grade = grade + (100/numofquestion)

                                    //else do nothing
                                }

                                //MessageBox.Show(lineCount.ToString()+" "+grade.ToString());
                                h.Add(grade);
                                

                                line2 = SR.ReadLine();

                                if (line2 == null || line2 == "")
                                {
                                    glaf = false;
                                }
                                else
                                {
                                    glaf = flagLineLength(line2, lineLength);
                                }

                            }


                            initDataGridViewforGrading(lineCount);
                        }
                        else
                        {
                            MessageBox.Show("Line 1 is not number or does not match criteria.");
                        }
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
            }

        }

        private void initDataGridViewforGrading(int lineCount)
        {
            dataGridView2.RowCount = lineCount+1;
            dataGridView2.ColumnCount = 2;

            dataGridView2.Columns[0].HeaderText = "Student ID";
            dataGridView2.Columns[1].HeaderText = "Grade";

            for (int i = 0; i <= lineCount; i++)
            {
                dataGridView2.Rows[i].Cells[1].ValueType = typeof(double);
                dataGridView2.Rows[i].Cells[1].Value = h[i]*100/(GlobalClass.VR_row+1);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            changeFile();
        }

        private int ChangeSublineToInt( string subline1)
        {
            int res=-1;
            int a;
            for(int i=1; i<=subline1.Length-1; i++)
            {
                a = (int)char.GetNumericValue(subline1[i]);
                if (a == 1)
                {
                    res = i-1;
                }
            }
            return res;
        }

        private bool compareIntToAns(int A, int B)
        {
            return (A == B);
        }

        private void changeFile()
        {
            openFileDialog1.FileName = textBox1.Text;
            openFileDialog1.Filter = "ASUM Files (*.asum)|*.asum|All files (*.*)|*.*";
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
            textBox2.Text = openFileDialog1.FileName;
            GlobalClass.ASUMDir = textBox1.Text;
        }

    }
}
