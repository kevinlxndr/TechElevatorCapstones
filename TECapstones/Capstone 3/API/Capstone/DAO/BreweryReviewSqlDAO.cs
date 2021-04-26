using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public class BreweryReviewSqlDAO : IBreweryReviewDAO
    {
        private readonly string connectionString;

        public BreweryReviewSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<BreweryReview> GetBreweryReviews()
        {
            List<BreweryReview> breweryReviews = new List<BreweryReview>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "Select * from brewery_reviews";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        BreweryReview currentReview = GetBreweryReviewsFromReader(reader);
                        breweryReviews.Add(currentReview);

                    }
                    return breweryReviews;
                }
            }
            catch (Exception e)
            {

                throw e;
            }

        }
        public BreweryReview AddBreweryReview(BreweryReview review)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "INSERT INTO brewery_reviews (brewery_id, user_id, rating, title, review, is_private) values (@breweryId, @userId,  @rating, @title,@review, @isPrivate)";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@breweryId", review.BreweryId);
                    cmd.Parameters.AddWithValue("@userId", review.UserId);
                    cmd.Parameters.AddWithValue("@review", review.Review);
                    cmd.Parameters.AddWithValue("@title", review.Title);
                    cmd.Parameters.AddWithValue("@rating", review.BreweryRating);
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

        private BreweryReview GetBreweryReviewsFromReader(SqlDataReader reader)
        {
            BreweryReview beerReview = new BreweryReview()
            {
                BreweryReviewId = Convert.ToInt32(reader["brewery_review_id"]),
                BreweryId = Convert.ToInt32(reader["brewery_id"]),
                UserId = Convert.ToInt32(reader["user_id"]),
                Title = Convert.ToString(reader["title"]),
                Review = Convert.ToString(reader["review"]),
                BreweryRating = Convert.ToInt32(reader["rating"]),
                isPrivate = Convert.ToInt32(reader["is_private"])
            };
            return beerReview;

        }
    }
}
