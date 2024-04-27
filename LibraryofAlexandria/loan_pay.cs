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
    public partial class loan_pay : Form
    {
        private string username;
        public loan_pay(string username)
        {

            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string loanId = id_box.Text;
            decimal paymentAmount;

            if (!decimal.TryParse(payment_box.Text, out paymentAmount))
            {
                MessageBox.Show("Invalid payment amount. Please enter a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Retrieve loan details based on loan ID
                string loanQuery = "SELECT loan_fees, payment_status, student_id FROM loan WHERE loan_id = @loanId";
                MySqlCommand loanCmd = new MySqlCommand(loanQuery, Connection.dbConnect);
                loanCmd.Parameters.AddWithValue("@loanId", loanId);
              
                if (Connection.OpenConnection())
                {
                    using (MySqlDataReader loanReader = loanCmd.ExecuteReader())
                    {
                        if (loanReader.Read())
                        {
                            decimal currentFees = Convert.ToDecimal(loanReader["loan_fees"]);
                            string paymentstatus = loanReader["payment_status"].ToString();
                            int studentId = Convert.ToInt32(loanReader["student_id"]);

                            // Check if payment amount exceeds loan fees
                            if (paymentAmount > currentFees)
                            {
                                MessageBox.Show("Payment amount cannot exceed remaining loan fees.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Connection.CloseConnection();
                                return;
                            }

                            // Calculate remaining fees after payment
                            decimal remainingFees = Math.Max(0, currentFees - paymentAmount);

                            // Update loan status based on remaining fees
                            string updatedStatus = (remainingFees > 0) ? "Not Paid" : "Paid";                           

                            // Update loan table with new fees and status
                            string updateLoanQuery = "UPDATE loan SET loan_fees = @remainingFees, payment_status = @updatedStatus WHERE loan_id = @loanId";

                            loanReader.Close(); // Close the reader before executing another command

                            MySqlCommand updateLoanCmd = new MySqlCommand(updateLoanQuery, Connection.dbConnect);
                            updateLoanCmd.Parameters.AddWithValue("@remainingFees", remainingFees);
                            updateLoanCmd.Parameters.AddWithValue("@updatedStatus", updatedStatus);
                            updateLoanCmd.Parameters.AddWithValue("@loanId", loanId);

                            int rowsAffected = updateLoanCmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {

                                // Update student_fees in the student table
                                string updateStudentFeesQuery = "UPDATE student SET student_fees = student_fees - @paymentAmount WHERE student_id = @studentId";

                                MySqlCommand updateStudentFeesCmd = new MySqlCommand(updateStudentFeesQuery, Connection.dbConnect);
                                updateStudentFeesCmd.Parameters.AddWithValue("@paymentAmount", paymentAmount);
                                updateStudentFeesCmd.Parameters.AddWithValue("@studentId", studentId);

                                int studentRowsAffected = updateStudentFeesCmd.ExecuteNonQuery();

                                if (studentRowsAffected > 0)
                                {
                                    MessageBox.Show("Loan payment processed successfully!");
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show("Failed to update student fees.");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Failed to process loan payment.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Loan ID not found.");
                        }
                    }

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void pay_box_TextChanged(object sender, EventArgs e)
        {         
        }

        private void loan_pay_Load(object sender, EventArgs e)
        {

        }
    }
}
