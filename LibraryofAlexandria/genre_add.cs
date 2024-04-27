using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibraryofAlexandria
{
    public partial class genre_add : Form
    {
        private string username;
        public genre_add(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void genre_add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (genre_box.Text == null)
            {
                MessageBox.Show("Please fill in the required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string genre = genre_box.Text;

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    string query = "INSERT INTO category (category_name) VALUES (@genre)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@genre", genre);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successully added");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Registration Failed!");
                    }                    
                }
                Connection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
