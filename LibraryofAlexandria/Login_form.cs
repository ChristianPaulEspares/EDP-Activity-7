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
    public partial class Login_form : Form
    {
        public Point mouseLocation;
        public Login_form()
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
        }

        private void Login_form_Load(object sender, EventArgs e)
        {

        }

        private void crownLabel1_Click(object sender, EventArgs e)
        {

        }

        private void nightLabel2_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void hopePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void foxLabel1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel1_Click(object sender, EventArgs e)
        {

        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void foxTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void foxLabel2_Click(object sender, EventArgs e)
        {

        }

        private void foxTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            Recover_form ReForm = new Recover_form();
            ReForm.Show();
            this.Hide();
        }

        private void airButton1_Click(object sender, EventArgs e)
        {
            string username = user_box.Text;
            string password = password_box.Text;

            if ( username == "" || password == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {


                if (Connection.OpenConnection())
                {
                    // Check if the provided username and password match
                    string query = "SELECT COUNT(*) FROM users WHERE user_name = @username AND user_password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    if (count > 0)
                    {
                        string updateQuery = "UPDATE users SET user_status = 'Active' WHERE user_name = @username";
                        MySqlCommand updateCmd = new MySqlCommand(updateQuery, Connection.dbConnect);
                        updateCmd.Parameters.AddWithValue("@username", username);
                        int rowsAffected = updateCmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Login Successful!");
                            Main_form mform = new Main_form(username);
                            mform.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update user status!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    Connection.CloseConnection();
                }
            }
        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            Register_form rForm = new Register_form();
            rForm.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void mouse_down(object sender, MouseEventArgs e)
        {
            mouseLocation = new Point(-e.X, -e.Y);
        }

        private void mouse_move(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point mousePose = Control.MousePosition;
                mouseLocation.Offset(mouseLocation.X, mouseLocation.Y);
                Location = mousePose;
            }
        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            password_box.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }
    }
}
