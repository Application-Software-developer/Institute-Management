namespace AcademyManager
{
    partial class ClientForm
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
            this.txtStudentId = new System.Windows.Forms.MaskedTextBox();
            this.btnCheckin = new System.Windows.Forms.Button();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.Label();
            this.lstLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtStudentId
            // 
            this.txtStudentId.Location = new System.Drawing.Point(325, 59);
            this.txtStudentId.Name = "txtStudentId";
            this.txtStudentId.Size = new System.Drawing.Size(177, 21);
            this.txtStudentId.TabIndex = 0;
            // 
            // btnCheckin
            // 
            this.btnCheckin.Location = new System.Drawing.Point(310, 103);
            this.btnCheckin.Name = "btnCheckin";
            this.btnCheckin.Size = new System.Drawing.Size(75, 23);
            this.btnCheckin.TabIndex = 1;
            this.btnCheckin.Text = "등원";
            this.btnCheckin.UseVisualStyleBackColor = true;
            this.btnCheckin.Click += new System.EventHandler(this.btnCheckin_Click);
            // 
            // btnCheckout
            // 
            this.btnCheckout.Location = new System.Drawing.Point(413, 103);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(75, 23);
            this.btnCheckout.TabIndex = 2;
            this.btnCheckout.Text = "하원";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(254, 62);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(65, 12);
            this.name.TabIndex = 3;
            this.name.Text = "이름 입력: ";
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(256, 158);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(257, 136);
            this.lstLog.TabIndex = 4;
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstLog);
            this.Controls.Add(this.name);
            this.Controls.Add(this.btnCheckout);
            this.Controls.Add(this.btnCheckin);
            this.Controls.Add(this.txtStudentId);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MaskedTextBox txtStudentId;
        private System.Windows.Forms.Button btnCheckin;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.ListBox lstLog;
    }
}