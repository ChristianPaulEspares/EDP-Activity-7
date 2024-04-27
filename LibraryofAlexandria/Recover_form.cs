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
    public partial class Recover_form : Form
    {
        public Point mouseLocation;
        public Recover_form()
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void foxLabel1_Click(object sender, EventArgs e)
        {

        }

        private void foxLabel2_Click(object sender, EventArgs e)
        {

        }

        private void airButton1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void code_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            string email = email_box.Text;
            string username = user_box.Text;
            string code = code_box.Text;

            if (username == "" || email == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else{   
                if (Connection.OpenConnection())
                {
                    // Check if the provided email and username match
                    string query = "SELECT COUNT(*) FROM users WHERE user_email = @email AND user_name = @username";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@username", username);

                    int countUser = Convert.ToInt32(cmd.ExecuteScalar());
                    if (countUser > 0)
                    {
                        // Email and username are valid, now check the code
                        string codeQuery = "SELECT COUNT(*) FROM users WHERE user_email = @email AND user_name = @username AND user_verification = @code";
                        MySqlCommand codeCmd = new MySqlCommand(codeQuery, Connection.dbConnect);
                        codeCmd.Parameters.AddWithValue("@email", email);
                        codeCmd.Parameters.AddWithValue("@username", username);
                        codeCmd.Parameters.AddWithValue("@code", code);

                        int countCode = Convert.ToInt32(codeCmd.ExecuteScalar());
                        if (countCode > 0)
                        {
                            // Verification code is valid
                            MessageBox.Show("Verification Successful! You can now change your password.");

                            // Navigate to the change password form
                            Newpassword_form Newform = new Newpassword_form(email, username);
                            Newform.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid verification code.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or username.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Connection.CloseConnection();
                }
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login_form lForm = new Login_form();
            lForm.Show();
            this.Hide();
        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
