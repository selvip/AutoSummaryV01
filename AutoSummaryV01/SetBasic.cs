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

namespace AutoSummaryV01
{
    public partial class InputSettings : Form
    {
         
        public InputSettings()
        {
            InitializeComponent();
        }

        private void button_sb_ok_Click(object sender, EventArgs e)
        {
            if ((comboBox1.Text != "") & (comboBox2.Text != "") & (textBox1.Text != ""))
            {
                if(radioButton1.Checked == true)
                {
                    GlobalClass.BlueCross = true;
                }
                else
                {
                    GlobalClass.BlueCross = false;
                }
                
                if(GlobalClass.NumOfAns > 0 & GlobalClass.NumOfQues > 0)
                {

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please fill the number of question and answer. Click apply after.");
                }
                
                
            }
            else
            {
                MessageBox.Show("Select number of question and answer.");
            }
        }

        private void button_sb_bf_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Title = "Open Files";
            ofd.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png|All files (*.*)|*.*";
            DialogResult result = ofd.ShowDialog();
            GlobalClass.FileDir = ofd.FileNames;
            MessageBox.Show("Files found: " + GlobalClass.FileDir.Length.ToString(), "Message");
            string fileNames = "";
            for (int i = 0; i <= GlobalClass.FileDir.Length - 1; i++)
            {
                fileNames = fileNames
                    + GlobalClass.FileDir[i]
                    + Environment.NewLine;
            }
            textBox1.Text = fileNames;
        }
        

        private void SetBasic_Load(object sender, EventArgs e)
        {
            button_is_ok.BackColor = Color.Black;
            button_is_ok.ForeColor = Color.Gold;

            button_sb_bf.BackColor = Color.Black;
            button_sb_bf.ForeColor = Color.Gold;

            radioButton1.Checked = true;
            
            comboBox3.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox1.SelectedIndex = 1;
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 0 )
            {
                button_is_sa.Enabled = false;
            }
            else if(comboBox3.SelectedIndex == 1)
            {
                button_is_sa.Enabled = true;
            }
        }

        private void button_is_sa_Click(object sender, EventArgs e)
        {
            AnswerKey newform = new AnswerKey();
            newform.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalClass.NumOfQues = int.Parse(comboBox1.Text);
            GlobalClass.NumOfAns = int.Parse(comboBox2.Text);
        }
    }
    
}
