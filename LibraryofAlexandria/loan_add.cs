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
    public partial class loan_add : Form
    {
        private string username;
        public loan_add(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
            LoadBooks();
            LoadStudent();

        }

        private void bigLabel4_Click(object sender, EventArgs e)
        {

        }

        private void loan_add_Load(object sender, EventArgs e)
        {
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((book_box.SelectedItem == null) || (student_box.SelectedItem == null))
            {
                MessageBox.Show("Please select both a book and a student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string book = book_box.SelectedItem.ToString();
            string student = student_box.SelectedItem.ToString();
            string borrowbook = borrowdate_picker.Text;
            string issuebook = Issueddate_picker.Text;
            double fees = 0.00;
            string placeholder = "Pending";

            try
            {               
                if (Connection.OpenConnection())
                {
                    // Retrieve book ID based on selected book title
                    string bookIdquery = "SELECT book_id FROM books WHERE book_title = @book";
                    MySqlCommand bookIdCmd = new MySqlCommand(bookIdquery, Connection.dbConnect);
                    bookIdCmd.Parameters.AddWithValue("@book", book);
                    int bookId = Convert.ToInt32(bookIdCmd.ExecuteScalar());

                    // Retrieve student ID based on selected student name
                    string studentIdquery = "SELECT student_id FROM student WHERE student_name = @student";
                    MySqlCommand studentIdCmd = new MySqlCommand(studentIdquery, Connection.dbConnect);
                    studentIdCmd.Parameters.AddWithValue("@student", student);
                    int studentId = Convert.ToInt32(studentIdCmd.ExecuteScalar());

                    // Insert the student into the database
                    string query = "INSERT INTO loan (book_id, student_id, borrowed_date, due_date, loan_fees, loan_status) VALUES (@bookId, @studentId, @borrowbook, @issuebook, @fees, @placeholder)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    cmd.Parameters.AddWithValue("@studentId", studentId);
                    cmd.Parameters.AddWithValue("@borrowbook", borrowbook);
                    cmd.Parameters.AddWithValue("@issuebook", issuebook);
                    cmd.Parameters.AddWithValue("@placeholder", placeholder);
                    cmd.Parameters.AddWithValue("@fees", fees);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Loan Successully added");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Loan adding Failed!");
                    }                   
                }
             Connection.CloseConnection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error3: " + ex.Message);
            }

        }

        private void LoadBooks()
        {
            string query = "SELECT book_title FROM books";

            try
            {             
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            book_box.Items.Add(reader["book_title"].ToString());
                        }

                        reader.Close();
                    }
                 Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message);
            }
        }

        private void LoadStudent()
        {
            string query = "SELECT student_name FROM student";

            try
            {            
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student_box.Items.Add(reader["student_name"].ToString());
                        }
                        reader.Close();
                    }
                 Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error2: " + ex.Message);
            }
        }
    }
}
