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
    public partial class author_add : Form
    {
        private string username;
        public author_add(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void author_add_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (author_box.Text == null)
            {
                MessageBox.Show("Please fill in the required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string author = author_box.Text;

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    string query = "INSERT INTO author (author_name) VALUES (@author)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@author", author);
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
