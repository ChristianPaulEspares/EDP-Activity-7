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
    public partial class loan_edit : Form
    {
        private string username;
        public loan_edit(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;         
        }

        private void bigLabel4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void loan_edit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void LoadloanDetails(string loanId)
        {
            string query = @"SELECT l.loan_id, b.book_title, s.student_name 
                                FROM loan l
                                INNER JOIN books b ON l.book_id = b.book_id
                                INNER JOIN student s ON l.student_id = s.student_id
                             WHERE l.loan_id = @loanId";

            try
            {
                if (Connection.OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@loanId", loanId);


                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Populate the textboxes with retrieved data
                            book_box.Text = reader["book_title"].ToString();
                            student_box.Text = reader["student_name"].ToString();
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

        private void button6_Click(object sender, EventArgs e)
        {
            if ((book_box.Text == null) || (student_box.Text == null) || (id_box.Text == null))
            {
                MessageBox.Show("Please select both a book and a student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string id = id_box.Text;
            string book = book_box.Text;
            string student = student_box.Text;            
            string returnbook = returndate_picker.Text;

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    // Retrieve author ID based on selected author name
                    string bookIdquery = "SELECT book_id FROM books WHERE book_title = @book";
                    MySqlCommand bookIdCmd = new MySqlCommand(bookIdquery, Connection.dbConnect);
                    bookIdCmd.Parameters.AddWithValue("@book", book);
                    int bookId = Convert.ToInt32(bookIdCmd.ExecuteScalar());

                    // Retrieve student ID based on selected student name
                    string studentIdquery = "SELECT student_id FROM student WHERE student_name = @student";
                    MySqlCommand studentIdCmd = new MySqlCommand(studentIdquery, Connection.dbConnect);
                    studentIdCmd.Parameters.AddWithValue("@student", student);
                    int studentId = Convert.ToInt32(studentIdCmd.ExecuteScalar());

                    // Update the book in the database based on book ID
                    string query = @"UPDATE loan 
                                        SET book_id = @bookId, 
                                            student_id = @studentId,                                         
                                            return_date = @returnbook                                           
                                        WHERE loan_id = @id";

                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    cmd.Parameters.AddWithValue("@studentId", studentId);;
                    cmd.Parameters.AddWithValue("@returnbook", returnbook);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Retrieve updated loan data including the due_date
                        string selectLoanQuery = "SELECT due_date FROM loan WHERE loan_id = @id";
                        MySqlCommand selectLoanCmd = new MySqlCommand(selectLoanQuery, Connection.dbConnect);
                        selectLoanCmd.Parameters.AddWithValue("@id", id);

                        // Get the due_date from database
                        object dueDateObj = selectLoanCmd.ExecuteScalar();
                        if (dueDateObj != null && dueDateObj != DBNull.Value)
                        {
                            DateTime dueDate = Convert.ToDateTime(dueDateObj);
                            DateTime returnDate = DateTime.Parse(returnbook);
                            string loanStatus = (returnDate > dueDate) ? "Overdue" : "Returned";
                            string paymentStatus = (dueDate >= returnDate) ? "No Payment" : "Not Paid";


                            // Update loan status in the loan table
                            string updateStatusQuery = "UPDATE loan SET loan_status = @status, payment_status = @paystatus WHERE loan_id = @id";
                            MySqlCommand updateStatusCmd = new MySqlCommand(updateStatusQuery, Connection.dbConnect);
                            updateStatusCmd.Parameters.AddWithValue("@paystatus", paymentStatus);
                            updateStatusCmd.Parameters.AddWithValue("@status", loanStatus);
                            updateStatusCmd.Parameters.AddWithValue("@id", id);
                            updateStatusCmd.ExecuteNonQuery();

                            decimal feeRatePerDay = 3.00m;
                            decimal fees = CalculateLoanFees(dueDate, returnDate, feeRatePerDay);

                            // Update loan_fees in the loan table
                            string updateFeesQuery = "UPDATE loan SET loan_fees = loan_fees + @fees WHERE loan_id = @id";
                            MySqlCommand updateFeesCmd = new MySqlCommand(updateFeesQuery, Connection.dbConnect);
                            updateFeesCmd.Parameters.AddWithValue("@fees", fees);
                            updateFeesCmd.Parameters.AddWithValue("@id", id);
                            updateFeesCmd.ExecuteNonQuery();
                         
                            // Update student_fees in the book table
                            string updateBookquantityQuery = "UPDATE books SET book_quantity = book_quantity - @newQuantity WHERE book_id = @bookId";
                            MySqlCommand updateBookCmd = new MySqlCommand(updateBookquantityQuery, Connection.dbConnect);
                            updateBookCmd.Parameters.AddWithValue("@newQuantity", 1); // Adjust book quantity here
                            updateBookCmd.Parameters.AddWithValue("@bookId", bookId);                           

                            // Update student_fees in the student table
                            string updateStudentFeesQuery = "UPDATE student SET student_fees = student_fees + @fees WHERE student_id = @studentId";
                            MySqlCommand updateStudentFeesCmd = new MySqlCommand(updateStudentFeesQuery, Connection.dbConnect);
                            updateStudentFeesCmd.Parameters.AddWithValue("@fees", fees);
                            updateStudentFeesCmd.Parameters.AddWithValue("@studentId", studentId);
                            int studentRowsAffected = updateStudentFeesCmd.ExecuteNonQuery();

                            if (studentRowsAffected > 0)
                            {                            

                                MessageBox.Show("Loan updated successfully!");
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Failed to update student fees.");
                            }
                         
                        }
                        else
                        {
                            MessageBox.Show("Due date not found for loan ID " + id);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Failed to update loan!");
                    }

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message);
            }
        }     

        private decimal CalculateLoanFees(DateTime dueDate, DateTime returnDate, decimal feeRatePerDay)
        {
            // Calculate the difference in days between returnDate and dueDate
            TimeSpan difference = returnDate - dueDate;
            int daysOverdue = difference.Days;

            // Calculate the fees based on the number of overdue days and the fee rate per day
            decimal fees = daysOverdue * feeRatePerDay;

            if (fees < 0)
            {
                fees = 0;  // Set fees to zero if it's negative
            }

            // Return the calculated fees
            return fees;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // When ID is entered or changed, load corresponding book details
            string loanId = id_box.Text;
            if (!string.IsNullOrWhiteSpace(loanId))
            {
                LoadloanDetails(loanId);
            }
        }
        
    }
}
