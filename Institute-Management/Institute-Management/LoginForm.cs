using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Institute_Management
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }
        // [로그인 버튼 클릭 시 동작하는 이벤트 핸들러]
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // txtId : 사용자 ID 입력 TextBox
            // txtPw : 사용자 비밀번호 입력 TextBox
            // btnLogin : 로그인 버튼 (이 메서드에 연결되어 있어야 함)

            // 입력값 가져오기 (공백 제거)
            string enteredId = txtId.Text.Trim();
            string enteredPw = txtPw.Text.Trim();

            // 입력값이 비어있을 경우 경고 메시지 출력
            if (string.IsNullOrEmpty(enteredId) || string.IsNullOrEmpty(enteredPw))
            {
                MessageBox.Show("ID와 비밀번호를 모두 입력해주세요.", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 인증 기준 ID/PW (현재는 하드코딩 방식 사용)
            const string validId = "admin";  // 관리자 ID
            const string validPw = "1234";   // 관리자 비밀번호

            // 입력값이 인증 정보와 일치하는 경우
            if (enteredId == validId && enteredPw == validPw)
            {
                // 로그인 성공 시
                // 이 폼(LoginForm)의 DialogResult 값을 OK로 설정하여
                // 외부에서 로그인 성공 여부를 확인 가능하도록 함
                this.DialogResult = DialogResult.OK;

                // 로그인 폼 종료 → 이후 MainForm 등 다른 폼으로 전환 가능
                this.Close();
            }
            else
            {
                // 로그인 실패 시 메시지 박스 출력
                MessageBox.Show("로그인 실패. ID 또는 비밀번호를 확인하세요.", "인증 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
