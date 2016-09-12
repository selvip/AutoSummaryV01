using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSummaryV01
{
    public partial class AnswerKey : Form
    {
        private ComboBox[] ans;
        private Label[] ques;

        public AnswerKey()
        {
            InitializeComponent();
        }

        private void AnswerKey_Load(object sender, EventArgs e)
        {

            ans = new ComboBox[GlobalClass.NumOfQues];
            ques = new Label[GlobalClass.NumOfQues];

            for (int i = 0; i < GlobalClass.NumOfQues; i++)
            {
                ques[i] = new Label();
                ques[i].Location = new Point(50, 30+ i * 25);
                ques[i].Text = "Question "+ i.ToString();

                ans[i] = new ComboBox();
                ans[i].Location = new Point(120, 30 + i*25);

                for (int j = 0; j < GlobalClass.NumOfAns; j++)
                {
                    ans[i].Items.Add("Answer " + j.ToString());
                    
                }

                this.Controls.Add(ques[i]);
                this.Controls.Add(ans[i]);
            }

        }

        

        private void button_ok_Click(object sender, EventArgs e)
        {
            bool flag = true;

            GlobalClass.grading = new int[GlobalClass.NumOfQues];

            for (int i = 0; i < GlobalClass.NumOfQues; i++)
            {
                if (ans[i].SelectedItem != null)
                {
                    flag = true;
                    GlobalClass.grading[i] = ans[i].SelectedIndex;
                }
                else
                {
                    flag = false;
                }
            }

            if (flag == true)
            {
                this.Close();
            }
            else
            {
                MessageBox.Show("Please fill all the combo boxes.");
            }
        }
    }
}
