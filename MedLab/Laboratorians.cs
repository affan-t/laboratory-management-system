using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Windows.Forms;
using System;
using System.Data;
using System.Threading;


namespace MedLab
{
    public partial class Laboratorians : Form
    {
        public Laboratorians()
        {
            InitializeComponent();
            ShowLaboratorian();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private void ShowLaboratorian()
        {
            Con.Open();
            string Query = "Select * from LaboratorianTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            LabDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LabNameTb.Text == "" || LabAddressTb.Text == "" || LabPasswordTb.Text == "" || LabUsernameTb.Text == "" || LabPhoneTb.Text == "" || LabGenderCb.SelectedIndex == -1)
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    {
                        Con.Open();
                        SqlCommand cmd = new SqlCommand("insert into LaboratorianTbl(LabName, LabGender, LabAddress, LabPhone, LabDOB, LabUsername, LabPassword) values(@LN,@LG,@LA,@LP,@LD, @LU, @LPass)", Con);
                        cmd.Parameters.AddWithValue("@LN", LabNameTb.Text);
                        cmd.Parameters.AddWithValue("@LG", LabGenderCb.SelectedItem.ToString());
                        cmd.Parameters.AddWithValue("@LA", LabAddressTb.Text);
                        cmd.Parameters.AddWithValue("@LP", LabPhoneTb.Text);
                        cmd.Parameters.AddWithValue("@LD", LabDOB.Value.Date);
                        cmd.Parameters.AddWithValue("@LU", LabUsernameTb.Text);
                        cmd.Parameters.AddWithValue("@LPass", LabPasswordTb.Text);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Laboratorian Added Successfully!");
                        Con.Close();
                        ShowLaboratorian();
                        Reset();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (LabNameTb.Text == "" || LabAddressTb.Text == "" || LabPasswordTb.Text == "" || LabUsernameTb.Text == "" || LabPhoneTb.Text == "" || LabGenderCb.SelectedIndex == -1 || LabUsernameTb.Text == "" || LabPasswordTb.Text == "")
            {
                MessageBox.Show("Select The Laboratorian to Update!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update LaboratorianTbl Set LabName=@LN, LabGender=@LG, LabAddress=@LA, LabPhone=@LP, LabDOB=@LD, LabUsername=@LU, LabPassword=@LPass where LabId=@LKey", Con);
                    cmd.Parameters.AddWithValue("@LN", LabNameTb.Text);
                    cmd.Parameters.AddWithValue("@LG", LabGenderCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@LA", LabAddressTb.Text);
                    cmd.Parameters.AddWithValue("@LP", LabPhoneTb.Text);
                    cmd.Parameters.AddWithValue("@LD", LabDOB.Value.Date);
                    cmd.Parameters.AddWithValue("@LU", LabUsernameTb.Text);
                    cmd.Parameters.AddWithValue("@LPass", LabPasswordTb.Text);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Updated Successfully!");
                    Con.Close();
                    ShowLaboratorian();
                    Reset(); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void Reset()
        {
            Key = 0;
            LabNameTb.Clear(); LabGenderCb.SelectedIndex = -1; LabAddressTb.Clear();
            LabUsernameTb.Clear(); LabPasswordTb.Clear(); LabPhoneTb.Clear(); LabDOB.Value = DateTime.Now;
        }
        int Key = 0;
        private void LabDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < LabDGV.Rows.Count)
            {
                DataGridViewRow row = LabDGV.Rows[e.RowIndex];
                LabNameTb.Text = row.Cells["LabName"].Value?.ToString() ?? string.Empty;
                LabGenderCb.SelectedItem = row.Cells["LabGender"].Value?.ToString() ?? string.Empty;
                LabAddressTb.Text = row.Cells["LabAddress"].Value?.ToString() ?? string.Empty;
                LabPhoneTb.Text = row.Cells["LabPhone"].Value?.ToString() ?? string.Empty;
                LabUsernameTb.Text = row.Cells["LabUsername"].Value?.ToString() ?? string.Empty;
                LabPasswordTb.Text = row.Cells["LabPassword"].Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["LabDOB"].Value?.ToString(), out DateTime labDOB))
                {
                    LabDOB.Value = labDOB;
                }
                else
                {
                    LabDOB.Value = DateTime.Today;
                }
                if (int.TryParse(row.Cells["LabId"].Value?.ToString(), out int labId))
                {
                    Key = labId;
                }
                else
                {
                    Key = 0;
                }
            }
            else
            {
                MessageBox.Show("Invalid row selected!");
            }
        }


        private void DeleteBtn_Click(object sender, EventArgs e)
        {
            if (Key == 0)
            {
                MessageBox.Show("Select a Laboratorian!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete LaboratorianTbl where LabId=@LKey", Con);
                    cmd.Parameters.AddWithValue("@LKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Laboratorian Deleted Successfully!");
                    Con.Close();
                    ShowLaboratorian();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Laboratorians Obj = new Laboratorians();
            Obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests Obj= new Tests();
            Obj.Show();
            this.Hide();
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

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Inventory Obj = new Inventory();
            Obj.Show();
            this.Hide();
        }
    }
}
