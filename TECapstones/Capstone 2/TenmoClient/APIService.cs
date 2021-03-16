using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Net.Http;
using TenmoClient.Data;

namespace TenmoClient
{
    public class APIService
    {
        private readonly string API_URL = "";
        private readonly RestClient client = new RestClient();
        public APIService(string api_url)
        {
            API_URL = api_url;
        }

        public List<API_User> GetUsers() 
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "users");
            IRestResponse<List<API_User>> response = client.Get<List<API_User>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public LoginUser GetUser(int id)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + "users/"+id);
            IRestResponse<LoginUser> response = client.Get<LoginUser>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public int TransferMoney(int fromId, int toId, decimal amount)
        {
            Transfer transfer = new Transfer();
            transfer.FromUserId = fromId;
            transfer.ToUserId = toId;
            transfer.AmountTransfered = amount;

            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"transfer");
            request.AddJsonBody(transfer);
            IRestResponse<int> response = client.Put<int>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return 0;
        }

        public int GetUserId(string username)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"users");
            request.AddParameter("username", username);
            IRestResponse<int> response = client.Get<int>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return -1;
        }

        public decimal GetBalanceForUser(int id)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"users/{id}/balance");
            IRestResponse<Decimal> response = client.Get<Decimal>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return 0;
        }

        public List<Transfer> GetPastTransfers(int id)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"users/{id}/transfers");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public List<Transfer> GetPendingTransfers(int id)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"users/{id}/transfers/pending");
            IRestResponse<List<Transfer>> response = client.Get<List<Transfer>>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public Transfer GetTransferDetails(int transferId)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"transfer/{transferId}/details");
            IRestResponse<Transfer> response = client.Get<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
            }
            return null;
        }

        public bool RequestTransfer(int userId, int requestId, decimal amount)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"transfer/request");

            Transfer transfer = new Transfer();
            transfer.FromUserId = requestId;
            transfer.ToUserId = userId;
            transfer.AmountTransfered = amount;
            transfer.Status = "Pending";
            transfer.Type = "Request";
            request.AddJsonBody(transfer);
            IRestResponse<bool> response = client.Post<bool>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return true;
            }
            return false;
        }
        public bool UpdateRequest(int transferId, int selection)
        {
            client.Authenticator = new JwtAuthenticator(UserService.GetToken());
            RestRequest request = new RestRequest(API_URL + $"transfer/request/update");

            Transfer transfer = GetTransferDetails(transferId);
            transfer.Status = selection ==1 ? "Approved" : "Rejected";
            request.AddJsonBody(transfer);
            IRestResponse<Transfer> response = client.Put<Transfer>(request);

            if (response.ResponseStatus != ResponseStatus.Completed || !response.IsSuccessful)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return true;
            }
            return false;
        }

        private void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpRequestException("Authorization is required for this endpoint.Please log in.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new HttpRequestException("You do not have permission to perform the requested action");
                }
                else
                {
                    throw new HttpRequestException("Error occurred - received non-success response: " + (int)response.StatusCode);
                }
            }
        }
    }
}
