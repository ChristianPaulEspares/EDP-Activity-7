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
using MySql.Data.MySqlClient;

namespace LibraryofAlexandria
{
    public partial class account_edit : Form
    {

        private string username;
        public account_edit(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            this.username = username;
        }

        

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void photo_button_Click(object sender, EventArgs e)
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

        private void account_edit_Load(object sender, EventArgs e)
        {
            // Display the photo corresponding to the username from the database
            string selectQuery = "SELECT user_profile FROM users WHERE user_name = @username";
            using (MySqlConnection connection = Connection.dbConnect)
            {
                using (MySqlCommand cmd = new MySqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    try
                    {
                        Connection.OpenConnection();
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            byte[] profilePictureBytes = (byte[])result;
                            icon_box.Image = byteArrayToImage(profilePictureBytes);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        Connection.CloseConnection();
        }

        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
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

        private void button6_Click(object sender, EventArgs e)
        {
            string newuser = user_box.Text;
            string newEmail = email_box.Text;
            byte[] newProfilePicture = ImageToByteArray(icon_box.Image);

            // Update database with new email and picture
            string updateQuery = "UPDATE users SET user_email = @newEmail, user_profile = @newProfilePicture, user_name = @newuser WHERE user_name = @username";
            
            // Check if any required field is empty
            if (string.IsNullOrWhiteSpace(newuser) || string.IsNullOrWhiteSpace(newEmail))
            {
                MessageBox.Show("Please fill in all the required fields.");
                return; // Exit the method without further processing
            }

            try
              {
                if (Connection.OpenConnection()) 
                    {
                    MySqlCommand cmd = new MySqlCommand(updateQuery, Connection.dbConnect);

                    cmd.Parameters.AddWithValue("@newEmail", newEmail);
                    cmd.Parameters.AddWithValue("@newProfilePicture", newProfilePicture);
                    cmd.Parameters.AddWithValue("@newuser", newuser);  // Use newUsername parameter
                    cmd.Parameters.AddWithValue("@username", username);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                        {
                        MessageBox.Show("Profile updated successfully!");
                        DialogResult = DialogResult.OK; // Close the form with OK result
                        username = newuser;
                        Account_form acform = new Account_form(username);
                        acform.Show();
                        this.Hide();
                        Close();
                        }
                    else
                        {
                        MessageBox.Show("Failed to update profile!");
                        }
                }
             }
             catch (Exception ex)
                 {
                        MessageBox.Show("Error: " + ex.Message);
                 }

         Connection.CloseConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Account_form acform = new Account_form(username);
            acform.Show();
            Close();
        }

        private void user_box_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
