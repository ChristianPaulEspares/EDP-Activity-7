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
    
    public partial class Newpassword_form : Form
    {
        private string email;
        private string username;
        public Newpassword_form(string email, string username)
        {
            InitializeComponent();
            this.email = email;
            this.username = username;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void New_password_form_Load(object sender, EventArgs e)
        {

        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            string newPassword = password_box.Text;
            string confirmPassword = newpass_box.Text;

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Update the password in the database
            if (Connection.OpenConnection())
            {
                string query = "UPDATE users SET user_password = @newPassword WHERE user_email = @email AND user_name = @username";
                MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@username", username);

                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password changed successfully.");
                    Login_form lform = new Login_form();
                    lform.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to change password. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Connection.CloseConnection();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
