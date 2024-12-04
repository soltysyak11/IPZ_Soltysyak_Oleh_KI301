using Npgsql;
using System.Windows.Forms;
using System;

class DataBaseInfo
{
    NpgsqlConnection connection = new NpgsqlConnection("Host=localhost; Port=5432; Username=postgres; Password=gamingf17; Database=ipz");

    public void OpenConnection()
    {
        if (connection.State == System.Data.ConnectionState.Closed)
        {
            connection.Open();
        }
    }
    public void CloseConnection()
    {
        if (connection.State == System.Data.ConnectionState.Open)
        {
            connection.Close();
        }
    }
    public NpgsqlConnection GetConnection()
    {
        return connection;
    }
    public bool CheckDatabaseConnection()
    {
        try
        {
            OpenConnection(); // Спроба відкрити з'єднання
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка підключення до бази даних: " + ex.Message);
            MessageBox.Show("Data Base Error: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

    }
}
