namespace AutoSummaryV01
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.button_sbf = new System.Windows.Forms.Button();
            this.button_sof = new System.Windows.Forms.Button();
            this.button_gr = new System.Windows.Forms.Button();
            this.button_vr = new System.Windows.Forms.Button();
            this.label_sbf = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_sbf
            // 
            this.button_sbf.Location = new System.Drawing.Point(12, 82);
            this.button_sbf.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_sbf.Name = "button_sbf";
            this.button_sbf.Size = new System.Drawing.Size(184, 58);
            this.button_sbf.TabIndex = 0;
            this.button_sbf.Text = "Set Basic Form";
            this.button_sbf.UseVisualStyleBackColor = true;
            this.button_sbf.Click += new System.EventHandler(this.button1_Click);
            // 
            // button_sof
            // 
            this.button_sof.Location = new System.Drawing.Point(12, 159);
            this.button_sof.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_sof.Name = "button_sof";
            this.button_sof.Size = new System.Drawing.Size(184, 58);
            this.button_sof.TabIndex = 1;
            this.button_sof.Text = "Select Output File";
            this.button_sof.UseVisualStyleBackColor = true;
            this.button_sof.Click += new System.EventHandler(this.button_sof_Click);
            // 
            // button_gr
            // 
            this.button_gr.Location = new System.Drawing.Point(12, 237);
            this.button_gr.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_gr.Name = "button_gr";
            this.button_gr.Size = new System.Drawing.Size(184, 58);
            this.button_gr.TabIndex = 2;
            this.button_gr.Text = "Getting Result";
            this.button_gr.UseVisualStyleBackColor = true;
            this.button_gr.Click += new System.EventHandler(this.button_gr_Click);
            // 
            // button_vr
            // 
            this.button_vr.Location = new System.Drawing.Point(12, 315);
            this.button_vr.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button_vr.Name = "button_vr";
            this.button_vr.Size = new System.Drawing.Size(184, 58);
            this.button_vr.TabIndex = 3;
            this.button_vr.Text = "View Result";
            this.button_vr.UseVisualStyleBackColor = true;
            this.button_vr.Click += new System.EventHandler(this.button_vr_Click);
            // 
            // label_sbf
            // 
            this.label_sbf.AutoSize = true;
            this.label_sbf.Location = new System.Drawing.Point(305, 105);
            this.label_sbf.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_sbf.Name = "label_sbf";
            this.label_sbf.Size = new System.Drawing.Size(0, 14);
            this.label_sbf.TabIndex = 5;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(214, 181);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Directory:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gold;
            this.ClientSize = new System.Drawing.Size(318, 503);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_sbf);
            this.Controls.Add(this.button_vr);
            this.Controls.Add(this.button_gr);
            this.Controls.Add(this.button_sof);
            this.Controls.Add(this.button_sbf);
            this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "MainForm";
            this.Text = "Auto Summary V.01";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_sbf;
        private System.Windows.Forms.Button button_sof;
        private System.Windows.Forms.Button button_gr;
        private System.Windows.Forms.Button button_vr;
        private System.Windows.Forms.Label label_sbf;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
    }
}

