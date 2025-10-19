using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;
using System;
using System.Data;
using System.Threading;

namespace MedLab
{
    public partial class Patients : Form
    {
        public Patients()
        {
            InitializeComponent();
            ShowPatient();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private void ShowPatient()
        {
            Con.Open();
            string Query = "Select * from PatientTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            PatDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void Reset()
        {
            Key = 0;
            PatNameTb.Clear(); PatPhoneTb.Clear(); PatAddressTb.Clear(); PatGenderGb.SelectedIndex = -1;
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || PatGenderGb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into PatientTbl(PatName, PatGen, PatAdd, PatPhone,  PatDOB) values(@PN,@PG,@PA,@PP,@PD)", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenderGb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Added Successfully!");
                    Con.Close();
                    ShowPatient();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        int Key = 0;
        private void PatDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            if (e.RowIndex >= 0 && e.RowIndex < PatDGV.Rows.Count)
            {
                DataGridViewRow row = PatDGV.Rows[e.RowIndex];
                PatNameTb.Text = row.Cells["PatName"].Value?.ToString();
                PatGenderGb.SelectedItem = row.Cells["PatGen"].Value?.ToString();
                PatAddressTb.Text = row.Cells["PatAdd"].Value?.ToString();
                PatPhoneTb.Text = row.Cells["PatPhone"].Value?.ToString();
                if (DateTime.TryParse(row.Cells["PatDOB"].Value?.ToString(), out DateTime labDOB))
                {
                    PatDOB.Value = labDOB;
                }
                if (int.TryParse(row.Cells["PatNum"].Value?.ToString(), out int patId))
                {
                    Key = patId;
                }
                else
                {
                    Key = 0;
                }
            }

        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (PatNameTb.Text == "" || PatPhoneTb.Text == "" || PatAddressTb.Text == "" || PatGenderGb.SelectedIndex == -1)
            {
                MessageBox.Show("Select The Patient to Update!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update PatientTbl Set PatName=@PN, PatGen=@PG, PatAdd=@PA, PatPhone=@PP,  PatDOB=@PD where PatNum=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PN", PatNameTb.Text);
                    cmd.Parameters.AddWithValue("@PG", PatGenderGb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@PA", PatAddressTb.Text);
                    cmd.Parameters.AddWithValue("@PP", PatPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@PD", PatDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Updated Successfully!");
                    Con.Close();
                    ShowPatient();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Patient!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete PatientTbl where PatNum=@PKey", Con);
                    cmd.Parameters.AddWithValue("@PKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Patient Deleted Successfully!");
                    Con.Close();
                    ShowPatient();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Results Obj = new Results();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Patients Obj = new Patients();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Login Obj = new Login();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {
            Results obj = new Results();
            obj.Show();
            this.Hide();
        }
    }
}
