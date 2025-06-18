using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AcademyManager
{
    public partial class ClientForm : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        public ClientForm()
        {
            InitializeComponent();
        }
        private void ClientForm_Load(object sender, EventArgs e)
        {
            ConnectToServer();
        }
        private void ConnectToServer()
        {
            try
            {
                client = new TcpClient("127.0.0.1", 9000); // 서버 주소 및 포트
                stream = client.GetStream();
                lstLog.Items.Add("[서버 연결됨]");
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 연결 실패: " + ex.Message);
            }
        }
        private void SendMessage(string message)
        {
            if (stream != null && stream.CanWrite)
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                stream.Write(data, 0, data.Length);
            }
        }
        private void btnCheckin_Click(object sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            if (!string.IsNullOrEmpty(studentId))
            {
                SendMessage($"CHECKIN|{studentId}");
                lstLog.Items.Add($"[출석 전송] {studentId}");
                txtStudentId.Clear();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            string studentId = txtStudentId.Text.Trim();
            if (!string.IsNullOrEmpty(studentId))
            {
                SendMessage($"CHECKOUT|{studentId}");
                lstLog.Items.Add($"[하원 전송] {studentId}");
                txtStudentId.Clear();
            }
        }
        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stream?.Close();
            client?.Close();
        }
    }
}
