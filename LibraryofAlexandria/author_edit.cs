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
    public partial class author_edit : Form
    {
        private string username;
        public author_edit(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel4_Click(object sender, EventArgs e)
        {

        }

        private void author_edit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (name_box.Text == null || (id_box.Text == null)
             )
            {
                MessageBox.Show("Please fill in the required field.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string id = id_box.Text;
            string author = name_box.Text;

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    string query = "UPDATE author SET author_name = @author WHERE author_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@author", author);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Successully Updated");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Update Failed!");
                    }
                }
                Connection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void id_box_TextChanged(object sender, EventArgs e)
        {
            // When ID is entered or changed, load corresponding book details
            string authorid = id_box.Text;
            if (!string.IsNullOrWhiteSpace(authorid))
            {
                LoadauthorDetails(authorid);
            }
        }

        private void LoadauthorDetails(string authorid)
        {
            string query = "SELECT * FROM author WHERE author_id = @authorid";

            try
            {
                if (Connection.OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@authorid", authorid);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the textboxes with retrieved data
                            name_box.Text = reader["author_name"].ToString();
                        }
                        reader.Close();
                    }
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
