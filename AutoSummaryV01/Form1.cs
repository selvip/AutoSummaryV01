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

namespace AutoSummaryV01
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputSettings new_form = new InputSettings();
            new_form.ShowDialog();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {

            button_sbf.BackColor = Color.Black;
            button_sbf.ForeColor = Color.Gold;

            button_sof.BackColor = Color.Black;
            button_sof.ForeColor = Color.Gold;
            
            button_gr.BackColor = Color.Black;
            button_gr.ForeColor = Color.Gold;

            button_vr.BackColor = Color.Black;
            button_vr.ForeColor = Color.Gold;
            


            button_sof.Enabled = true;
            button_gr.Enabled = true;
            // button_sef.Enabled = false;
            

            button_sof.Visible = true;
            button_gr.Visible = true;
            // button_sef.Visible = false;

        }

        private void button_sof_Click(object sender, EventArgs e)
        {
            SaveFileDialog svd = new SaveFileDialog();
            svd.InitialDirectory = @"C:\";
            svd.Title = "Save ASUM file";
            svd.DefaultExt = "asum";
            svd.FileName = "new1";
            svd.Filter = "ASUM Files (*.asum)|*.asum|All files (*.*)|*.*";
            svd.CheckPathExists = true;
            svd.RestoreDirectory = true;
            svd.ShowDialog();
            label1.Text = label1.Text + Environment.NewLine + svd.FileName;
            GlobalClass.ASUMDir = svd.FileName;
        }

        private void button_gr_Click(object sender, EventArgs e)
        {
            Process newProcess = new Process();
            newProcess.ShowDialog();
        }

        private void button_vr_Click(object sender, EventArgs e)
        {
            GraphAndTable newViewResult = new GraphAndTable();
            newViewResult.Show();
        }


    }
}
