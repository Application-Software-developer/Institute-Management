namespace timeScheduler
{
    partial class TimeTableForm
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
            this.panelGrid = new System.Windows.Forms.Panel();
            this.txtTeacherName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lstTeachers = new System.Windows.Forms.ListBox();
            this.btnFinish = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnAllView = new System.Windows.Forms.Button();
            this.btnOut = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // panelGrid
            // 
            this.panelGrid.Location = new System.Drawing.Point(252, 63);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Size = new System.Drawing.Size(700, 450);
            this.panelGrid.TabIndex = 0;
            // 
            // txtTeacherName
            // 
            this.txtTeacherName.Location = new System.Drawing.Point(52, 63);
            this.txtTeacherName.Name = "txtTeacherName";
            this.txtTeacherName.Size = new System.Drawing.Size(145, 21);
            this.txtTeacherName.TabIndex = 0;
            this.txtTeacherName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTeacherName_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(52, 314);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 27);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "저장";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lstTeachers
            // 
            this.lstTeachers.FormattingEnabled = true;
            this.lstTeachers.ItemHeight = 12;
            this.lstTeachers.Location = new System.Drawing.Point(52, 100);
            this.lstTeachers.Name = "lstTeachers";
            this.lstTeachers.Size = new System.Drawing.Size(159, 208);
            this.lstTeachers.TabIndex = 2;
            this.lstTeachers.SelectedIndexChanged += new System.EventHandler(this.lstTeachers_SelectedIndexChanged);
            // 
            // btnFinish
            // 
            this.btnFinish.Location = new System.Drawing.Point(871, 519);
            this.btnFinish.Name = "btnFinish";
            this.btnFinish.Size = new System.Drawing.Size(81, 27);
            this.btnFinish.TabIndex = 3;
            this.btnFinish.Text = "완료";
            this.btnFinish.UseVisualStyleBackColor = true;
            this.btnFinish.Click += new System.EventHandler(this.btnFinish_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(790, 519);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(72, 25);
            this.btnLoad.TabIndex = 4;
            this.btnLoad.Text = "불러오기";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnAllView
            // 
            this.btnAllView.Location = new System.Drawing.Point(252, 521);
            this.btnAllView.Name = "btnAllView";
            this.btnAllView.Size = new System.Drawing.Size(75, 23);
            this.btnAllView.TabIndex = 5;
            this.btnAllView.Text = "View All";
            this.btnAllView.UseVisualStyleBackColor = true;
            this.btnAllView.Click += new System.EventHandler(this.btnAllView_Click);
            // 
            // btnOut
            // 
            this.btnOut.Location = new System.Drawing.Point(880, 623);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(72, 26);
            this.btnOut.TabIndex = 6;
            this.btnOut.Text = "나가기";
            this.btnOut.UseVisualStyleBackColor = true;
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // TimeTableForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.btnOut);
            this.Controls.Add(this.btnAllView);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnFinish);
            this.Controls.Add(this.lstTeachers);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtTeacherName);
            this.Controls.Add(this.panelGrid);
            this.Name = "TimeTableForm";
            this.Text = "TimeTableForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.TextBox txtTeacherName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ListBox lstTeachers;
        private System.Windows.Forms.Button btnFinish;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnAllView;
        private System.Windows.Forms.Button btnOut;
    }
}