using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diplomski_rad___s21_20.Views
{
    public partial class Korpa : Form
    {
        private readonly string connectionString = "Data Source=localhost;Initial Catalog=OpticarskaRadnjaDb;Integrated Security=True;";

        public Korpa()
        {
            InitializeComponent();
            PrikaziProizvodeUKorpi(Program.CurrentUser);
            btnIsprazniKorpu.Click += btnIsprazniKorpu_Click;
            btnZavrsiKupovinu.Click += btnZavrsiKupovinu_Click;
        }

        public void PrikaziProizvodeUKorpi(string korisnikUsername)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT n.Naocare_Ime, n.Naocare_Cena,n.Naocare_Slika ,k.Kolicina " +
                                   $"FROM Korpa k " +
                                   $"INNER JOIN Naocare n ON k.Naocare_Id = n.Naocare_Id " +
                                   $"INNER JOIN Korisnici kor ON k.Korisnik_Id = kor.id " +
                                   $"WHERE kor.username = @korisnikUsername";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, connection))
                    {
                        adapter.SelectCommand.Parameters.AddWithValue("@korisnikUsername", korisnikUsername);

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri dohvatanju proizvoda iz korpe: {ex.Message}");
            }
        }
        private void btnIsprazniKorpu_Click(object sender, EventArgs e)
        {
            IsprazniKorpu(Program.CurrentUser);
        }

        private void IsprazniKorpu(string korisnikUsername)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"DELETE FROM Korpa WHERE Korisnik_Id = (SELECT id FROM Korisnici WHERE username = @korisnikUsername)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@korisnikUsername", korisnikUsername);
                        command.ExecuteNonQuery();
                    }

                    // Osveži DataGridView
                    PrikaziProizvodeUKorpi(korisnikUsername);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri pražnjenju korpe: {ex.Message}");
            }
        }
        private void btnZavrsiKupovinu_Click(object sender, EventArgs e)
        {
            GenerisiRacun(Program.CurrentUser);
        }
        private void GenerisiRacun(string korisnikUsername)
        {
            try
            {
                string folderPath = @"C:\Računi";
                string filePath = Path.Combine(folderPath, $"Račun_{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.txt");
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                string absolutePath = Path.GetFullPath(filePath);
                using (StreamWriter sw = new StreamWriter(absolutePath))
                {
                    sw.WriteLine($"Racun za korisnika: {korisnikUsername}");
                    sw.WriteLine("Stavke racuna:");

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        // Upit za dohvatanje stavki racuna
                        string query = $"SELECT n.Naocare_Ime, n.Naocare_Cena, k.Kolicina " +
                                       $"FROM Korpa k " +
                                       $"INNER JOIN Naocare n ON k.Naocare_Id = n.Naocare_Id " +
                                       $"INNER JOIN Korisnici kor ON k.Korisnik_Id = kor.id " +
                                       $"WHERE kor.username = @korisnikUsername";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@korisnikUsername", korisnikUsername);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string naziv = reader.GetString(0);
                                    int kolicina = reader.GetInt32(2);

                                    // Dohvati vrednost cene iz baze
                                    decimal cena = 0;
                                    if (!reader.IsDBNull(1) && reader.GetFieldType(1) == typeof(decimal))
                                    {
                                        cena = reader.GetDecimal(1);
                                    }
                                    else
                                    {
                                        // Ako vrednost u bazi nije decimalna, postavi je na 0
                                        sw.WriteLine($"{naziv} - {kolicina} komada - Cena nije validna");
                                        continue;
                                    }

                                    sw.WriteLine($"{naziv} - {kolicina} komada - {cena * kolicina} din");
                                }
                            }
                        }
                    }


                    // Dohvatamo ukupnu cenu racuna
                    decimal ukupno = 0;
                    string queryUkupno = $"SELECT SUM(n.Naocare_Cena * k.Kolicina) " +
                                         $"FROM Korpa k " +
                                         $"INNER JOIN Naocare n ON k.Naocare_Id = n.Naocare_Id " +
                                         $"INNER JOIN Korisnici kor ON k.Korisnik_Id = kor.id " +
                                         $"WHERE kor.username = @korisnikUsername";

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(queryUkupno, connection))
                        {
                            command.Parameters.AddWithValue("@korisnikUsername", korisnikUsername);
                            ukupno = (decimal)command.ExecuteScalar();
                        }
                    }

                    // Upisujemo ukupnu cenu u fajl
                    sw.WriteLine($"Ukupno: {ukupno} din");
                }

                // Obavestavamo korisnika da je račun generisan
                MessageBox.Show($"Račun je generisan. Detalji su sačuvani u fajlu: {absolutePath}");

                // Automatski otvori generisani račun
                Process.Start("notepad.exe", absolutePath);

                // Ispražnjavamo korpu nakon završetka kupovine
                IsprazniKorpu(korisnikUsername);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri generisanju računa: {ex.Message}");
            }
        }


    }
}

