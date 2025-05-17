using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InstituteManagement;

namespace InstituteManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            // 프로그램 실행 시 인증 상태를 확인하여 비정상 접근 차단
            if (!Program.IsAuthenticated)
            {
                // 인증되지 않은 접근이면 경고 후 앱 종료
                MessageBox.Show("로그인이 필요합니다.", "접근 거부", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Environment.Exit(0);
            }

            InitializeComponent();
        }
    }
}