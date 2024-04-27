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
    public partial class Book_author : Form
    {
        private string username;
        private Aboutform aboutForm;
        bool expand = false;
        public Point mouseLocation;
        public Book_author(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            this.username = username;
            UpdateLabel(username);
            UpdateUserInfo(username);
            LoadDataIntoDataGridView();
        }

        private void Book_author_Load(object sender, EventArgs e)
        {

        }

        private void bigLabel2_Click(object sender, EventArgs e)
        {

        }

        private void books_dropdown_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Book_genre genreform = new Book_genre(username);
            genreform.Show();
            this.Hide();
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void icon_sidebar_Click(object sender, EventArgs e)
        {
            Account_form Aform = new Account_form(username);
            Aform.Show();
            this.Hide();
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

        private void button5_Click(object sender, EventArgs e)
        {
            Main_form mform = new Main_form(username);
            mform.Show();
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
            string query = "SELECT * FROM author";

            try
            {
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dt.Columns["author_name"].ColumnName = "Author";


                    dataGridView1.DataSource = dt;


                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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

        

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer_dropdown.Start();
        }

        private void user_sidebar_label_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dashboard_loan loanform = new Dashboard_loan(username);
            loanform.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dashboard_student studform = new Dashboard_student(username);
            studform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard_book bookform = new Dashboard_book(username);
            bookform.Show();
            this.Hide();
        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            LoadDataIntoDataGridView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            author_edit editauthor = new author_edit(username);
            editauthor.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            author_add addauthor = new author_add(username);
            addauthor.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
