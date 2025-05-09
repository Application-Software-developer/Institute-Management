using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace AcademyManager
{
    public partial class CheckInAndOut : Form
    {
        private TcpListener server;
        private Thread listenThread;
        public CheckInAndOut()
        {
            InitializeComponent();
        }

        private void CheckInAndOut_Load(object sender, EventArgs e)
        {
            listenThread = new Thread(StartServer);
            listenThread.IsBackground = true;
            listenThread.Start();
            lstLog.Items.Add("서버 시작됨...");
        }

        private void StartServer()
        {
            try
            {
                server = new TcpListener(IPAddress.Any, 9000);
                server.Start();

                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread clientThread = new Thread(HandleClient);
                    clientThread.IsBackground = true;
                    clientThread.Start(client);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("서버 오류: " + ex.Message);
            }
        }

        private void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            int bytes;
            while ((bytes = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytes);
                this.Invoke((MethodInvoker)delegate {
                    lstLog.Items.Add("[수신] " + message);
                });
            }

            stream.Close();
            client.Close();
        }

        private void CheckInAndOutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            server?.Stop();
            listenThread?.Abort();
        }
    }
}
