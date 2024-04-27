using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace LibraryofAlexandria
{
    public partial class book_add : Form
    {
        private string username;
        public book_add(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
            LoadAuthors();
            LoadGenres();
        }

        private void book_add_Load(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void bigLabel8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

            if ((name_box.Text == null) || (author_box.SelectedItem == null) ||
               (genre_box.SelectedItem == null) || (date_box.Text == null) || 
               (quantity_box.Text == null) || (isbn_box.Text == null))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string title = name_box.Text;
            string author = author_box.SelectedItem.ToString();
            string category = genre_box.SelectedItem.ToString();
            string date = date_box.Text;
            string quantitytext = quantity_box.Text;
            int quantity;
            string isbn = isbn_box.Text;

            
            // Validate if quantity is a valid integer
            if (!int.TryParse(quantitytext, out quantity))
            {
                MessageBox.Show("Quantity must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
               Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    // Retrieve author ID based on selected author name
                    string authorIdQuery = "SELECT author_id FROM author WHERE author_name = @author";
                    MySqlCommand authorIdCmd = new MySqlCommand(authorIdQuery, Connection.dbConnect);
                    authorIdCmd.Parameters.AddWithValue("@author", author);
                    int authorId = Convert.ToInt32(authorIdCmd.ExecuteScalar());

                    // Retrieve genre ID based on selected genre name
                    string categoryIdQuery = "SELECT category_id FROM category WHERE category_name = @category";
                    MySqlCommand categoryIdCmd = new MySqlCommand(categoryIdQuery, Connection.dbConnect);
                    categoryIdCmd.Parameters.AddWithValue("@category", category);
                    int categoryId = Convert.ToInt32(categoryIdCmd.ExecuteScalar());

                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                    // Insert the book into the database
                    string query = "INSERT INTO books (book_title, book_published, book_number, book_author, book_quantity, book_category, date_added) VALUES (@title, @date, @isbn, @authorId, @quantity, @categoryId, @currentDate)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@date", date);
                    cmd.Parameters.AddWithValue("@isbn", isbn);
                    cmd.Parameters.AddWithValue("@authorId", authorId);
                    cmd.Parameters.AddWithValue("@quantity", quantity);
                    cmd.Parameters.AddWithValue("@categoryId", categoryId);
                    cmd.Parameters.AddWithValue("@currentDate", currentDate);
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
                    Connection.CloseConnection();              
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void LoadAuthors()
        {
            string query = "SELECT author_name FROM author";

            try
            {
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        author_box.Items.Add(reader["author_name"].ToString());
                    }

                    reader.Close();
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadGenres()
        {
            string query = "SELECT category_name FROM category";

            try
            {
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        genre_box.Items.Add(reader["category_name"].ToString());
                    }

                    reader.Close();
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
