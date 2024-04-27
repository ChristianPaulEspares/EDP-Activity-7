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
    public partial class Dashboard_loan : Form
    {
        private string username;
        private Aboutform aboutForm;
        bool expand = false;
        public Point mouseLocation;
        public Dashboard_loan(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
            UpdateLabel(username);
            UpdateUserInfo(username);
            LoadDataIntoDataGridView();
        }

        private void Dashboard_loan_Load(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
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

        private void LoadDataIntoDataGridView()
        {
            string query = @"
            SELECT l.loan_id,b.book_title AS book_id, s.student_name AS student_id,
                   l.borrowed_date, l.due_date,l.return_date,l.loan_status,l.loan_fees,l.payment_status
             FROM loan l
             INNER JOIN books b ON l.book_id = b.book_id
             INNER JOIN student s ON l.student_id = s.student_id";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dt.Columns["book_id"].ColumnName = "Book Title";
                    dt.Columns["student_id"].ColumnName = "Student";
                    dt.Columns["borrowed_date"].ColumnName = "Date Borrowed";
                    dt.Columns["due_date"].ColumnName = "Date Due";
                    dt.Columns["return_date"].ColumnName = "Date Returned";
                    dt.Columns["loan_status"].ColumnName = "Status";
                    dt.Columns["loan_fees"].ColumnName = "Loan Fees";
                    dt.Columns["payment_status"].ColumnName = "Payment Status";


                    dataGridView1.DataSource = dt;

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer_dropdown.Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main_form mform = new Main_form(username);
            mform.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dashboard_student studform = new Dashboard_student(username);
            studform.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            loan_add addloan = new loan_add(username);
            addloan.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            loan_edit editloan = new loan_edit(username);
            editloan.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            loan_del delloan = new loan_del(username);
            delloan.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            loan_pay payloan = new loan_pay(username);
            payloan.Show();
        }
    }
}
