using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MedLab
{
    public partial class StatisticalReport : Form
    {
        public StatisticalReport()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private void GenReport_Click(object sender, EventArgs e)
        {
            if (FromDate.Value > ToDate.Value)
            {
                MessageBox.Show("From date cannot be later than To date.");
                return;
            }

            try
            {
                DateTime fromDate = FromDate.Value;
                DateTime toDate = ToDate.Value;

                string query = @"SELECT R.Test AS TestId, T.TestName, 
                    COUNT(R.ResNum) AS NoOfTestsDone, 
                    SUM(R.TestCost) AS TotalProfit,
                    @FromDate AS FromDate, 
                    @ToDate AS ToDate
                    FROM ResultTbl R
                    JOIN TestTbl T ON R.Test = T.TestCode
                    WHERE R.TestDate BETWEEN @FromDate AND @ToDate
                    GROUP BY R.Test, T.TestName";

                Con.Open();
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@FromDate", fromDate);
                cmd.Parameters.AddWithValue("@ToDate", toDate);
                SqlDataReader reader = cmd.ExecuteReader();
                ReportsDGV.Rows.Clear();

                while (reader.Read())
                {
                    // Safely get the TestId as a nullable int
                    int testId = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);

                    // Safely get the TestName, could be nullable
                    string testName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);

                    // Safely get NoOfTestsDone as an integer, with a fallback to 0 if NULL
                    int noOfTestsDone = reader.IsDBNull(2) ? 0 : reader.GetInt32(2);

                    // Safely get TotalProfit as a decimal, fallback to 0 if NULL
                    decimal totalProfit = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3);

                    // For the FromDate and ToDate, ensure they are correctly returned as DateTime
                    string fromDateStr = reader.IsDBNull(4) ? "N/A" : reader.GetDateTime(4).ToString("dd-MM-yyyy");
                    string toDateStr = reader.IsDBNull(5) ? "N/A" : reader.GetDateTime(5).ToString("dd-MM-yyyy");

                    // Adding the row to the DataGridView
                    ReportsDGV.Rows.Add(testId, testName, noOfTestsDone, totalProfit, $"{fromDateStr} to {toDateStr}");
                }

                reader.Close();
                Con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Patients patients = new Patients();
            patients.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Results results = new Results();
            results.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            StatisticalReport report = new StatisticalReport();
            report.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }
    }
}
