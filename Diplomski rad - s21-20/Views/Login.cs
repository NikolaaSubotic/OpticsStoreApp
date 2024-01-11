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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Click += btnLogin_Click;
            txtPassword.PasswordChar = '*';
            this.AcceptButton = btnLogin;
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;


            if (ProveriKorisnikaUBazi(username, password))
            {
                Program.CurrentUser = username;
                MessageBox.Show("Uspešno ste se prijavili!");
                this.Hide();
            }
            else
            {
                txtUsername.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtUsername.Focus();

                MessageBox.Show("Pogrešno korisničko ime ili lozinka. Pokušajte ponovo.");
            }
        }

        private bool ProveriKorisnikaUBazi(string username, string password)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=OpticarskaRadnjaDb;Integrated Security=True;";

            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM Korisnici WHERE username = @username AND password = @password";
                    command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                    command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.HasRows; // Ako ima redova, korisnik postoji u bazi.
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška pri proveri korisnika: " + ex.Message);
                return false; // Vraćamo false ako je došlo do greške
            }
        }


    }
}
