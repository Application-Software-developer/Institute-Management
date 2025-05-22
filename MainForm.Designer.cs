namespace prototype
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
            this.btnSheet = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSheet
            // 
            this.btnSheet.Location = new System.Drawing.Point(263, 160);
            this.btnSheet.Name = "btnSheet";
            this.btnSheet.Size = new System.Drawing.Size(240, 152);
            this.btnSheet.TabIndex = 1;
            this.btnSheet.Text = "장부 이동";
            this.btnSheet.UseVisualStyleBackColor = true;
            this.btnSheet.Click += new System.EventHandler(this.btnSheet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSheet);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSheet;
    }
}