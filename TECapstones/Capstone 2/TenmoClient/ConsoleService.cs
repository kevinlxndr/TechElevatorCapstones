using System;
using System.Collections.Generic;
using TenmoClient.Data;


namespace TenmoClient
{
    public class ConsoleService
    {
        private APIService apiService { get; }
        public ConsoleService()
        {
            apiService = new APIService("https://localhost:44315/");
        }


        /// <summary>
        /// Prompts for transfer ID to view, approve, or reject
        /// </summary>
        /// <param name="action">String to print in prompt. Expected values are "Approve" or "Reject" or "View"</param>
        /// <returns>ID of transfers to view, approve, or reject</returns>
        public int PromptForTransferID(string action)
        {
            Console.WriteLine("");
            Console.Write("Please enter transfer ID to " + action + " (0 to cancel): ");
            if (!int.TryParse(Console.ReadLine(), out int auctionId))
            {
                Console.WriteLine("Invalid input. Only input a number.");
                return 0;
            }
            else
            {
                return auctionId;
            }
        }

        public LoginUser PromptForLogin()
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();
            string password = GetPasswordFromConsole("Password: ");

            LoginUser loginUser = new LoginUser
            {
                Username = username,
                Password = password
            };
            return loginUser;
        }

        private string GetPasswordFromConsole(string displayMessage)
        {
            string pass = "";
            Console.Write(displayMessage);
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Backspace Should Not Work
                if (!char.IsControl(key.KeyChar))
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Remove(pass.Length - 1);
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter);
            Console.WriteLine("");
            return pass;
        }

        public decimal GetBalance(int id)
        {
            decimal balance = apiService.GetBalanceForUser(id);
            Console.WriteLine($"\nYour current balance is: {balance:c2}");
            return balance;
        }

        public int PromptForUserId()
        {
            List<API_User> userList = apiService.GetUsers();
            List<int> userIds = new List<int>();
            Console.WriteLine(
                "---------------------------------------\n" +
                "Users\n" +
                "ID".PadRight(10) + "Name\n" +
                "---------------------------------------\n");

            for (int i = 0; i < userList.Count; i++)
            {
                Console.WriteLine($"{userList[i].UserId}".PadRight(10) + $"{userList[i].Username}");
                userIds.Add(userList[i].UserId);
            }

            while (true)
            {
                Console.Write("\nPlease enter the ID of the user you are sending to (0 to cancel):\n");
                int.TryParse(Console.ReadLine(), out int userId);

                if (userId == 0)
                {
                    return 0;
                }
                else if (!userIds.Contains(userId) || UserService.GetUserId() == userId)
                {
                    Console.WriteLine("Invalid User ID. Please try again.");
                }
                else
                {
                    return userId;
                }
            }

        }

        public decimal PromptForTransferAmount(bool request)
        {
            decimal balance = apiService.GetBalanceForUser(UserService.GetUserId());

            while (true)
            {

                Console.Write("\nEnter amount:\n");
                int.TryParse(Console.ReadLine(), out int amount);

                if (amount > balance && !request)
                {
                    Console.WriteLine("Not enough money to transfer. Please enter a valid amount.");
                }
                else if (amount < 0)
                {
                    Console.WriteLine("Please enter a positive amount.");
                }
                else
                {
                    return amount;
                }
            }

        }

        public bool Transfer()
        {
            int userId = PromptForUserId();
            if (userId != 0)
            {
                decimal amount = PromptForTransferAmount(false);
                int createdId = apiService.TransferMoney(UserService.GetUserId(), userId, amount);
                if (createdId != 0)
                {
                    Console.WriteLine($"\nTransfer for {amount:C2} with Id {createdId} was successful.");
                }
                else
                {
                    Console.WriteLine("\nTransfer failed");
                }
                return true;
            }
            return false;
        }

        public bool GetPastTransfers()
        {
            List<Transfer> transfers = apiService.GetPastTransfers(UserService.GetUserId());
            List<int> transferIds = new List<int>();
            Console.WriteLine(
               "--------------------------------------------------\n" +
               "Transfers\n" +
               "ID".PadRight(15) + "From/To".PadRight(25) + "Amount\n" +
               "--------------------------------------------------\n");

            foreach (Transfer transfer in transfers)
            {
                transferIds.Add(transfer.TransferId);
                int currentUser = UserService.GetUserId();
                if (transfer.ToUserId == currentUser && transfer.Type != "Request")
                {
                    Console.WriteLine($"{transfer.TransferId}".PadRight(15) + $"From: {transfer.FromUserName}".PadRight(25) + $"{transfer.AmountTransfered:C2}");
                }
                else
                {
                    Console.WriteLine($"{transfer.TransferId}".PadRight(17) + $"To: {transfer.ToUserName}".PadRight(23) + $"{transfer.AmountTransfered:C2}");
                }
            }

            Console.WriteLine("\nPlease enter transfer id to view details (0 to cancel)");
            int.TryParse(Console.ReadLine(), out int transferId);
            if (transferId != 0 && transferIds.Contains(transferId))
            {
                Transfer transfer = apiService.GetTransferDetails(transferId);
                Console.WriteLine(
                    "\n-----------------------------------\n" +
                    "Transfer Details\n" +
                    "-------------------------------------\n" +
                    $"Id: {transfer.TransferId}\n" +
                    $"From: {transfer.FromUserName}\n" +
                    $"To: {transfer.ToUserName}\n" +
                    $"Type: {transfer.Type}\n" +
                    $"Status: {transfer.Status}\n" +
                    $"Amount: {transfer.AmountTransfered:C2}\n");
            }
            return true;

        }

        public bool RequestTransfer()
        {
            int requestUserId = PromptForUserId();
            decimal requestAmount = PromptForTransferAmount(true);
            bool result = apiService.RequestTransfer(UserService.GetUserId(), requestUserId, requestAmount);
            if (result)
            {
                Console.WriteLine("Transfer Request Sent.");
                return true;
            }
            Console.WriteLine("Transfer Request Failed.");
            return false;
        }

        public bool GetPendingTransfers()
        {
            List<Transfer> transfers = apiService.GetPendingTransfers(UserService.GetUserId());
            List<int> transferIds = new List<int>();
            Console.WriteLine(
               "--------------------------------------------------\n" +
               "Pending Transfers\n" +
               "ID".PadRight(20) + "To".PadRight(20) + "Amount\n" +
               "--------------------------------------------------\n");

            foreach (Transfer transfer in transfers)
            {
                transferIds.Add(transfer.TransferId);
                int currentUser = UserService.GetUserId();
                Console.WriteLine($"{transfer.TransferId}".PadRight(20) + $"To: {transfer.ToUserName}".PadRight(20) + $"{transfer.AmountTransfered:C2}");
            }

            Console.WriteLine("\nPlease enter transfer id to approve/reject (0 to cancel)");
            int.TryParse(Console.ReadLine(), out int transferId);
            if (transferId != 0 && transferIds.Contains(transferId))
            {
                Console.WriteLine("\n1: Approve\n" +
                "2: Reject\n" +
                "3: Don't approve or reject\n" +
                "---------------------------\n" +
                "Please choose an option:");
                int.TryParse(Console.ReadLine(),out int selection);
                
                if(selection != 3)
                {
                    if (selection == 1 && apiService.GetBalanceForUser(UserService.GetUserId()) < transfers.Find(x => x.TransferId == transferId).AmountTransfered)
                    {
                        Console.WriteLine("Unable to accept that request. It is for more money than you have.");
                        return false;
                    }
                    else
                    {
                        apiService.UpdateRequest(transferId, selection);
                        string str = selection == 1 ? "approved" : "rejected";
                        Console.WriteLine($"\nRequest ID: {transferId} successfully {str}.\n");
                        return true;
                    }
                }
            }
            return true;
        }


    }

}
