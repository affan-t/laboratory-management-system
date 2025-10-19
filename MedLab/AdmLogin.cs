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
    public partial class AdmLogin : Form
    {
        public AdmLogin()
        {
            InitializeComponent();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Login obj = new Login();
            obj.Show();
            this.Hide();
        }

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(PassTb.Text))
            {
                MessageBox.Show("Enter the password.");
                return;
            }

            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    Con.Open();

                    string query = "SELECT COUNT(*) FROM Admin WHERE AdminPass = @AdminPass";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@AdminPass", PassTb.Text);

                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            Laboratorians obj = new Laboratorians();
                            obj.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Wrong Password");
                            PassTb.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }


        private void ResetPassLbl_Click(object sender, EventArgs e)
        {
            ResetPass obj = new ResetPass();
            obj.Show();
            this.Hide();
        }
    }
}
