using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BeerImagesSqlDAO : IBeerImagesDAO
    {
        private readonly string connectionString;

        public BeerImagesSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public List<BeerImages> GetBeerImages(int id)
        {
            List<BeerImages> beerImages = new List<BeerImages>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT beer_img_path FROM beer_images WHERE beer_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        BeerImages returnBeerImages = new BeerImages();
                        returnBeerImages.BeerImgPath = Convert.ToString(reader["beer_img_path"]);
                        beerImages.Add(returnBeerImages);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return beerImages;
        }

    }
}
