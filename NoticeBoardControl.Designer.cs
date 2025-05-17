// NoticeBoardControl.Designer.cs
using System.Windows.Forms;

namespace mainForm
{
    partial class NoticeBoardControl
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ListView lvNotices;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.FlowLayoutPanel btnPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lvNotices = new System.Windows.Forms.ListView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDetail = new System.Windows.Forms.Button();
            this.btnPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(48, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(232, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "📌 공지사항 게시판";
            // 
            // lvNotices
            // 
            this.lvNotices.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvNotices.FullRowSelect = true;
            this.lvNotices.GridLines = true;
            this.lvNotices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvNotices.HideSelection = false;
            this.lvNotices.Location = new System.Drawing.Point(54, 68);
            this.lvNotices.Name = "lvNotices";
            this.lvNotices.Size = new System.Drawing.Size(544, 342);
            this.lvNotices.TabIndex = 1;
            this.lvNotices.UseCompatibleStateImageBehavior = false;
            this.lvNotices.View = System.Windows.Forms.View.Details;
            // 
            // btnCreate
            // 
            this.btnCreate.BackColor = System.Drawing.Color.SkyBlue;
            this.btnCreate.FlatAppearance.BorderSize = 0;
            this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreate.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnCreate.Location = new System.Drawing.Point(30, 20);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(10);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(160, 40);
            this.btnCreate.TabIndex = 0;
            this.btnCreate.Text = "📝 공지사항 작성";
            this.btnCreate.UseVisualStyleBackColor = false;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click_1);
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.SkyBlue;
            this.btnEdit.FlatAppearance.BorderSize = 0;
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnEdit.Location = new System.Drawing.Point(210, 20);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(10);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(160, 40);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "✏️ 공지사항 수정";
            this.btnEdit.UseVisualStyleBackColor = false;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click_1);
            // 
            // btnDetail
            // 
            this.btnDetail.BackColor = System.Drawing.Color.SkyBlue;
            this.btnDetail.FlatAppearance.BorderSize = 0;
            this.btnDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetail.Font = new System.Drawing.Font("Noto Sans KR", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnDetail.Location = new System.Drawing.Point(390, 20);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(10);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(160, 40);
            this.btnDetail.TabIndex = 2;
            this.btnDetail.Text = "🔍 상세 보기";
            this.btnDetail.UseVisualStyleBackColor = false;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click_1);
            // 
            // btnPanel
            // 
            this.btnPanel.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnPanel.Controls.Add(this.btnCreate);
            this.btnPanel.Controls.Add(this.btnEdit);
            this.btnPanel.Controls.Add(this.btnDetail);
            this.btnPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnPanel.Location = new System.Drawing.Point(0, 469);
            this.btnPanel.Name = "btnPanel";
            this.btnPanel.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.btnPanel.Size = new System.Drawing.Size(764, 70);
            this.btnPanel.TabIndex = 2;
            // 
            // NoticeBoardControl
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lvNotices);
            this.Controls.Add(this.btnPanel);
            this.Font = new System.Drawing.Font("맑은 고딕", 10F);
            this.Name = "NoticeBoardControl";
            this.Size = new System.Drawing.Size(764, 539);
            this.btnPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}
 