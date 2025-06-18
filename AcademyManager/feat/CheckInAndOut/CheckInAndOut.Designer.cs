namespace AcademyManager
{
    partial class CheckInAndOut
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
            this.lstLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lstLog
            // 
            this.lstLog.FormattingEnabled = true;
            this.lstLog.ItemHeight = 12;
            this.lstLog.Location = new System.Drawing.Point(191, 37);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(379, 328);
            this.lstLog.TabIndex = 0;
            // 
            // CheckInAndOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lstLog);
            this.Name = "CheckInAndOut";
            this.Text = "CheckInAndOut";
            this.Load += new System.EventHandler(this.CheckInAndOut_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstLog;
    }
}