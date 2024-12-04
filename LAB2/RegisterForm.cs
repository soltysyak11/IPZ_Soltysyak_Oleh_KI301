using Npgsql;
using System;
using System.Data;
using System.Drawing;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LAB2
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(FormClosingFunction);

            UserNameField.Text = "Введіть ім'я";
            UserSurnameField.Text = "Введіть прізвище";
            UserEmailField.Text = "Введіть email";
            UserLoginField.Text = "Придумайте логін";

            this.UserPasswordField1.AutoSize = false;
            this.UserPasswordField1.Size = new Size(this.UserPasswordField1.Size.Width, 64);

            this.UserPasswordField2.AutoSize = false;
            this.UserPasswordField2.Size = new Size(this.UserPasswordField2.Size.Width, 64);
        }

        private void UserNameField_Enter(object sender, EventArgs e)
        {
            if (UserNameField.Text == "Введіть ім'я")
            {
                UserNameField.Text = "";
                UserNameField.ForeColor = Color.Black;
            }
        }
        private void UserNameField_Leave(object sender, EventArgs e)
        {
            if (UserNameField.Text == "")
            {
                UserNameField.Text = "Введіть ім'я";
                UserNameField.ForeColor = Color.Gray;
            }
        }
        private void UserSurnameField_Enter(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "Введіть прізвище")
            {
                UserSurnameField.Text = "";
                UserSurnameField.ForeColor = Color.Black;
            }
        }
        private void UserSurnameField_Leave(object sender, EventArgs e)
        {
            if (UserSurnameField.Text == "")
            {
                UserSurnameField.Text = "Введіть прізвище";
                UserSurnameField.ForeColor = Color.Gray;
            }
        }
        private void UserEmailField_Enter(object sender, EventArgs e)
        {
            if (UserEmailField.Text == "Введіть email")
            {
                UserEmailField.Text = "";
                UserEmailField.ForeColor = Color.Black;
            }
        }
        private void UserEmailField_Leave(object sender, EventArgs e)
        {
            if (UserEmailField.Text == "")
            {
                UserEmailField.Text = "Введіть email";
                UserEmailField.ForeColor = Color.Gray;
            }
        }
        private void UserLoginField_Enter_1(object sender, EventArgs e)
        {
            if (UserLoginField.Text == "Придумайте логін")
            {
                UserLoginField.Text = "";
                UserLoginField.ForeColor = Color.Black;
            }
        }
        private void UserLoginField_Leave(object sender, EventArgs e)
        {
            if (UserLoginField.Text == "")
            {
                UserLoginField.Text = "Придумайте логін";
                UserLoginField.ForeColor = Color.Gray;
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Log_In loginForm = new Log_In();
            loginForm.Show();
        }
        private void RegisterButton_Click_1(object sender, EventArgs e)
        {
            // Отримати значення імені, прізвища, та email з полів вводу
            string enteredName = UserNameField.Text;
            string enteredSurname = UserSurnameField.Text;
            string enteredEmail = UserEmailField.Text;
            string enteredLogin = UserLoginField.Text;
            string enteredPassword = "";
            if (UserNameField.Text == "Введіть ім'я" && UserSurnameField.Text == "Введіть прізвище" && UserEmailField.Text == "Введіть email" && UserLoginField.Text == "Придумайте логін" && UserPasswordField1.Text == "" && UserPasswordField2.Text == "")
            {
                MessageBox.Show("Enter data!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UserPasswordField1.Text == UserPasswordField2.Text)
            {
                if (CheckUserPassword(UserPasswordField1.Text))
                {
                    enteredPassword = UserPasswordField1.Text;
                }
                else
                {
                    MessageBox.Show("The password must be at least 8 characters long!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Passwords are different!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (UserNameField.Text == "Введіть ім'я")
            {
                MessageBox.Show("Enter your name!");
                return;
            }
            if (UserSurnameField.Text == "Введіть прізвище")
            {
                MessageBox.Show("Enter your surname!");
                return;
            }
            if (UserEmailField.Text == "Введіть email")
            {
                MessageBox.Show("Enter your email!");
                return;
            }
            if (UserLoginField.Text == "Придумайте логін")
            {
                MessageBox.Show("Enter your login!");
                return;
            }
            if (UserPasswordField1.Text == "")
            {
                MessageBox.Show("Enter your password!");
                return;
            }
            if (CheckUserLogin())
            {
                return;
            }
            if (CheckUserEmail())
            {
                return;
            }
            if (EmailValidation(enteredEmail))
            {
                //MessageBox.Show("Email is correct!");
            }
            else
            {
                MessageBox.Show("Email Error!\nFollow email structure: [example@example.com]", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TcpClient client = new TcpClient("127.0.0.1", 3660);
            DataBaseInfo DB = new DataBaseInfo();
            NpgsqlCommand command = new NpgsqlCommand("INSERT INTO users (login, password, email, name, surname) VALUES (@EL, @EP, @EE, @EN, @ES)", DB.GetConnection());
            ServerInfo.InsertRegisterData(command, enteredName, enteredSurname, enteredEmail, enteredLogin, enteredPassword);
            DB.OpenConnection();
            if (command.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("Account Creation Success!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Log_In.SendMessage(client, "InsertRegisterDataSuccess");
                this.Hide();
                VideoMonitoringForm videoForm = new VideoMonitoringForm();
                videoForm.Show();
            }
            else
            {
                MessageBox.Show("Account Creation Error!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Log_In.SendMessage(client, "InsertRegisterDataError");
            }
            DB.CloseConnection();
        }

        public Boolean CheckUserLogin()
        {
            DataBaseInfo DB = new DataBaseInfo();
            DataTable table = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE login = @EL", DB.GetConnection());
            command.Parameters.AddWithValue("@EL", UserLoginField.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This LogIn is already taken!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean CheckUserEmail()
        {
            DataBaseInfo DB = new DataBaseInfo();
            DataTable table = new DataTable();
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE email = @EE", DB.GetConnection());
            command.Parameters.AddWithValue("@EE", UserEmailField.Text);
            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MessageBox.Show("This Email is already taken!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return true;
            }
            else
            {
                return false;
            }
        }
        public Boolean CheckUserPassword(string password)
        {
            return password.Length >= 8;
        }
        public Boolean EmailValidation(string email)
        {
            // Регулярний вираз для перевірки електронної пошти
            string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";

            // Створення об'єкта Regex
            Regex regex = new Regex(pattern);

            // Перевірка електронної пошти
            return regex.IsMatch(email);
        }
        private void FormClosingFunction(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                Application.Exit();
            }
        }
    }
}
