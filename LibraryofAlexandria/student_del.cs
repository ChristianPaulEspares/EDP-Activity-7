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
    public partial class student_del : Form
    {
        private string username;
        public student_del(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

            private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string studIdText = del_box.Text.Trim();

            // Ensure the ID is not empty
            if (string.IsNullOrWhiteSpace(studIdText))
            {
                MessageBox.Show("Please enter a valid ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convert the ID string to an integer (assuming bookId is an integer)
            if (!int.TryParse(studIdText, out int studid))
            {
                MessageBox.Show("Invalid ID format. Please enter a valid integer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Caution! Are you sure you want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Connection.CloseConnection();
                    if (Connection.OpenConnection())
                    {
                        // Construct the SQL UPDATE query to set all fields to null
                        string query = "DELETE FROM student WHERE student_id = @studid";

                        MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                        cmd.Parameters.AddWithValue("@studid", studid);

                        // Execute the UPDATE query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Book with ID '{studid}' details have been deleted successfully!");
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show($"No record found with the student ID '{studid}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void bigLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void student_del_Load(object sender, EventArgs e)
        {

        }
    }
}
