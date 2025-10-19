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
    public partial class Results : Form
    {
        public Results()
        {
            InitializeComponent();
            ShowResult();
            GetPatient();
            GetLab();
            GetTest();
            DateLbl.Text = DateTime.Today.Day.ToString() + "/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private void ShowResult()
        {
            Con.Open();
            string Query = "Select * from ResultTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            ResultDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void GetPatient()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select PatNum from PatientTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("PatNum", typeof(string));
            dt.Load(Rdr);
            PatIdCb.ValueMember = "PatNum";
            PatIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetPatientName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from PatientTbl where PatNum = " + PatIdCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                PatNameTb.Text = dr["PatName"].ToString();
            }
            Con.Close();
        }
        private void GetLab()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select LabId from LaboratorianTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("LabId", typeof(int));
            dt.Load(Rdr);
            LabIdCb.ValueMember = "LabId";
            LabIdCb.DataSource = dt;
            Con.Close();
        }
        private void GetLabName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from LaboratorianTbl where LabId = " + LabIdCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                LabNameCb.Text = dr["LabName"].ToString();
            }
            Con.Close();
        }
        private void GetTest()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select TestCode from TestTbl", Con);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("TestCode", typeof(int));
            dt.Load(Rdr);
            TestIdCb.ValueMember = "TestCode";
            TestIdCb.DataSource = dt;
            Con.Close();
        }
        int Cost = 0;
        private void GetTestName()
        {
            Con.Open();
            SqlCommand cmd = new SqlCommand("Select * from TestTbl where TestCode = " + TestIdCb.SelectedValue.ToString() + "", Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                TestNameCb.Text = dr["TestName"].ToString();
                Cost = Convert.ToInt32(dr["TestCost"].ToString());
            }
            Con.Close();
        }

        private void PatIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetPatientName();
        }

        private void LabIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetLabName();
        }

        private void TestIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetTestName();
        }

        int TCost = 0;
        private void OkBtn_Click(object sender, EventArgs e)
        {
            if(TestIdCb.SelectedIndex == -1 || ResultCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select All the Fields!");
            }
            else
            {
                TestTb.Text = TestNameCb.Text + " : " + ResultCb.SelectedItem.ToString() + '\n';
                TCost = Cost;
                TestCostTb.Text = TCost.ToString();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LabIdCb.SelectedIndex == -1 || TestIdCb.SelectedIndex == -1 || PatIdCb.SelectedIndex == -1 || ResultCb.SelectedIndex == -1)
            {
                MessageBox.Show("Select All the Fields!");
            }
            else
            {
                try
                {
                    Con.Open();
                    string Query = "insert into ResultTbl(PatId, PatName, Laboratorian, LabName, TestDone, Test, TestCost, TestDate) values(@PI, @PN, @Lab, @LN, @TD, @Test, @TC, @TDate)";
                    SqlCommand cmd = new SqlCommand(Query, Con);
                    cmd.Parameters.AddWithValue("@PI", PatIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@Lab", LabIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@LN", LabNameCb.Text);
                    cmd.Parameters.AddWithValue("@TD", TestTb.Text);
                    cmd.Parameters.AddWithValue("@Test", TestIdCb.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@TC", TestCostTb.Text);
                    cmd.Parameters.AddWithValue("@TDate", DTime.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Result Saved Successfully!");
                    Con.Close();
                    ShowResult();
                    Reset();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ResultDGV_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            printDocument1.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("pprnm", 500, 600);

            if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float marginLeft = 50; // Margin from the left
            float marginTop = 50; // Margin from the top
            float lineHeight = 30; // Spacing between lines

            // Title
            e.Graphics.DrawString("MediLab", new Font("Century Gothic", 22, FontStyle.Bold), Brushes.Red, marginLeft + 150, marginTop);
            e.Graphics.DrawString("Patient Test Report", new Font("Century Gothic", 18, FontStyle.Regular), Brushes.Black, marginLeft + 150, marginTop + lineHeight);


            if (ResultDGV.SelectedRows.Count > 0)
            {
                var selectedRow = ResultDGV.SelectedRows[0];

                string Rnumber = selectedRow.Cells[0].Value?.ToString() ?? "N/A";
                string Pid = selectedRow.Cells[1].Value?.ToString() ?? "N/A";
                string Pname = selectedRow.Cells[2].Value?.ToString() ?? "N/A";
                string Lid = selectedRow.Cells[3].Value?.ToString() ?? "N/A";
                string Lname = selectedRow.Cells[4].Value?.ToString() ?? "N/A";
                string Tdone = selectedRow.Cells[5].Value?.ToString() ?? "N/A";
                string Test = selectedRow.Cells[6].Value?.ToString() ?? "N/A";
                string Tcost = selectedRow.Cells[7].Value?.ToString() ?? "N/A";
                string Tdate = selectedRow.Cells[8].Value?.ToString() ?? "N/A";

                float currentY = marginTop + (2 * lineHeight);

                // Report Details Section
                e.Graphics.DrawString("Report Details", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Report No: 3000", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Patient ID: 5000", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Patient Name: Ali", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += 2 * lineHeight;

                // Laboratorian Details Section
                e.Graphics.DrawString("Laboratorian Details", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Laboratorian ID: 1000", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Laboratorian Name: Affan", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += 2 * lineHeight;

                // Test Details Section
                e.Graphics.DrawString("Test Details", new Font("Century Gothic", 16, FontStyle.Bold), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Tests Done: UltraSound : Negative", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Test ID: 3000", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;
                e.Graphics.DrawString("Test Cost: Rs. 1200", new Font("Century Gothic", 14, FontStyle.Regular), Brushes.Black, marginLeft, currentY);
                currentY += lineHeight;

                // Footer
                e.Graphics.DrawString("Thank you for choosing MediLab!", new Font("Century Gothic", 12, FontStyle.Italic), Brushes.Black, marginLeft, currentY + lineHeight);
            }
            else
            {
                e.Graphics.DrawString("No selected rows to print.", new Font("Century Gothic", 20, FontStyle.Regular), Brushes.Black, new Point(10, 100));
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (ResultDGV.SelectedRows.Count > 0)
            {
                int resNum = Convert.ToInt32(ResultDGV.SelectedRows[0].Cells[0].Value);

                Con.Open();
                string query = "DELETE FROM ResultTbl WHERE ResNum = @ResNum";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@ResNum", resNum);
                cmd.ExecuteNonQuery();
                Con.Close();
                ShowResult();
                Reset();
                MessageBox.Show("Record deleted successfully.");
            }
            else
            {
                MessageBox.Show("No record selected.");
            }
        }


        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (ResultDGV.SelectedRows.Count > 0)
            {
                int resNum = Convert.ToInt32(ResultDGV.SelectedRows[0].Cells[0].Value);

                Con.Open();
                string query = "UPDATE ResultTbl SET PatId = @PatId, PatName = @PatName, Laboratorian = @Laboratorian, LabName = @LabName, TestDone = @TestDone, Test = @Test, TestCost = @TestCost, TestDate = @TestDate WHERE ResNum = @ResNum";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@PatId", PatIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@PatName", PatNameTb.Text);
                cmd.Parameters.AddWithValue("@Laboratorian", LabIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@LabName", LabNameCb.Text);
                cmd.Parameters.AddWithValue("@TestDone", TestTb.Text);
                cmd.Parameters.AddWithValue("@Test", TestIdCb.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@TestCost", TestCostTb.Text);
                cmd.Parameters.AddWithValue("@TestDate", DTime.Value.Date);
                cmd.Parameters.AddWithValue("@ResNum", resNum);
                cmd.ExecuteNonQuery();
                Con.Close();
                ShowResult();
                Reset();
                MessageBox.Show("Record updated successfully.");
            }
            else
            {
                MessageBox.Show("No record selected.");
            }
        }

        private void Reset()
        {
            PatIdCb.SelectedIndex = -1;
            PatNameTb.Clear();
            LabIdCb.SelectedIndex = -1;
            LabNameCb.Clear();
            TestIdCb.SelectedIndex = -1;
            TestNameCb.Clear();
            ResultCb.SelectedIndex = -1;
            TestTb.Clear();
            TestCostTb.Clear();
            DTime.Value = DateTime.Today;
        }

        private void ResultDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < ResultDGV.Rows.Count)
            {
                DataGridViewRow row = ResultDGV.Rows[e.RowIndex];
                PatIdCb.SelectedValue = row.Cells["PatId"].Value;
                PatNameTb.Text = row.Cells["PatName"].Value?.ToString() ?? string.Empty;
                LabIdCb.SelectedValue = row.Cells["Laboratorian"].Value;
                LabNameCb.Text = row.Cells["LabName"].Value?.ToString() ?? string.Empty;
                TestTb.Text = row.Cells["TestDone"].Value?.ToString() ?? string.Empty;
                TestIdCb.SelectedValue = row.Cells["Test"].Value;
                TestCostTb.Text = row.Cells["TestCost"].Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["TestDate"].Value?.ToString(), out DateTime testDate))
                {
                    DTime.Value = testDate;
                }
                else
                {
                    DTime.Value = DateTime.Today;
                }
            }
            else
            {
                MessageBox.Show("Invalid row selected!");
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            StatisticalReport Obj = new StatisticalReport();
            Obj.Show();
            this.Hide();
        }
    }
}
