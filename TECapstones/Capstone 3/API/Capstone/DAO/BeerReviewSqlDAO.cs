using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BeerReviewSqlDAO : IBeerReviewDAO
    {
        private readonly string connectionString;

        public BeerReviewSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<BeerReview> GetBeerReviews()
        {
            List<BeerReview> beerReviews = new List<BeerReview>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "Select * from beer_reviews";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BeerReview currentReview = GetBeerReviewsFromReader(reader);
                        beerReviews.Add(currentReview);

                    }
                    return beerReviews;
                }
            }
            catch (Exception e)
            {

                throw e;
            }     
           
        }
        public BeerReview AddBeerReview(BeerReview review)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText= "INSERT INTO beer_reviews (beer_id,user_id, title,beerRating, beerReview, is_private) values (@beerId, @userId,@title,@beerRating, @beerReview, @isPrivate)";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@beerId", review.BeerId);
                    cmd.Parameters.AddWithValue("@userId", review.UserId);
                    cmd.Parameters.AddWithValue("@title", review.Title);
                    cmd.Parameters.AddWithValue("@beerRating", review.BeerRating);
                    cmd.Parameters.AddWithValue("@beerReview", review.Review);
                    cmd.Parameters.AddWithValue("@isPrivate", review.isPrivate);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return review;
        }

        private BeerReview GetBeerReviewsFromReader(SqlDataReader reader)
        {
            BeerReview beerReview = new BeerReview()
            {
                BeerReviewId = Convert.ToInt32(reader["beerReview_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                BeerId = Convert.ToInt32(reader["beer_id"]),
                Title = Convert.ToString(reader["title"]),
                BeerRating = Convert.ToInt32(reader["beerRating"]),
                Review = Convert.ToString(reader["beerReview"]),
                isPrivate = Convert.ToInt32(reader["is_private"])
            };
            return beerReview;

        }
    }
}
