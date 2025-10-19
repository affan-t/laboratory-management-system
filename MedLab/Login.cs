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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

        private void AdminLoginLbl_Click(object sender, EventArgs e)
        {
            AdmLogin Adm = new AdmLogin();
            Adm.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (UnameTb.Text == "" || PassTb.Text == "")
            {
                MessageBox.Show("Enter Username and Password");
                return;
            }

            try
            {
                Con.Open();
                string query = "SELECT COUNT(*) FROM LaboratorianTbl WHERE LabUsername=@username AND LabPassword=@password";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.Parameters.AddWithValue("@username", UnameTb.Text);
                cmd.Parameters.AddWithValue("@password", PassTb.Text);

                int count = (int)cmd.ExecuteScalar();
                if (count == 1)
                {
                    Patients main = new Patients();
                    main.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Wrong Username or Password");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            finally
            {
                Con.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to exit?", "Exit Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

    }
}
