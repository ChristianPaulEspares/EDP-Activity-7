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
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace LibraryofAlexandria
{
    public partial class Report_form : Form
    {
        private string username;
        private Aboutform aboutForm;
        bool expand = false;
        public Report_form(string username)
        {
            InitializeComponent();
            Connection connection = new Connection();
            connection.Connect();
            UpdateLabel(username);
            UpdateUserInfo(username);
            this.username = username;
        }

        private void nightControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void Report_form_Load(object sender, EventArgs e)
        {


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bigLabel4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string templatePath = @"D:\3rd_year_2nd_sem\IT_120 Event Driven Programming\resource_page\template LOA.xlsx";
                DataTable dataTableToExport = null;
                Image chartImage = null;
                // Determine which data to export based on the current DataGridView content
                if (ContainsOverdueBooks())
                {
                    dataTableToExport = LoanOverdueloan();
                    chartImage = CreateLoanOverdueChart();
                    // Assuming LoanOverdueLoanFiltered() fetches the overdue loan data
                }
                else if (ContainsBorrowedBooks())
                {
                    dataTableToExport = LoanBorrowbook();
                    chartImage = CreateLoanBorrowChart();
                    // Assuming LoadBorrowedBooks() fetches the borrowed books data
                }
                else if (ContainsInventoryCount())
                {
                    dataTableToExport = LoadDataInventory();
                    chartImage = CreateDataInventoryChart();
                    // Assuming LoadInventoryCount() fetches the inventory count data
                }
                // Add more conditions as needed for other types of data views

                // Export the selected data to Excel using the template
                if (dataTableToExport != null && dataTableToExport.Rows.Count > 0 && chartImage != null)
                {
                    // Export DataTable to Sheet1
                    ExportToExcel(templatePath, dataTableToExport);

                    // Export Image to Sheet2
                    if (chartImage != null)
                    {
                        ExportChartToExcel(templatePath, chartImage);
                    }
                    else
                    {
                        MessageBox.Show("No chart image available for export.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to Excel: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ContainsBorrowedBooks()
        {
            // Check if the DataGridView contains a column named "Status" with the header text "Status" and rows with "Pending" status
            return (dataGridView1.Columns.Contains("Status") &&
                    dataGridView1.Columns["Status"].HeaderText == "Status" &&
                    ContainsStatusValue("Pending"));
        }

        private bool ContainsOverdueBooks()
        {
            // Check if the DataGridView contains a column named "Status" with the header text "Status" and rows with "Overdue" status
            return (dataGridView1.Columns.Contains("Status") &&
                    dataGridView1.Columns["Status"].HeaderText == "Status" &&
                    ContainsStatusValue("Overdue"));
        }

        private bool ContainsInventoryCount()
        {
            // Check if the DataGridView contains columns corresponding to inventory data (e.g., "Book Title", "Author", "Genre", "Quantity", "Date Added")
            return (dataGridView1.Columns.Contains("Book Title") &&
                    dataGridView1.Columns.Contains("Author") &&
                    dataGridView1.Columns.Contains("Genre") &&
                    dataGridView1.Columns.Contains("Quantity") &&
                    dataGridView1.Columns.Contains("Date Added"));
        }

        // Helper method to check if the specified status value exists in the "Status" column
        private bool ContainsStatusValue(string status)
        {
            if (dataGridView1.DataSource is DataTable dataTable)
            {
                foreach (DataRow row in dataTable.Rows)
                {
                    if (row["Status"].ToString() == status)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ExportToExcel(string templatePath, DataTable dataTable)
        {
            // Create Excel application instance
            Excel.Application excelApp = new Excel.Application();
            Excel.Workbook workbook = excelApp.Workbooks.Open(templatePath);
            Excel.Worksheet worksheet = workbook.Sheets[1]; // First sheet for table         

            try
            {
                int startRow = 9; // Start printing data from row 9
                int startCol = 1; // Start printing data from column 2 (B)

                // Create a table (ListObject) in Excel starting from row 9, column 2
                Excel.Range tableRange = worksheet.Range[worksheet.Cells[startRow, startCol], worksheet.Cells[startRow, startCol + dataTable.Columns.Count - 1]];
                Excel.ListObject table = worksheet.ListObjects.AddEx(Excel.XlListObjectSourceType.xlSrcRange, tableRange, Type.Missing, Excel.XlYesNoGuess.xlYes);
                table.Name = "DataTableTable";

                // Set table headers based on DataTable column headers
                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    table.ListColumns[col + 1].Name = dataTable.Columns[col].ColumnName;
                }

                // Populate the table with data from DataTable
                int row = startRow + 1; // Start populating data from the next row
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    for (int col = 0; col < dataTable.Columns.Count; col++)
                    {
                        // Insert data into the table cell by cell
                        table.ListRows.Add(Type.Missing);
                        worksheet.Cells[row, startCol + col].Value = dataRow[col];
                    }
                    row++;
                }



                // Generate base filename based on current date
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                string baseFilename = $"{currentDate}_report";
                string outputPath = GetUniqueFilename(baseFilename, "xlsx");

                // Save the filled template as a new file with unique filename
                workbook.SaveAs(outputPath);

                MessageBox.Show($"Data exported successfully to: {outputPath}", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting data to Excel: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up Excel objects
                // Clean up Excel objects
                workbook.Close();
                excelApp.Quit();
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                ReleaseObject(excelApp);
            }
        }

        private void ExportChartToExcel(string templatePath, Image chartImage)
        {
            Excel.Application excelApp = null;
            Excel.Workbook workbook = null;
            Excel.Worksheet chartSheet = null;

            try
            {
                // Create a new Excel application instance
                excelApp = new Excel.Application();
                workbook = excelApp.Workbooks.Add(); // Create a new workbook (not based on a template)

                // Create a new worksheet for the chart image
                chartSheet = workbook.Sheets.Add();
                chartSheet.Name = "ChartSheet";

                // Insert the chart image into the worksheet
                Clipboard.SetImage(chartImage);
                chartSheet.Paste(chartSheet.Range["A1"]);

                // Generate base filename based on current date
                string currentDate = DateTime.Now.ToString("dd-MM-yyyy");
                string baseFilename = $"{currentDate}_chart";
                string outputPath = GetUniqueFilename(baseFilename, "xlsx");

                // Save the filled chart to a new Excel file with unique filename
                workbook.SaveAs(outputPath);

                MessageBox.Show($"Chart exported successfully to: {outputPath}", "Export to Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting chart to Excel: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Clean up Excel objects
                if (chartSheet != null) ReleaseObject(chartSheet);
                if (workbook != null)
                {
                    workbook.Close(false); // Close the workbook without saving changes
                    ReleaseObject(workbook);
                }
                if (excelApp != null)
                {
                    excelApp.Quit();
                    ReleaseObject(excelApp);
                }
            }
        }



        // Helper method to release COM objects
        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private string GetUniqueFilename(string baseFilename, string extension)
        {
            string fullPath = $@"D:\3rd_year_2nd_sem\IT_120 Event Driven Programming\resource_page\{baseFilename}.{extension}";
            int count = 0;

            while (File.Exists(fullPath))
            {
                baseFilename = $"{baseFilename}{count += 1}";
                fullPath = $@"D:\3rd_year_2nd_sem\IT_120 Event Driven Programming\resource_page\{baseFilename}.{extension}";
            }

            return fullPath;
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

        private void button1_Click(object sender, EventArgs e)
        {
            SetUserStatusInactive(username);
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Dashboard_student studform = new Dashboard_student(username);
            studform.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Dashboard_loan loanform = new Dashboard_loan(username);
            loanform.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Main_form mform = new Main_form(username);
            mform.Show();
            this.Hide();
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

        private void InventoryCount_Click(object sender, EventArgs e)
        {
            LoadDataInventory();
            LoadDataInventoryChart();
        }

        private DataTable LoadDataInventory()
        {

            DataTable dataTable = new DataTable();

            string query = @"
            SELECT b.book_id, b.book_title,a.author_name AS book_author, c.category_name AS book_category,
                   b.book_published,b.book_quantity,b.date_added
             FROM books b
             INNER JOIN author a ON b.book_author = a.author_id
             INNER JOIN category c ON b.book_category = c.category_id";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    da.Fill(dataTable);

                    // Assign custom column headers
                    dt.Columns["book_title"].ColumnName = "Book Title";
                    dt.Columns["book_author"].ColumnName = "Author";
                    dt.Columns["book_category"].ColumnName = "Genre";
                    dt.Columns["book_quantity"].ColumnName = "Quantity";
                    dt.Columns["date_added"].ColumnName = "Date Added";

                    dataGridView1.DataSource = dt;

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            return dataTable;
        }

        private void LoadDataInventoryChart()
        {
            string query = @"
        SELECT c.category_name AS Genre, SUM(b.book_quantity) AS TotalQuantity
        FROM books b
        INNER JOIN category c ON b.book_category = c.category_id
        GROUP BY c.category_name";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Assign custom column headers (optional for chart data)
                    dt.Columns["Genre"].ColumnName = "Genre";
                    dt.Columns["TotalQuantity"].ColumnName = "Total Quantity";

                    // Populate the chart with the aggregated data
                    chart1.DataSource = dt;
                    chart1.Series.Clear();
                    chart1.Series.Add("GenreQuantity");
                    chart1.Series["GenreQuantity"].XValueMember = "Genre";
                    chart1.Series["GenreQuantity"].YValueMembers = "Total Quantity";

                    // Set chart title and axis labels
                    chart1.Titles.Add("Inventory Count of Books");
                    chart1.ChartAreas[0].AxisX.Title = "Genre";
                    chart1.ChartAreas[0].AxisY.Title = "Total Books";
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void loanOverdue_Click(object sender, EventArgs e)
        {
            LoanOverdueloan();
            LoanOverdueChartLoan();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Book_author authorform = new Book_author(username);
            authorform.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Book_genre genreform = new Book_genre(username);
            genreform.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Dashboard_book bookform = new Dashboard_book(username);
            bookform.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private DataTable LoanOverdueloan()
        {

            DataTable dataTable = new DataTable();

            string query = @"
        SELECT l.loan_id, b.book_title AS BookTitle, s.student_name AS StudentName,
               l.borrowed_date AS DateBorrowed, l.due_date AS DateDue, l.return_date AS DateReturned,
               l.loan_status AS Status, l.loan_fees AS LoanFees, l.payment_status AS PaymentStatus
        FROM loan l
        INNER JOIN books b ON l.book_id = b.book_id
        INNER JOIN student s ON l.student_id = s.student_id
        WHERE l.payment_status = 'Not Paid'";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "loan");

                    dataTable = ds.Tables["loan"];

                    DataTable loansTable = ds.Tables["loan"];
                    loansTable.DefaultView.RowFilter = "PaymentStatus = 'Not Paid'";
                    // Bind the filtered DataTable to the DataGridView
                    dataGridView1.DataSource = ds.Tables["loan"];
                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error1: " + ex.Message);
            }
            return dataTable; // Return the populated DataTable
        }

        private void LoanOverdueChartLoan()
        {
            string query = @"
        SELECT l.loan_id, l.return_date, l.loan_fees
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
                    DataSet ds = new DataSet();
                    da.Fill(ds, "loan");

                    DataTable loansTable = ds.Tables["loan"];

                    // Dictionary to store total fees per month (key: month in yyyy-MM format, value: total fees)
                    Dictionary<string, decimal> monthlyFees = new Dictionary<string, decimal>();

                    // Calculate total fees per month
                    foreach (DataRow row in loansTable.Rows)
                    {
                        // Check if return_date is null before processing
                        if (row["return_date"] != DBNull.Value)
                        {
                            DateTime returnDate = Convert.ToDateTime(row["return_date"]);
                            decimal loanFees = Convert.ToDecimal(row["loan_fees"]);
                            string monthKey = returnDate.ToString("yyyy-MM");

                            if (monthlyFees.ContainsKey(monthKey))
                            {
                                monthlyFees[monthKey] += loanFees;
                            }
                            else
                            {
                                monthlyFees.Add(monthKey, loanFees);
                            }
                        }
                    }

                    // Clear existing series data in the chart
                    chart1.Series.Clear();


                    // Add a new series (monthly fees)
                    Series series = new Series();
                    series.ChartType = SeriesChartType.Column;
                    chart1.Series.Add(series);
                    series.LegendText = "Fees";

                    // Bind data to chart series
                    foreach (var kvp in monthlyFees)
                    {
                        string month = kvp.Key;
                        decimal totalFees = kvp.Value;
                        series.Points.AddXY(month, totalFees);
                    }

                    // Set chart title and axis labels
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Monthly Loan Fees");
                    chart1.ChartAreas[0].AxisX.Title = "Month";
                    chart1.ChartAreas[0].AxisY.Title = "Total Fees";

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = !dataGridView1.Visible;

            // Update button text based on DataGridView visibility
            if (dataGridView1.Visible)
            {
                // Hide the Chart and show the DataGridView
                chart1.Visible = false;
                dataGridView1.Visible = true;
            }
            else
            {
                // Hide the DataGridView and show the Chart
                dataGridView1.Visible = false;
                chart1.Visible = true;

            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            LoanBorrowbook();
            LoanBorrowChartbook();
        }

        private DataTable LoanBorrowbook()
        {
            DataTable dataTable = new DataTable(); // Create a new DataTable to hold the loan data

            string query = @"
        SELECT l.loan_id, b.book_title AS BookTitle, s.student_name AS StudentName,
               l.borrowed_date AS DateBorrowed, l.due_date AS DateDue, l.return_date AS DateReturned,
               l.loan_status AS Status, l.loan_fees AS LoanFees, l.payment_status AS PaymentStatus
        FROM loan l
        INNER JOIN books b ON l.book_id = b.book_id
        INNER JOIN student s ON l.student_id = s.student_id
        WHERE l.loan_status = 'Pending'"; // Filter by loan status

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "loan");

                    dataTable = ds.Tables["loan"];
                    DataTable loansTable = ds.Tables["loan"];
                    loansTable.DefaultView.RowFilter = "Status = 'Pending'";
                    dataGridView1.DataSource = ds.Tables["loan"];

                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            return dataTable; // Return the populated DataTable
        }

        private void LoanBorrowChartbook()
        {
            string query = @"
        SELECT l.loan_id, l.borrowed_date as Borrowdate
        FROM loan l";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "loan");

                    DataTable loansTable = ds.Tables["loan"];

                    // Dictionary to store total fees per month (key: month in yyyy-MM format, value: total fees)
                    Dictionary<string, int> monthlyBookCounts = new Dictionary<string, int>();

                    // Calculate total counts of books borrowed per month
                    foreach (DataRow row in loansTable.Rows)
                    {
                        DateTime borrowDate = Convert.ToDateTime(row["Borrowdate"]);
                        string monthKey = borrowDate.ToString("yyyy-MM");

                        if (monthlyBookCounts.ContainsKey(monthKey))
                        {
                            monthlyBookCounts[monthKey] += 1;
                        }
                        else
                        {
                            monthlyBookCounts.Add(monthKey, 1);
                        }
                    }

                    // Clear existing series data in the chart
                    chart1.Series.Clear();

                    // Add a new series (monthly book counts)
                    Series series = new Series();
                    series.ChartType = SeriesChartType.Column;
                    chart1.Series.Add(series);
                    series.LegendText = "Borrowed Books";

                    // Bind data to chart series
                    foreach (var kvp in monthlyBookCounts)
                    {
                        string month = kvp.Key;
                        int bookCount = kvp.Value;
                        series.Points.AddXY(month, bookCount);


                    }

                    // Set chart title and axis labels
                    chart1.Titles.Clear();
                    chart1.Titles.Add("Monthly Book Borrow Counts");
                    chart1.ChartAreas[0].AxisX.Title = "Month";
                    chart1.ChartAreas[0].AxisY.Title = "Total Books Borrowed";


                    Connection.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private Image CreateLoanOverdueChart()
        {
            string query = @"
        SELECT l.return_date, l.loan_fees
        FROM loan l";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Dictionary to store total fees per month (key: month in yyyy-MM format, value: total fees)
                    Dictionary<string, decimal> monthlyFees = new Dictionary<string, decimal>();

                    // Calculate total fees per month
                    foreach (DataRow row in dt.Rows)
                    {
                        // Check if return_date is not DBNull and loan_fees is a valid decimal
                        if (row["return_date"] != DBNull.Value && decimal.TryParse(row["loan_fees"].ToString(), out decimal loanFees))
                        {
                            DateTime returnDate = Convert.ToDateTime(row["return_date"]);
                            string monthKey = returnDate.ToString("yyyy-MM");

                            if (monthlyFees.ContainsKey(monthKey))
                            {
                                monthlyFees[monthKey] += loanFees;
                            }
                            else
                            {
                                monthlyFees.Add(monthKey, loanFees);
                            }
                        }
                    }

                    // Create a new chart control
                    Chart chart = new Chart();
                    chart.Size = new Size(600, 400);
                    chart.ChartAreas.Add(new ChartArea());
                    chart.Series.Add("LoanFees");
                    chart.Series["LoanFees"].ChartType = SeriesChartType.Column;

                    // Bind data to the chart series
                    foreach (var kvp in monthlyFees)
                    {
                        string month = kvp.Key;
                        decimal totalFees = kvp.Value;
                        chart.Series["LoanFees"].Points.AddXY(month, totalFees);
                    }

                    // Render the chart to an image
                    MemoryStream ms = new MemoryStream();
                    chart.SaveImage(ms, ChartImageFormat.Png);
                    Image chartImage = Image.FromStream(ms);
                    ms.Close();

                    return chartImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating loan overdue chart: " + ex.Message);
            }

            return null;
        }


        private Image CreateLoanBorrowChart()
        {
            string query = @"
        SELECT l.loan_id, l.borrowed_date as Borrowdate
        FROM loan l";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "loan");

                    DataTable loansTable = ds.Tables["loan"];

                    // Dictionary to store total counts of books borrowed per month
                    Dictionary<string, int> monthlyBookCounts = new Dictionary<string, int>();

                    // Calculate total counts of books borrowed per month
                    foreach (DataRow row in loansTable.Rows)
                    {
                        DateTime borrowDate = Convert.ToDateTime(row["Borrowdate"]);
                        string monthKey = borrowDate.ToString("yyyy-MM");

                        if (monthlyBookCounts.ContainsKey(monthKey))
                        {
                            monthlyBookCounts[monthKey] += 1;
                        }
                        else
                        {
                            monthlyBookCounts.Add(monthKey, 1);
                        }
                    }

                    // Create a new chart control
                    Chart chart = new Chart();
                    chart.Size = new Size(600, 400);
                    chart.ChartAreas.Add(new ChartArea());
                    chart.Series.Add("BorrowedBooks");
                    chart.Series["BorrowedBooks"].ChartType = SeriesChartType.Column;

                    // Bind data to the chart series
                    foreach (var kvp in monthlyBookCounts)
                    {
                        string month = kvp.Key;
                        int bookCount = kvp.Value;
                        chart.Series["BorrowedBooks"].Points.AddXY(month, bookCount);
                    }

                    // Render the chart to an image
                    MemoryStream ms = new MemoryStream();
                    chart.SaveImage(ms, ChartImageFormat.Png);
                    Image chartImage = Image.FromStream(ms);
                    ms.Close();

                    return chartImage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating loan borrow chart: " + ex.Message);
            }

            return null;
        }


        private Image CreateDataInventoryChart()
        {
            string query = @"
        SELECT c.category_name AS Genre, SUM(b.book_quantity) AS TotalQuantity
        FROM books b
        INNER JOIN category c ON b.book_category = c.category_id
        GROUP BY c.category_name";

            try
            {
                Connection.CloseConnection();
                if (Connection.OpenConnection())
                {
                    MySqlCommand cmd = new MySqlCommand(query, Connection.dbConnect);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Create a new chart control dynamically
                    Chart chart = new Chart();
                    chart.Size = new Size(600, 400);
                    chart.ChartAreas.Add(new ChartArea());
                    chart.Series.Add("InventoryCount");
                    chart.Series["InventoryCount"].ChartType = SeriesChartType.Column;

                    // Bind data to the chart series
                    foreach (DataRow row in dt.Rows)
                    {
                        string genre = Convert.ToString(row["Genre"]);
                        int totalQuantity = Convert.ToInt32(row["TotalQuantity"]);
                        chart.Series["InventoryCount"].Points.AddXY(genre, totalQuantity);
                    }

                    // Render the chart to an image
                    MemoryStream ms = new MemoryStream();
                    chart.SaveImage(ms, ChartImageFormat.Png);
                    Image chartImage = Image.FromStream(ms);
                    ms.Close();

                    return chartImage;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error creating data inventory chart: " + ex.Message);
            }

            return null;
        }



    }
}
