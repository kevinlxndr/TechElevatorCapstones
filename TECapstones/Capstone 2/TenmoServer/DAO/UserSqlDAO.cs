using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class UserSqlDAO : IUserDAO
    {
        private readonly string connectionString;
        const decimal startingBalance = 1000;

        public UserSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public User GetUser(string username)
        {
            User returnUser = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt FROM users WHERE username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        returnUser = GetUserFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUser;
        }

        public List<User> GetUsers()
        {
            List<User> returnUsers = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT user_id, username, password_hash, salt FROM users", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            User u = GetUserFromReader(reader);
                            returnUsers.Add(u);
                        }

                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return returnUsers;
        }

        public User AddUser(string username, string password)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(password);

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO users (username, password_hash, salt) VALUES (@username, @password_hash, @salt)", conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password_hash", hash.Password);
                    cmd.Parameters.AddWithValue("@salt", hash.Salt);
                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT @@IDENTITY", conn);
                    int userId = Convert.ToInt32(cmd.ExecuteScalar());

                    cmd = new SqlCommand("INSERT INTO accounts (user_id, balance) VALUES (@userid, @startBalance)", conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    cmd.Parameters.AddWithValue("@startBalance", startingBalance);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return GetUser(username);
        }

        public decimal GetCurrentBalance(int id)
        {
            decimal balance = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "select balance from accounts where user_id = @id";
                    SqlCommand cmd = new SqlCommand(sqlText,conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            balance = Convert.ToDecimal(reader["balance"]);
                        }
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return balance;
        }

        public bool TransferFunds( int fromUserId, int toUserId, decimal amountTransfered)
        {
            int rowsAffectedTo = 0;
            int rowsAffectedFrom = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    string sqlText = "update accounts set balance = (balance - @amount) where user_id = @id";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@id", fromUserId);
                    cmd.Parameters.AddWithValue("@amount", amountTransfered);
                    cmd.Transaction = transaction;
                    rowsAffectedFrom = cmd.ExecuteNonQuery();

                    sqlText = "update accounts set balance = (balance + @amount) where user_id = @id";
                    cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@id", toUserId);
                    cmd.Parameters.AddWithValue("@amount", amountTransfered);
                    cmd.Transaction = transaction;
                    rowsAffectedTo = cmd.ExecuteNonQuery();

                    if (rowsAffectedFrom == 1 && rowsAffectedTo == 1)
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            if (rowsAffectedFrom == 1 && rowsAffectedTo == 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        private User GetUserFromReader(SqlDataReader reader)
        {
            User u = new User()
            {
                UserId = Convert.ToInt32(reader["user_id"]),
                Username = Convert.ToString(reader["username"]),
                PasswordHash = Convert.ToString(reader["password_hash"]),
                Salt = Convert.ToString(reader["salt"]),
            };

            return u;
        }
    }
}
