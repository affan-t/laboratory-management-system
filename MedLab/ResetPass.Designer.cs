namespace MedLab
{
    partial class ResetPass
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
            this.ResetPassBtn = new System.Windows.Forms.Button();
            this.NewPassLbl = new System.Windows.Forms.Label();
            this.NewPassTb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SecKeyTb = new System.Windows.Forms.TextBox();
            this.SecKeyLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ResetPassBtn
            // 
            this.ResetPassBtn.BackColor = System.Drawing.Color.SlateBlue;
            this.ResetPassBtn.Font = new System.Drawing.Font("Century Gothic", 11.25F);
            this.ResetPassBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.ResetPassBtn.Location = new System.Drawing.Point(179, 163);
            this.ResetPassBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ResetPassBtn.Name = "ResetPassBtn";
            this.ResetPassBtn.Size = new System.Drawing.Size(84, 33);
            this.ResetPassBtn.TabIndex = 63;
            this.ResetPassBtn.Text = "Reset";
            this.ResetPassBtn.UseVisualStyleBackColor = false;
            this.ResetPassBtn.Click += new System.EventHandler(this.ResetPassBtn_Click);
            // 
            // NewPassLbl
            // 
            this.NewPassLbl.AutoSize = true;
            this.NewPassLbl.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPassLbl.Location = new System.Drawing.Point(131, 104);
            this.NewPassLbl.Name = "NewPassLbl";
            this.NewPassLbl.Size = new System.Drawing.Size(119, 19);
            this.NewPassLbl.TabIndex = 62;
            this.NewPassLbl.Text = "New Password";
            // 
            // NewPassTb
            // 
            this.NewPassTb.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewPassTb.Location = new System.Drawing.Point(135, 126);
            this.NewPassTb.Name = "NewPassTb";
            this.NewPassTb.Size = new System.Drawing.Size(186, 25);
            this.NewPassTb.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 28);
            this.label1.TabIndex = 60;
            this.label1.Text = "MedLab Laboratories";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MedLab.Properties.Resources.icons8_circle_48;
            this.pictureBox2.Location = new System.Drawing.Point(424, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(26, 25);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 64;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // SecKeyTb
            // 
            this.SecKeyTb.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecKeyTb.Location = new System.Drawing.Point(135, 76);
            this.SecKeyTb.Name = "SecKeyTb";
            this.SecKeyTb.Size = new System.Drawing.Size(186, 25);
            this.SecKeyTb.TabIndex = 61;
            // 
            // SecKeyLbl
            // 
            this.SecKeyLbl.AutoSize = true;
            this.SecKeyLbl.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SecKeyLbl.Location = new System.Drawing.Point(131, 54);
            this.SecKeyLbl.Name = "SecKeyLbl";
            this.SecKeyLbl.Size = new System.Drawing.Size(102, 19);
            this.SecKeyLbl.TabIndex = 62;
            this.SecKeyLbl.Text = "Security Key";
            // 
            // ResetPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(457, 215);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ResetPassBtn);
            this.Controls.Add(this.SecKeyLbl);
            this.Controls.Add(this.NewPassLbl);
            this.Controls.Add(this.SecKeyTb);
            this.Controls.Add(this.NewPassTb);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ResetPass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ResetPass";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button ResetPassBtn;
        private System.Windows.Forms.Label NewPassLbl;
        private System.Windows.Forms.TextBox NewPassTb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SecKeyTb;
        private System.Windows.Forms.Label SecKeyLbl;
    }
}