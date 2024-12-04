using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace LAB2
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!InitializeDatabaseAndServer())
            {
                Environment.Exit(1);
            }

            Application.Run(new Log_In());
        }

        private static bool InitializeDatabaseAndServer()
        {
            // �������� ���� �����
            DataBaseInfo DBCheck = new DataBaseInfo();
            if (!DBCheck.CheckDatabaseConnection())
            {
                Console.WriteLine("������� ���������� �� ���� �����.");
                return false;
            }

            // ������ �������
            ServerInfo myServer = new ServerInfo();
            Task.Run(() => myServer.Server());

            // �������� �������
            if (!myServer.CheckServerConnection())
            {
                Console.WriteLine("������� ���������� �� �������.");
                return false;
            }

            return true;
        }

    }
}