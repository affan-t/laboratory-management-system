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
    public partial class ResetPass : Form
    {
        public ResetPass()
        {
            InitializeComponent();
        }


        private void ResetPassBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewPassTb.Text) || string.IsNullOrWhiteSpace(SecKeyTb.Text))
            {
                MessageBox.Show("Fill all the fields!");
                return;
            }

            try
            {
                using (SqlConnection Con = new SqlConnection(@"Data Source=DESKTOP-TKTBHN2\SQLEXPRESS;Initial Catalog=MediLabDb;Integrated Security=True;Encrypt=True;TrustServerCertificate=True"))
                {
                    Con.Open();

                    string query = "UPDATE Admin SET AdminPass = @NewPass WHERE SecurityKey = @SecKey";
                    using (SqlCommand cmd = new SqlCommand(query, Con))
                    {
                        cmd.Parameters.AddWithValue("@NewPass", NewPassTb.Text);
                        cmd.Parameters.AddWithValue("@SecKey", SecKeyTb.Text);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Password Updated Successfully");
                            NewPassTb.Clear();
                            SecKeyTb.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Invalid Security Key. Password not updated.");
                            NewPassTb.Clear();
                            SecKeyTb.Clear();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AdmLogin obj = new AdmLogin();
            obj.Show();
            this.Hide();
        }
    }
}
