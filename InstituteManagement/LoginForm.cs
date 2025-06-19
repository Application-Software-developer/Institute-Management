using System;
using System.Drawing;
using System.Windows.Forms;

namespace InstituteManagement
{
    public partial class LoginForm : Form
    {
        private Label lblId, lblPw;
        private TextBox txtId, txtPw;
        private Button btnLogin;

        public LoginForm()
        {
            InitializeStandaloneLayout();
        }

        private void InitializeStandaloneLayout()
        {
            this.Text = "로그인";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ClientSize = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.WhiteSmoke;

            Label title = new Label
            {
                Text = "학원 통합 관리 시스템",
                Font = new Font("Noto Sans KR", 14.25F, FontStyle.Bold),
                ForeColor = Color.Black,
                Location = new Point(50, 30),
                AutoSize = true
            };

            lblId = new Label
            {
                Text = "ID:",
                Location = new Point(50, 90),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10F)
            };

            txtId = new TextBox
            {
                Location = new Point(140, 90),
                Size = new Size(200, 25)
            };

            lblPw = new Label
            {
                Text = "PW:",
                Location = new Point(50, 130),
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 10F)
            };

            txtPw = new TextBox
            {
                Location = new Point(140, 130),
                Size = new Size(200, 25),
                UseSystemPasswordChar = true
            };

            btnLogin = new Button
            {
                Text = "로그인",
                Location = new Point(140, 180),
                Size = new Size(200, 40),
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;

            this.Controls.AddRange(new Control[] { title, lblId, txtId, lblPw, txtPw, btnLogin });
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string enteredId = txtId.Text.Trim();
            string enteredPw = txtPw.Text.Trim();

            if (string.IsNullOrEmpty(enteredId) || string.IsNullOrEmpty(enteredPw))
            {
                MessageBox.Show("ID와 비밀번호를 모두 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            const string validId = "admin";
            const string validPw = "1234";

            if (enteredId == validId && enteredPw == validPw)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("로그인 실패. ID 또는 비밀번호를 확인하세요.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
