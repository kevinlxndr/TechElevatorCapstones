using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class BeerSqlDAO : IBeerDAO
    {
        private readonly string connectionString;

        public BeerSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Beer GetBeer(int id)
        {
            Beer returnBeer = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT beer_id, brewery_id, beer_type_id, name, abv, description, ingredients, isActive FROM beers WHERE beer_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        returnBeer = GetBeerFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnBeer;
        }

        public List<Beer> GetBeers()
        {
            List<Beer> beerList = new List<Beer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from beers",conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Beer currentBeer = GetBeerFromReader(reader);
                        beerList.Add(currentBeer);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return beerList;

        }

        public Beer AddBeer(Beer beer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO beers (name, abv, brewery_id, beer_type_id, description, ingredients, isActive) VALUES (@name, @abv, @breweryId, @beerTypeId, @description, @ingredients, 1)", conn);
                    cmd.Parameters.AddWithValue("@name", beer.Name);
                    cmd.Parameters.AddWithValue("@breweryId", beer.BreweryId);
                    cmd.Parameters.AddWithValue("@beerTypeId", beer.BeerTypeId);
                    cmd.Parameters.AddWithValue("@abv", beer.Abv);
                    cmd.Parameters.AddWithValue("@description", beer.Description);
                    cmd.Parameters.AddWithValue("@ingredients", beer.Ingredients);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return beer;
        }
        public Beer DeleteBeer(int id, Beer beer)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlText = "UPDATE beers set isActive = 0 where beer_id = @beerId;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@beerId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return beer;
        }

        public Beer UpdateBeer(int id, Beer beer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlTest = "UPDATE beers set name = @name, brewery_id = @breweryId, beer_type_id=@beer_type_id," +
                        " abv=@abv, description=@description, ingredients = @ingredients where beer_id = @beerId;";
                    SqlCommand cmd = new SqlCommand(sqlTest, conn);
                    cmd.Parameters.AddWithValue("@name", beer.Name);
                    cmd.Parameters.AddWithValue("@breweryId", beer.BreweryId);
                    cmd.Parameters.AddWithValue("@beer_type_id", beer.BeerTypeId);
                    cmd.Parameters.AddWithValue("@abv", beer.Abv);
                    cmd.Parameters.AddWithValue("@description", beer.Description);
                    cmd.Parameters.AddWithValue("@ingredients", beer.Ingredients);
                    cmd.Parameters.AddWithValue("@beerId", id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return beer;
        }

        private Beer GetBeerFromReader(SqlDataReader reader)
            {
                Beer beer = new Beer()
                {
                    BeerId = Convert.ToInt32(reader["beer_id"]),
                    BreweryId = Convert.ToInt32(reader["brewery_id"]),
                    BeerTypeId = Convert.ToInt32(reader["beer_type_id"]),
                    Name = Convert.ToString(reader["name"]),
                    Abv = Convert.ToDecimal(reader["abv"]),
                    Description = Convert.ToString(reader["description"]),
                    Ingredients = Convert.ToString(reader["ingredients"]),
                    IsActive = Convert.ToInt32(reader["isActive"])
                };

                return beer;
            }
    }
}
