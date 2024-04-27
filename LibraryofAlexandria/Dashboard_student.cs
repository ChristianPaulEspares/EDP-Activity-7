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
    public partial class Dashboard_student : Form
    {
        private string username;
        private Aboutform aboutForm;
        bool expand = false;
        public Point mouseLocation;
        public Dashboard_student(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
            UpdateLabel(username);
            UpdateUserInfo(username);
            LoadDataIntoDataGridView();
        }

        private void Dashboard_student_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            student_del delstudent = new student_del(username);
            delstudent.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            student_edit editstudent = new student_edit(username);
            editstudent.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bigLabel4_Click(object sender, EventArgs e)
        {

        }

        private void sidebar_dashboard_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bigLabel3_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            student_add addstudent = new student_add(username);
            addstudent.Show();
        }

        private void timer_dropdown_Tick(object sender, EventArgs e)
        {
            if (expand == false)
            {
                books_dropdown.Height += 15;
                if (books_dropdown.Height >= books_dropdown.MaximumSize.Height)
                {
                    timer_dropdown.Stop();
                    expand = true;
                }
            }
            else
            {
                books_dropdown.Height -= 15;
                if (books_dropdown.Height <= books_dropdown.MinimumSize.Height)
                {
                    timer_dropdown.Stop();
                    expand = false;
                }
            }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer_dropdown.Start();
        }

        private void icon_sidebar_Click(object sender, EventArgs e)
        {
            Account_form Aform = new Account_form(username);
            Aform.Show();
            this.Hide();
        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Book_genre genreform = new Book_genre(username);
            genreform.Show();
            this.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Book_author authorform = new Book_author(username);
            authorform.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SetUserStatusInactive(username);
            Application.Exit();
        }

        private void UpdateLabel(string username)
        {
            user_sidebar_label.Text = username;
        }
        private void UpdateUserInfo(string username)
        {

            string query = "SELECT user_profile FROM users WHERE user_name = @username";
            MySqlConnection connection = Connection.dbConnect;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);

            try
            {
                Connection.OpenConnection();
                MySqlDataReader reader = cmd.ExecuteReader();
                {
                    if (reader.Read())
                    {
                        // Assuming photo is stored as byte[] in database and displayed in a PictureBox
                        byte[] photoBytes = (byte[])reader["user_profile"];
                        icon_sidebar.Image = byteArrayToImage(photoBytes);
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

        private void SetUserStatusInactive(string username)
        {
            string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string query = "UPDATE users SET user_status = 'Inactive', user_last_online = @lastOnline WHERE user_name = @username";
            MySqlConnection connection = Connection.dbConnect;
            MySqlCommand cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@lastOnline", currentTime);

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Thank you, Good Bye.");
                        Connection.CloseConnection();
                    }
                    else
                    {
                        MessageBox.Show("Failed to update user status.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                // Make sure to close the connection regardless of whether an exception occurred or not
                Connection.CloseConnection();
            }
        }

        private void LoadDataIntoDataGridView()
        {
            string query = "SELECT * FROM student";

            try
            {
             Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                  
                    dt.Columns["student_name"].ColumnName = "Student";
                    dt.Columns["student_age"].ColumnName = "Age";
                    dt.Columns["student_pnumber"].ColumnName = "Contact Number";
                    dt.Columns["student_idnum"].ColumnName = "Learner's ID";
                    dt.Columns["student_address"].ColumnName = "Address";
                    dt.Columns["student_sex"].ColumnName = "Sex";
                    dt.Columns["student_fees"].ColumnName = "Overdue fees";
                    



                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main_form mform = new Main_form(username);
            mform.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dashboard_loan loanform = new Dashboard_loan(username);
            loanform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard_book bookform = new Dashboard_book(username);
            bookform.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }
    }
}
