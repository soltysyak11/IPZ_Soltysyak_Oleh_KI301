using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Windows.Forms;

namespace LAB2
{
    public partial class Log_In : Form
    {
        public Log_In()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormClosingFunction);
            loginField.Text = "Введіть логін";

            this.passwordField.AutoSize = false;
            this.passwordField.Size = new System.Drawing.Size(this.passwordField.Size.Width, 64);
        }

        private void loginField_Enter(object sender, EventArgs e)
        {
            if (loginField.Text == "Введіть логін")
            {
                loginField.Text = "";
                loginField.ForeColor = System.Drawing.Color.Black;
            }
        }
        private void loginField_Leave(object sender, EventArgs e)
        {
            if (loginField.Text == "")
            {
                loginField.Text = "Введіть логін";
                loginField.ForeColor = Color.Gray;
            }
        }

        private void RegisterButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void LoginButton_Click_1(object sender, EventArgs e)
        {
            string enteredLogin = loginField.Text;
            string enteredPassword = passwordField.Text;
            if (loginField.Text == "Введіть логін" && passwordField.Text == "")
            {
                MessageBox.Show("Enter data!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TcpClient client = new TcpClient("127.0.0.1", 3660);
            DataBaseInfo DB = new DataBaseInfo();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            DataTable table = new DataTable();
            ServerInfo.ReadLogInData(DB, adapter, table, enteredLogin, enteredPassword);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("LogIn Success!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SendMessage(client, "ReadLogInDataSuccess");
                this.Hide();
                VideoMonitoringForm videoForm = new VideoMonitoringForm();
                videoForm.Show();
            }
            else
            {
                MessageBox.Show("LogIn Error!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SendMessage(client, "ReadLogInDataError");
            }
        }
        private void FormClosingFunction(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }

        public static void SendMessage(TcpClient client, string message)
        {
            NetworkStream networkStream = client.GetStream();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(message);
            networkStream.Write(buffer, 0, buffer.Length);
            Console.WriteLine($"Sent message: {message}");
        }
    }
}
