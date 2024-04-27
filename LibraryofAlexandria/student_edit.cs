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
    public partial class student_edit : Form
    {
        private string username;
        public student_edit(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            this.username = username;
        }

        private void student_edit_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string id = id_box.Text;
            string name = name_box.Text;
            string sex = sex_box.Text;
            string cNumber = contact_box.Text;
            string studId = studId_box.Text;
            string address = address_box.Text;
            string agetext = age_box.Text;
            int age;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(sex) ||
                string.IsNullOrWhiteSpace(cNumber) ||
                string.IsNullOrWhiteSpace(studId) ||
                string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(agetext)  ||
                string.IsNullOrWhiteSpace(id))
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

                    string query = @"UPDATE student 
                                        SET student_name = @name, 
                                            student_age = @age, 
                                            student_pnumber = @cNumber, 
                                            student_idnum = @studId, 
                                            student_address = @address, 
                                            student_sex = @sex 
                                        WHERE student_id = @id";

                    // Update the student into the database                  
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@sex", sex);
                    cmd.Parameters.AddWithValue("@studId", studId);
                    cmd.Parameters.AddWithValue("@address", address);
                    cmd.Parameters.AddWithValue("@age", age);
                    cmd.Parameters.AddWithValue("@cNumber", cNumber);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Student updated successfully!");
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

        private void id_box_TextChanged(object sender, EventArgs e)
        {
            // When ID is entered or changed, load corresponding book details
            string studId = id_box.Text;
            if (!string.IsNullOrWhiteSpace(studId))
            {
                LoadStudentDetails(studId);
            }
        }

        private void LoadStudentDetails(string studId)
        {
            string query = "SELECT * FROM student WHERE student_id = @id";

            try
            {
                if (Connection.OpenConnection())
                {

                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    cmd.Parameters.AddWithValue("@id", studId);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                         
                            // Populate the textboxes with retrieved data
                            name_box.Text = reader["student_name"].ToString();
                            sex_box.Text = reader["student_sex"].ToString();
                            contact_box.Text = reader["student_pnumber"].ToString();
                            studId_box.Text = reader["student_idnum"].ToString();
                            address_box.Text = reader["student_address"].ToString();
                            age_box.Text = reader["student_age"].ToString();
                        }
                        reader.Close();
                    }
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
