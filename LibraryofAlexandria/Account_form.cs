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
    public partial class Account_form : Form
    {
        private string username;
        private Aboutform aboutForm;

        public Account_form(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            this.username = username;
            UpdateLabel(username);
            UpdateUserInfo(username);
            DisplayUserInfo();

        }


        private void Account_form_Load(object sender, EventArgs e)
        {

        }

        private void user_sidebar_label_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            if (aboutForm == null || aboutForm.IsDisposed)
            {
                aboutForm = new Aboutform(this);
                aboutForm.Show();
                this.Hide();
            }
            else
            {
                aboutForm.Focus();
            }
        }

        private void sidebar_dashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer_dropdown_Tick(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Click(object sender, EventArgs e)
        {

        }

        private void icon_user_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Connection.CloseConnection();
            Main_form mform = new Main_form(username);
            mform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string verification = GetUserVerification(username);
            MessageBox.Show("Security Code: " + verification, "Security Code", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void photo_button_Click(object sender, EventArgs e)
        {
            account_edit acform = new account_edit(username);
            acform.Show();
            this.Hide();
        }

        private void user_label_Click(object sender, EventArgs e)
        {

        }

        private void email_label_Click(object sender, EventArgs e)
        {

        }

        private void search_box_TextChanged(object sender, EventArgs e)
        {
            string searchText = search_box.Text.Trim();
            string query = "SELECT user_name, user_email, date_added, user_status, user_last_online FROM users WHERE user_name LIKE @searchText";
            try
            {
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@searchText", $"%{searchText}%");
                    MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    data.Fill(dt);

                    dataGridView1.DataSource = dt;
          
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
         Connection.CloseConnection();
        }

        private void bigLabel5_Click(object sender, EventArgs e)
        {

        }

        private void status_label_Click(object sender, EventArgs e)
        {

        }

        private void status_text_Click(object sender, EventArgs e)
        {

        }

        private void user_text_Click(object sender, EventArgs e)
        {

        }

        private void UpdateLabel(string username)
        {
            user_text.Text = username;
        }

        private void UpdateUserInfo(string username)
        {
            string query = "SELECT user_email, user_status, user_profile FROM users WHERE user_name = @username";

            try
            {
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@username", username);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        // Update UI elements with fetched data
                        email_text.Text = reader["user_email"].ToString();
                        status_text.Text = reader["user_status"].ToString();
                        // Assuming photo is stored as byte[] in database and displayed in a PictureBox
                        byte[] photoBytes = (byte[])reader["user_profile"];
                        icon_user.Image = byteArrayToImage(photoBytes);
                    }
                }
             }
             catch (Exception ex)
               {
                 MessageBox.Show("Error: " + ex.Message);
               }

            Connection.CloseConnection();
        }

        // Convert byte array to Image
        private Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }

        private string GetUserVerification(string username)
        {
            string verification = "";
            string query = "SELECT user_verification FROM users WHERE user_name = @username";
           
             try
             {
                if (Connection.OpenConnection())
                {
                MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                cmd.Parameters.AddWithValue("@username", username);
                object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        verification = result.ToString();
                    }
                }
             }
             catch (Exception ex)
             {
                 MessageBox.Show("Error: " + ex.Message);
             }
         Connection.CloseConnection();
         return verification;

        }

        private void DisplayUserInfo()
        {
           
            string query = "SELECT user_name, user_email, date_added, user_status, user_last_online FROM users";


            try
              {
                if (Connection.OpenConnection())
                {
                 MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                 MySqlDataAdapter data = new MySqlDataAdapter(cmd);
                 DataTable dt = new DataTable();
                 data.Fill(dt);

                 dataGridView1.DataSource = dt;
                }
              }

             catch (Exception ex)
               {
               MessageBox.Show("Error: " + ex.Message);
               }
            Connection.CloseConnection();
        }
        
        private void email_text_Click(object sender, EventArgs e)
        {

        }
    }
}
