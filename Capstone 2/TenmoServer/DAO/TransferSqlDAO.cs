using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using TenmoServer.Models;
using TenmoServer.Security;
using TenmoServer.Security.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;
        const decimal startingBalance = 1000;

        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public int AddTransfer(Transfer transfer)
        { 
            int createdId = 0;
            bool request = transfer.Type == "Request";
            int typeId =  request ? 1 : 2;
            int statusId = request ? 1 : 2;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "Insert into transfers" +
                        " (transfer_type_id,transfer_status_id,account_from,account_to,amount)" +
                        "values(@typeId,@statusId,@from,@to,@amount);" +
                        "select SCOPE_IDENTITY()";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@from", transfer.FromUserId);
                    cmd.Parameters.AddWithValue("@typeId", typeId);
                    cmd.Parameters.AddWithValue("@statusId", statusId);
                    cmd.Parameters.AddWithValue("@to", transfer.ToUserId);
                    cmd.Parameters.AddWithValue("@amount", transfer.AmountTransfered);
                    createdId = int.Parse(cmd.ExecuteScalar().ToString());
                }
            }
            catch (SqlException e)
            {
                throw e;
            }

            return createdId;
        }

        public Transfer UpdateTransfer(Transfer transfer)
        {
            Transfer result = null;
            int selection = transfer.Status == "Approved" ? 2 : 3;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "Update transfers set transfer_status_id = @statusId  where transfer_id = @transferId";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@statusId", selection);
                    cmd.Parameters.AddWithValue("@transferId", transfer.TransferId);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        result = transfer;
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return result;
        }

        public List<Transfer> GetTransfersById(int id, bool pending = false)
        {
            List<Transfer> transfers = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "";
                    if (pending)
                    {
                        sqlText = "select transfer_id, transfer_status_id,account_from, account_to, u1.username as from_username, u2.username as to_username," +
                        " amount from transfers join users as u1 on u1.user_id = transfers.account_from" +
                        " join users as u2 on u2.user_id = transfers.account_to " +
                        " where transfers.account_from = @id and transfer_status_id = 1";
                    }
                    else
                    {
                        sqlText = "select transfer_id,account_from, account_to, u1.username as from_username, u2.username as to_username," +
                                  " amount from transfers join users as u1 on u1.user_id = transfers.account_from" +
                                  " join users as u2 on u2.user_id = transfers.account_to " +
                                  " where transfers.account_from = @id or transfers.account_to = @id";
                    }
                    

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        transfers.Add(GetPartialTransferFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return transfers;

        }public Transfer GetTransferDetails(int id)
        {
            Transfer transfer = new Transfer();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "select transfer_id,account_from, account_to, u1.username as from_username, u2.username as to_username, transfer_types.transfer_type_desc, transfer_statuses.transfer_status_desc, amount from transfers " +
                        "join users as u1 on u1.user_id = transfers.account_from " +
                        "join users as u2 on u2.user_id = transfers.account_to " +
                        "join transfer_types on transfers.transfer_type_id = transfer_types.transfer_type_id " +
                        "join transfer_statuses on transfers.transfer_status_id = transfer_statuses.transfer_status_id " +
                        "where transfers.transfer_id = @id";

                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.HasRows && reader.Read())
                    {
                        transfer = (GetTransferFromReader(reader));
                    }
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return transfer;
        }

        private Transfer GetPartialTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                FromUserName = Convert.ToString(reader["from_username"]),
                ToUserName = Convert.ToString(reader["to_username"]),
                FromUserId = Convert.ToInt32(reader["account_from"]),
                ToUserId = Convert.ToInt32(reader["account_to"]),
                AmountTransfered = Convert.ToDecimal(reader["amount"])
            };
            return transfer;

        }


            private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                FromUserName = Convert.ToString(reader["from_username"]),
                ToUserName = Convert.ToString(reader["to_username"]),
                FromUserId = Convert.ToInt32(reader["account_from"]),
                ToUserId = Convert.ToInt32(reader["account_to"]),
                AmountTransfered = Convert.ToDecimal(reader["amount"]),
                Type = Convert.ToString(reader["transfer_type_desc"]),
                Status = Convert.ToString(reader["transfer_status_desc"])
            };
            return transfer;
        }
        
    }
}
