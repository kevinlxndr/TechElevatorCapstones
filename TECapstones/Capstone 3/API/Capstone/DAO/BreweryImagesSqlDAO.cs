using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BreweryImagesSqlDAO : IBreweryImagesDAO
    {
        private readonly string connectionString;

        public BreweryImagesSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public BreweryImages GetBreweryImages(int id)
        {
            BreweryImages returnBreweryImages = new BreweryImages();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM brewery_images WHERE brewery_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnBreweryImages.BreweryImageId = Convert.ToInt32(reader["brewery_img_id"]);
                        returnBreweryImages.BreweryId = id;
                        returnBreweryImages.BreweryImgPath = Convert.ToString(reader["brewery_img_path"]);
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return returnBreweryImages;
        }
        public void UpdateBreweryImage(BreweryImages img)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE brewery_images set brewery_img_path=@img where brewery_id = @id ", conn);
                    cmd.Parameters.AddWithValue("@id", img.BreweryId);
                    cmd.Parameters.AddWithValue("@img", img.BreweryImgPath);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}

