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
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
            ShowMachines();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");
        private void ShowMachines()
        {
            Con.Open();
            string Query = "Select * from MachineTbl";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            InventoryDGV.DataSource = ds.Tables[0];
            Con.Close();
        }
        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if(MachineNameTb.Text == "" || MainCostTb.Text == "")
            {
                MessageBox.Show("Missing Information!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into MachineTbl(MachineName, MainCost, MainDate) values(@MN, @MP, @MD)", Con);
                    cmd.Parameters.AddWithValue("@MN", MachineNameTb.Text);
                    cmd.Parameters.AddWithValue("@MP", MainCostTb.Text);
                    cmd.Parameters.AddWithValue("@MD", MainDate.Value.Date);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Machine Added Successfully!");
                    Con.Close();
                    ShowMachines();
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
            MachineNameTb.Clear();
            MainCostTb.Clear();
            MainDate.Value = DateTime.Now;
        }
        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (MachineNameTb.Text == "" || MainCostTb.Text == "" || MainDate.Value == null)
            {
                MessageBox.Show("Select the Machine to Update!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE MachineTbl SET MachineName=@MN, MainCost=@MC, MainDate=@MD WHERE MachineId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MN", MachineNameTb.Text);
                    cmd.Parameters.AddWithValue("@MC", Convert.ToInt32(MainCostTb.Text));
                    cmd.Parameters.AddWithValue("@MD", MainDate.Value.Date);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Machine Updated Successfully!");
                    Con.Close();
                    ShowMachines();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }


        int Key = 0;


        private void InventoryDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < InventoryDGV.Rows.Count)
            {
                DataGridViewRow row = InventoryDGV.Rows[e.RowIndex];
                MachineNameTb.Text = row.Cells["MachineName"].Value?.ToString() ?? string.Empty;
                MainCostTb.Text = row.Cells["MainCost"].Value?.ToString() ?? string.Empty;
                if (DateTime.TryParse(row.Cells["MainDate"].Value?.ToString(), out DateTime mainDate))
                {
                    MainDate.Value = mainDate;
                }
                else
                {
                    MainDate.Value = DateTime.Today;
                }
                if (int.TryParse(row.Cells["MachineId"].Value?.ToString(), out int machineId))
                {
                    Key = machineId;
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
                MessageBox.Show("Select a Machine!");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM MachineTbl WHERE MachineId=@MKey", Con);
                    cmd.Parameters.AddWithValue("@MKey", Key);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Machine Deleted Successfully!");
                    Con.Close();
                    ShowMachines();
                    Reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Laboratorians obj = new Laboratorians();
            obj.Show();
            this.Hide();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Tests tests = new Tests();
            tests.Show();
            this.Hide();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Inventory obj = new Inventory();
            obj.Show();
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
