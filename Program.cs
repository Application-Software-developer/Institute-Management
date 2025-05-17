using System;
using System.Windows.Forms;
using InstituteManagement;

namespace InstituteManagement
{
    static class Program
    {
        // 인증 성공 여부를 전역으로 관리 (MainForm 접근 제한용)
        public static bool IsAuthenticated = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 먼저 로그인 폼 실행
            using (LoginForm loginForm = new LoginForm())
            {
                DialogResult result = loginForm.ShowDialog();

                // 로그인 성공한 경우에만 MainForm 실행
                if (result == DialogResult.OK)
                {
                    IsAuthenticated = true; // 인증 상태 true로 설정
                    Application.Run(new MainForm());
                }
                else
                {
                    // 로그인 실패 시 앱 종료
                    Application.Exit();
                }
            }
        }
    }
}
