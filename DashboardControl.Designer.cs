// DashboardControl.Designer.cs
using System.Drawing;
using System.Windows.Forms;
using System;

namespace mainForm
{
    partial class DashboardControl
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblDate;
        private Label lblTime;
        private Timer timerClock;
        private Label lblNoticeTitle;
        private ListView lvNoticeSummary;
        private Button btnViewAllNotices;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.lblNoticeTitle = new System.Windows.Forms.Label();
            this.lvNoticeSummary = new System.Windows.Forms.ListView();
            this.btnViewAllNotices = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Noto Sans KR", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblDate.Location = new System.Drawing.Point(30, 20);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(300, 30);
            this.lblDate.TabIndex = 0;
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("Noto Sans KR Medium", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTime.Location = new System.Drawing.Point(30, 50);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(300, 30);
            this.lblTime.TabIndex = 1;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // lblNoticeTitle
            // 
            this.lblNoticeTitle.Font = new System.Drawing.Font("Noto Sans KR", 15.75F, System.Drawing.FontStyle.Bold);
            this.lblNoticeTitle.Location = new System.Drawing.Point(30, 100);
            this.lblNoticeTitle.Name = "lblNoticeTitle";
            this.lblNoticeTitle.Size = new System.Drawing.Size(300, 30);
            this.lblNoticeTitle.TabIndex = 2;
            this.lblNoticeTitle.Text = "📌 최근 공지사항";
            // 
            // lvNoticeSummary
            // 
            this.lvNoticeSummary.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold);
            this.lvNoticeSummary.FullRowSelect = true;
            this.lvNoticeSummary.GridLines = true;
            this.lvNoticeSummary.HideSelection = false;
            this.lvNoticeSummary.Location = new System.Drawing.Point(30, 140);
            this.lvNoticeSummary.Name = "lvNoticeSummary";
            this.lvNoticeSummary.Scrollable = false;
            this.lvNoticeSummary.Size = new System.Drawing.Size(700, 120);
            this.lvNoticeSummary.TabIndex = 3;
            this.lvNoticeSummary.UseCompatibleStateImageBehavior = false;
            this.lvNoticeSummary.View = System.Windows.Forms.View.Details;
            // 
            // btnViewAllNotices
            // 
            this.btnViewAllNotices.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold);
            this.btnViewAllNotices.Location = new System.Drawing.Point(565, 268);
            this.btnViewAllNotices.Name = "btnViewAllNotices";
            this.btnViewAllNotices.Size = new System.Drawing.Size(165, 30);
            this.btnViewAllNotices.TabIndex = 4;
            this.btnViewAllNotices.Text = "▶ 전체 공지사항 보기";
            this.btnViewAllNotices.UseVisualStyleBackColor = true;
            this.btnViewAllNotices.Click += new System.EventHandler(this.btnViewAllNotices_Click);
            // 
            // DashboardControl
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblNoticeTitle);
            this.Controls.Add(this.lvNoticeSummary);
            this.Controls.Add(this.btnViewAllNotices);
            this.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.Name = "DashboardControl";
            this.Size = new System.Drawing.Size(800, 350);
            this.ResumeLayout(false);

        }
    }
}