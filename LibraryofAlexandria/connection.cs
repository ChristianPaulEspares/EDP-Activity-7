using System;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibraryofAlexandria
{
  public class Connection
    {
        public static MySqlConnection dbConnect;
        private string server;
        private string database;
        private string uid;
        private string password;

        public void Connect()
        {
            server = "127.0.0.1"; // Or the IP address of your MySQL server
            database = "libraryofalexandrea"; // Corrected typo
            uid = "root";
            password = "@MYSQL12345q";
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};";
            dbConnect = new MySqlConnection(connectionString);
        }

        public static bool OpenConnection()
        {
            try
            {
                dbConnect.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        public static bool CloseConnection()
        {
            try
            {
                dbConnect.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }
    }
}
