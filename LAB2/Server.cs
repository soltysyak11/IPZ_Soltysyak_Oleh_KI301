using System;
using LAB2;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Threading;
using Npgsql;

namespace LAB2
{
    class ServerInfo
    {
        public void Server()
        {
            TcpListener serverSocket = new TcpListener(IPAddress.Any, 3660);
            System.Diagnostics.Debug.WriteLine("--> Server Started!");
            serverSocket.Start();

            try
            {
                while (true)
                {
                    TcpClient clientSocket = serverSocket.AcceptTcpClient();
                    System.Diagnostics.Debug.WriteLine("--> Client Connected!");

                    Task.Run(() => HandleClient(clientSocket));
                }
            }
            finally
            {
                serverSocket.Stop();
                System.Diagnostics.Debug.WriteLine("--> Server Stopped!");
            }
        }
        private void HandleClient(TcpClient clientSocket)
        {
            try
            {
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                System.Diagnostics.Debug.WriteLine($"--> Server received a message!");

                switch (receivedMessage)
                {
                    case "ReadLogInDataSuccess":
                        LogInServerCheckSuccess();
                        break;
                    case "ReadLogInDataError":
                        LogInServerCheckError();
                        break;
                    case "InsertRegisterDataSuccess":
                        RegisterServerCheckSuccess();
                        break;
                    case "InsertRegisterDataError":
                        RegisterServerCheckError();
                        break;
                    case "InsertObservationDataSuccess":
                        ObservationInsertServerCheckSuccess();
                        break;
                    case "InsertObservationDataError":
                        ObservationInsertServerCheckError();
                        break;
                }

                byte[] responseBuffer = Encoding.UTF8.GetBytes("Server received the message");
                networkStream.Write(responseBuffer, 0, responseBuffer.Length);
            }
            finally
            {
                clientSocket.Close();
                System.Diagnostics.Debug.WriteLine("--> Client Disconnected!");
            }
        }
        public bool CheckServerConnection()
        {
            try
            {
                TcpClient client = new TcpClient("127.0.0.1", 3660);
                client.Close();
                System.Diagnostics.Debug.WriteLine("--> [Server Check Completed!]");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Server Error!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // LoginButton: 
        private void LogInServerCheckSuccess()
        {
            System.Diagnostics.Debug.WriteLine("--> [LogIN] Data was successfully read!");
        }
        private void LogInServerCheckError()
        {
            System.Diagnostics.Debug.WriteLine("--> [LogIN] Data read Error!");
        }
        public static DataTable ReadLogInData(DataBaseInfo DB, NpgsqlDataAdapter adapter, DataTable table, string enteredLogin, string enteredPassword)
        {
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM users WHERE login = @EL AND password = @EP", DB.GetConnection());
            command.Parameters.AddWithValue("@EL", enteredLogin);
            command.Parameters.AddWithValue("@EP", enteredPassword);
            adapter.SelectCommand = command;
            adapter.Fill(table);

            return table;
        }

        // RegisterButton:
        private void RegisterServerCheckSuccess()
        {
            System.Diagnostics.Debug.WriteLine("--> [Register] Data was successfully inserted!");
        }
        private void RegisterServerCheckError()
        {
            System.Diagnostics.Debug.WriteLine("--> [Register] Data insertion Error!");
        }
        public static void InsertRegisterData(NpgsqlCommand command, string enteredName, string enteredSurname, string enteredEmail, string enteredLogin, string enteredPassword)
        {
            command.Parameters.AddWithValue("@EN", enteredName);
            command.Parameters.AddWithValue("@ES", enteredSurname);
            command.Parameters.AddWithValue("@EE", enteredEmail);
            command.Parameters.AddWithValue("@EL", enteredLogin);
            command.Parameters.AddWithValue("@EP", enteredPassword);
        }

        // Observation SaveButton:
        private void ObservationInsertServerCheckSuccess()
        {
            System.Diagnostics.Debug.WriteLine("--> [Observation] Data was successfully inserted!");
        }
        private void ObservationInsertServerCheckError()
        {
            System.Diagnostics.Debug.WriteLine("--> [Observation] Data insertion Error!");
        }
        public static void InsertObservationData(NpgsqlCommand command, DateTime timestamp, string recordedBy, string observationText, string importance)
        {
            command.Parameters.AddWithValue("@Timestamp", timestamp);
            command.Parameters.AddWithValue("@RecordedBy", recordedBy);
            command.Parameters.AddWithValue("@ObservationText", observationText);
            command.Parameters.AddWithValue("@Importance", importance);
        }

        // Other logs [Beta]:
    }
}
