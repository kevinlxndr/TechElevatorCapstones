using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class FavBreweriesSqlDAO : IFavBreweriesDAO
    {
        private readonly string connectionString;

        public FavBreweriesSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<FavBreweries> GetFavoriteBreweries(int id)
        {
            List<FavBreweries> favorites = new List<FavBreweries>();
            
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "SELECT brewery_id from users_favBreweries where user_id = @userId";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        FavBreweries favBreweries = new FavBreweries();
                        favBreweries.UserId = id;
                        favBreweries.BreweryID = Convert.ToInt32(reader["brewery_id"]);

                        favorites.Add(favBreweries);
                    }

                    return favorites;

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FavBreweries GetFav(int id, int breweryId)
        {
            FavBreweries fav = new FavBreweries();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT user_id from users_favBreweries where brewery_id = @breweryId and user_id = @userId";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@breweryId", breweryId);
                    cmd.Parameters.AddWithValue("@userId", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        fav.UserId = Convert.ToInt32(reader["user_id"]);
                    }
                    return fav;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public FavBreweries AddFavoriteBrewery(FavBreweries favBrewery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "INSERT INTO users_favBreweries(user_id, brewery_id) values (@userId, @breweryId);";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@userId", favBrewery.UserId);
                    cmd.Parameters.AddWithValue("@breweryId", favBrewery.BreweryID);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return favBrewery;
        }
        public void DeleteFav(int id, int breweryId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "DELETE from users_favBreweries where brewery_id = @breweryId and user_id = @userId";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@breweryId", breweryId);
                    cmd.Parameters.AddWithValue("@userId", id);

                    cmd.ExecuteNonQuery();
                   

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
