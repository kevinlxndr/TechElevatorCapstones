using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.Security;
using Capstone.Security.Models;

namespace Capstone.DAO
{
    public class BrewerySqlDAO : IBreweryDAO
    {
        private readonly string connectionString;

        public BrewerySqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public Brewery GetBrewery(int id)
        {
            Brewery returnBrewery = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM breweries WHERE brewery_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    { 
                        returnBrewery = GetBreweryFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnBrewery;
        }

        public List<Brewery> GetBreweries()
        {
            List<Brewery> breweryList = new List<Brewery>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * from breweries",conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Brewery currentBrewery = GetBreweryFromReader(reader);
                        breweryList.Add(currentBrewery);
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return breweryList;

        }

        public Brewery AddBrewery(Brewery brewery)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO breweries (name, brewer_id, street_address1, street_address2, city, state, zip, phone, history, hours_of_operation, website, brewery_status_id) " +
                        "VALUES (@name, @brewer_id, @street_address1, @street_address2, @city, @state, @zip, @phone, @history, @hours_of_operation, @website, 1); Select SCOPE_IDENTITY()", conn);
                    cmd.Parameters.AddWithValue("@name", brewery.Name);
                    cmd.Parameters.AddWithValue("@brewer_id", brewery.BrewerId);
                    cmd.Parameters.AddWithValue("@street_address1", brewery.StreetAddress1);
                    cmd.Parameters.AddWithValue("@street_address2", brewery.StreetAddress2);
                    cmd.Parameters.AddWithValue("@city", brewery.City);
                    cmd.Parameters.AddWithValue("@state", brewery.State);
                    cmd.Parameters.AddWithValue("@zip", brewery.Zip);
                    cmd.Parameters.AddWithValue("@phone", brewery.Phone);
                    cmd.Parameters.AddWithValue("@history", brewery.History);
                    cmd.Parameters.AddWithValue("@hours_of_operation", brewery.HoursOfOperation);
                    cmd.Parameters.AddWithValue("@website", brewery.Website);
                    decimal new_id = (Decimal)cmd.ExecuteScalar();

                    cmd = new SqlCommand("insert into brewery_images(brewery_id, brewery_img_path) values(@id, 'temp')", conn);
                    cmd.Parameters.AddWithValue("@id", new_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return brewery;
        }

        public Brewery UpdateBrewery(int id, Brewery brewery)
        {
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string sqlTest = "UPDATE breweries set name = @name, street_address1 = @street_address1, " +
                        "street_address2 = @street_address2, city = @city, state = @state, zip = @zip, phone = @phone, history = @history, " +
                        "hours_of_operation = @hours_of_operation, website = @website, brewery_status_id = @brewery_status " +
                        "where brewery_id = @breweryId;";
                    SqlCommand cmd = new SqlCommand(sqlTest, conn);
                    cmd.Parameters.AddWithValue("@name", brewery.Name);
                    cmd.Parameters.AddWithValue("@street_address1", brewery.StreetAddress1);
                    cmd.Parameters.AddWithValue("@street_address2", brewery.StreetAddress2);
                    cmd.Parameters.AddWithValue("@city", brewery.City);
                    cmd.Parameters.AddWithValue("@state", brewery.State);
                    cmd.Parameters.AddWithValue("@zip", brewery.Zip);
                    cmd.Parameters.AddWithValue("@phone", brewery.Phone);
                    cmd.Parameters.AddWithValue("@history", brewery.History);
                    cmd.Parameters.AddWithValue("@brewery_status", brewery.BreweryStatus);
                    cmd.Parameters.AddWithValue("@breweryId", id);
                    cmd.Parameters.AddWithValue("@hours_of_operation", brewery.HoursOfOperation);
                    cmd.Parameters.AddWithValue("@website", brewery.Website);
                    cmd.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return brewery;
        }

        private Brewery GetBreweryFromReader(SqlDataReader reader)
        {
            Brewery brewery = new Brewery()
            {
                BreweryId = Convert.ToInt32(reader["brewery_id"]),
                Name = Convert.ToString(reader["name"]),
                BrewerId = Convert.ToInt32(reader["brewer_id"]),
                StreetAddress1 = Convert.ToString(reader["street_address1"]),
                StreetAddress2 = Convert.ToString(reader["street_address2"]),
                City = Convert.ToString(reader["city"]),
                State = Convert.ToString(reader["state"]),
                Zip = Convert.ToInt32(reader["zip"]),
                Phone = Convert.ToString(reader["phone"]),
                History = Convert.ToString(reader["history"]),
                HoursOfOperation = Convert.ToString(reader["hours_of_operation"]),
                Website = Convert.ToString(reader["website"]),
                BreweryStatus = Convert.ToInt32(reader["brewery_status_id"])


            };

            return brewery;
        }
    }
}
