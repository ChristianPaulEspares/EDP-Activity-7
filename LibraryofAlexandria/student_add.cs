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

    public partial class student_add : Form
    {
        private string username;
        public student_add(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;          
        }

        private void bigLabel7_Click(object sender, EventArgs e)
        {

        }

        private void student_add_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            

            string name = name_box.Text;
            string sex = sex_box.Text;
            string cNumber = contact_box.Text;
            string studId = studId_box.Text;
            string address = address_box.Text;
            string agetext = age_box.Text;
            int age;
            double fees = 0.00;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(cNumber) ||
                string.IsNullOrWhiteSpace(studId) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(agetext))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate if quantity is a valid integer
            if (!int.TryParse(agetext, out age))
            {
                MessageBox.Show("Age must be a valid integer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {

                    // Insert the book into the database
                    string query = "INSERT INTO student (student_name, student_age, student_pnumber, student_idnum, student_address, student_sex, student_fees) " +
                        "VALUES (@name, @age, @cNumber, @studId, @address, @sex, @fees)";
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@sex", sex);
                    cmd.Parameters.AddWithValue("@studId", studId);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@cNumber", cNumber);
                    cmd.Parameters.AddWithValue("@fees", fees);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"{name} Successully added");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Registration Failed!");
                    }
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void Age_box_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
        }
    }
}
