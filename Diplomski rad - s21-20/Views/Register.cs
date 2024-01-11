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

namespace Diplomski_rad___s21_20.Views
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            btnRegister.Click += btnRegister_Click;
            txtPassword.PasswordChar = '*';
            txtConfirmPassword.PasswordChar = '*';
            this.AcceptButton = btnRegister;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Morate uneti korisničko ime i lozinku.");
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Lozinke se ne podudaraju.");
                return;
            }
            string connectionString = "Data Source=localhost;Initial Catalog=OpticarskaRadnjaDb;Integrated Security=True;";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "INSERT INTO Korisnici (username, password) VALUES (@username, @password)";
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Uspešno ste se registrovali!");
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Došlo je do greške pri registraciji.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri registraciji: " + ex.Message);
            }
        }
    }
}
