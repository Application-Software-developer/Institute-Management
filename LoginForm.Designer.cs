namespace prototype
{
    partial class LoginForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblId = new System.Windows.Forms.Label();
            this.lblPw = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtPw = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(233, 171);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(30, 15);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "ID :";
            // 
            // lblPw
            // 
            this.lblPw.AutoSize = true;
            this.lblPw.Location = new System.Drawing.Point(233, 251);
            this.lblPw.Name = "lblPw";
            this.lblPw.Size = new System.Drawing.Size(41, 15);
            this.lblPw.TabIndex = 1;
            this.lblPw.Text = "PW :";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(290, 168);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(176, 25);
            this.txtId.TabIndex = 2;
            // 
            // txtPw
            // 
            this.txtPw.Location = new System.Drawing.Point(290, 248);
            this.txtPw.Name = "txtPw";
            this.txtPw.Size = new System.Drawing.Size(176, 25);
            this.txtPw.TabIndex = 3;
            this.txtPw.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(516, 171);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(155, 102);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "로그인";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtPw);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.lblPw);
            this.Controls.Add(this.lblId);
            this.Name = "Login";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblId;
        private System.Windows.Forms.Label lblPw;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtPw;
        private System.Windows.Forms.Button btnLogin;
    }
}

