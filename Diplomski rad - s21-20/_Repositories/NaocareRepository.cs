using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using Diplomski_rad___s21_20.Models;

namespace Diplomski_rad___s21_20._Repositories
{
    public class NaocareRepository : BaseRepository, INaocareRepository
    {

        public NaocareRepository(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public void Add(NaocareModel naocareModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "insert into Naocare (Naocare_Ime, Naocare_Boja, Naocare_Cena, Naocare_Slika, Tip_Id) values (@ime, @boja, @cena, @slika, @tipId)";
                command.Parameters.Add("@ime", SqlDbType.NVarChar).Value = naocareModel.Ime;
                command.Parameters.Add("@boja", SqlDbType.NVarChar).Value = naocareModel.Boja;
                command.Parameters.Add("@cena", SqlDbType.Decimal).Value = naocareModel.Cena;
                command.Parameters.Add("@slika", SqlDbType.VarBinary).Value = naocareModel.Slika;
                // Ovde pristupite nazivu tipa iz vašeg modela
                string izabraniNazivTipa = naocareModel.TipNaočara; // Pretpostavljamo da je ovo naziv tipa.

                // Dobijte odgovarajući Tip_Id koristeći funkciju GetTipIdFromNaziv
                int tipId = GetTipIdFromNaziv(izabraniNazivTipa);

                command.Parameters.Add("@tipId", SqlDbType.Int).Value = tipId;
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "delete from Naocare where Naocare_Id=@id";
                command.Parameters.Add("@id", SqlDbType.Int).Value = id;
                command.ExecuteNonQuery();
            }
        }

        public void Edit(NaocareModel naocareModel)
        {
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = @"update Naocare set Naocare_Ime=@ime, Naocare_Boja=@boja, Naocare_Cena=@cena, Naocare_Slika=@slika, Tip_Id=@tipId where Naocare_Id=@id";
                command.Parameters.Add("@ime", SqlDbType.NVarChar).Value = naocareModel.Ime;
                command.Parameters.Add("@boja", SqlDbType.NVarChar).Value = naocareModel.Boja;
                command.Parameters.Add("@cena", SqlDbType.Decimal).Value = naocareModel.Cena;
                command.Parameters.Add("@slika", SqlDbType.VarBinary).Value = naocareModel.Slika;
                string izabraniNazivTipa = naocareModel.TipNaočara;
                int tipId = GetTipIdFromNaziv(izabraniNazivTipa);

                command.Parameters.Add("@tipId", SqlDbType.Int).Value = tipId;
                command.Parameters.Add("@id", SqlDbType.Int).Value = naocareModel.Id;
                command.ExecuteNonQuery();
            }
        }
        private int GetTipIdFromNaziv(string nazivTipa)
        {
            int tipId = -1; // Postavite neku podrazumevanu vrednost ili -1 ako tip nije pronađen.

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT Tip_Id FROM Tip WHERE Naziv = @nazivTipa";
                command.Parameters.Add("@nazivTipa", SqlDbType.NVarChar).Value = nazivTipa;

                var result = command.ExecuteScalar(); // Očekujemo da se vrati Tip_Id ili NULL ako tip nije pronađen.

                if (result != null && result != DBNull.Value)
                {
                    tipId = (int)result; // Pretvorimo rezultat u integer.
                }
            }

            return tipId;
        }

        public IEnumerable<NaocareModel> GetAll()
        {
            var naocareList = new List<NaocareModel>();
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // Izmijenite upit da koristi JOIN sa tabelom Tip i dodajte WHERE klauzulu za filtriranje po Tip_Id
                command.CommandText = "SELECT n.*, t.Naziv AS TipNaočaraNaziv FROM Naocare n LEFT JOIN Tip t ON n.Tip_Id = t.Tip_Id WHERE n.Tip_Id = 1 ORDER BY n.Naocare_Id DESC";
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var naocareModel = new NaocareModel();
                        naocareModel.Id = (int)reader[0];
                        naocareModel.Ime = reader[1].ToString();
                        naocareModel.Boja = reader[2].ToString();
                        naocareModel.Cena = (decimal)reader[3];
                        if (reader[4] != DBNull.Value)
                        {
                            naocareModel.Slika = (byte[])reader[4]; // Učitavanje slike iz baze
                        }
                        else
                        {
                            naocareModel.Slika = null; // Postavite Slika svojstvo na null ako je NULL vrednost u bazi
                        }
                        // Postavite vrednost TipNaočara iz rezultata JOIN-a
                        naocareModel.TipNaočara = reader["TipNaočaraNaziv"].ToString();
                        naocareList.Add(naocareModel);
                    }
                }
            }
            return naocareList;
        }

        public IEnumerable<NaocareModel> GetByValue(string value)
        {
            var naocareList = new List<NaocareModel>();
            int NaocareId = int.TryParse(value, out _) ? Convert.ToInt32(value) : 0;
            string NaocareIme = value;
            string NaocareBoja = value; // Dodajte Boja parametar
            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                // Izmijenite upit da koristi JOIN sa tabelom Tip i dodajte WHERE klauzulu za filtriranje po TipId i Boji
                command.CommandText = @"SELECT n.*, t.Naziv AS TipNaočaraNaziv 
                          FROM Naocare n 
                          LEFT JOIN Tip t ON n.Tip_Id = t.Tip_Id 
                          WHERE (n.Tip_Id = 1) 
                          AND (n.Naocare_Id = @id OR n.Naocare_Ime LIKE @ime + '%' OR n.Naocare_Boja LIKE @boja + '%') 
                          ORDER BY n.Naocare_Id DESC";
                command.Parameters.Add("@id", SqlDbType.Int).Value = NaocareId;
                command.Parameters.Add("@ime", SqlDbType.NVarChar).Value = NaocareIme;
                command.Parameters.Add("@boja", SqlDbType.NVarChar).Value = NaocareBoja; // Dodajte Boja parametar
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var naocareModel = new NaocareModel();
                        naocareModel.Id = (int)reader[0];
                        naocareModel.Ime = reader[1].ToString();
                        naocareModel.Boja = reader[2].ToString();
                        naocareModel.Cena = (decimal)reader[3];
                        if (reader[4] != DBNull.Value)
                        {
                            naocareModel.Slika = (byte[])reader[4]; // Učitavanje slike iz baze
                        }
                        else
                        {
                            naocareModel.Slika = null; // Postavite Slika svojstvo na null ako je NULL vrednost u bazi
                        }
                        // Postavite vrednost TipNaočara iz rezultata JOIN-a
                        naocareModel.TipNaočara = reader["TipNaočaraNaziv"].ToString();
                        naocareList.Add(naocareModel);
                    }
                }
            }
            return naocareList;
        }

        public List<string> GetTipoviNaočara()
        {
            List<string> tipoviNaočara = new List<string>();

            using (var connection = new SqlConnection(connectionString))
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT Naziv FROM Tip";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nazivTipa = reader["Naziv"].ToString();
                        tipoviNaočara.Add(nazivTipa);
                    }
                }
            }

            return tipoviNaočara;
        }
    }
}
