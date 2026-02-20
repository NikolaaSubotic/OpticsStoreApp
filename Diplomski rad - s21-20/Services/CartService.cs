using System;
using System.Data;
using System.Data.SqlClient;

namespace Diplomski_rad___s21_20.Services
{
    public class CartService
    {
        private readonly string connectionString;

        public CartService(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddToCart(string korisnikUsername, int naocareId, int kolicina)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"INSERT INTO Korpa (Korisnik_Id, Naocare_Id, Kolicina)
                                 VALUES ((SELECT id FROM Korisnici WHERE username = @korisnikUsername), @naocareId, @kolicina)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@korisnikUsername", SqlDbType.NVarChar).Value = korisnikUsername;
                    command.Parameters.Add("@naocareId", SqlDbType.Int).Value = naocareId;
                    command.Parameters.Add("@kolicina", SqlDbType.Int).Value = kolicina;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
