using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace LibraryofAlexandria
{

  
    public partial class Register_form : Form
    {
        public Point mouseLocation;

        public Register_form()
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
        }

        private void Recoverform_Load(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login_form lForm = new Login_form();
            lForm.Show();
            this.Hide();
        }

        private void panel2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            string email = email_box.Text;
            string username = user_box.Text;
            string password = pass_box.Text;
            string key = GenerateRandomKey(11);
            byte[] newProfilePicture = ImageToByteArray(icon_box.Image);



            if (email == "" || username == "" || password == "")
            {
                MessageBox.Show("Please fill all blank fields", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }
            else
            {

                if (Connection.OpenConnection())
                {
                    // Check if the username already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE user_name = @username";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, Connection.dbConnect);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    if (count > 0)
                    {
                        MessageBox.Show("Username already taken!");
                        Connection.CloseConnection();
                        return;
                    }


                }

                // If the username is available, proceed with registration
                string query = "INSERT INTO users (user_email, user_name, user_password, date_added, user_verification,user_profile) " +
                               "VALUES (@email, @username, @password, NOW(), @key, @newProfilePicture)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@key", key);
                    cmd.Parameters.AddWithValue("@newProfilePicture", newProfilePicture);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration Successful! Your reset key is: " + key + " use for account recovery");
                        Login_form lForm = new Login_form();
                        lForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Registration Failed!");
                    }

                    Connection.CloseConnection();
            }
        }

        private string GenerateRandomKey(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void foxLabel2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void foxLabel3_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void foxLabel1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void nightControlBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void hopePictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
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

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void bigLabel1_Click(object sender, EventArgs e)
        {

        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            pass_box.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void airButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg, *.jpeg, *.png, *.gif, *.bmp)|*.jpg; *.jpeg; *.png; *.gif; *.bmp|All Files (*.*)|*.*";
            openFileDialog.Title = "Select a photo";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = openFileDialog.FileName;
                // Display the selected photo in the picture box
                icon_box.Image = Image.FromFile(selectedFilePath);
            }
        }

        private byte[] ImageToByteArray(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
