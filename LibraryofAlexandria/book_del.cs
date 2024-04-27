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
    public partial class book_del : Form
    {
        private string username;
        public book_del(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void book_del_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string bookIdText = del_box.Text.Trim();

            // Ensure the ID is not empty
            if (string.IsNullOrWhiteSpace(bookIdText))
            {
                MessageBox.Show("Please enter a valid ID to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Convert the ID string to an integer (assuming bookId is an integer)
            if (!int.TryParse(bookIdText, out int bookId))
            {
                MessageBox.Show("Invalid ID format. Please enter a valid integer ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Caution! Are you sure you want to delete the book with this ID?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    Connection.CloseConnection();
                    if (Connection.OpenConnection())
                    {                      

                        // Construct the SQL DELETE query
                        string query = "DELETE FROM books WHERE book_id = @bookId";

                        MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                        cmd.Parameters.AddWithValue("@bookId", bookId);

                        // Execute the DELETE query
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Book with ID '{bookId}' has been deleted successfully!");
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show($"No record found with the book ID '{bookId}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
}
