using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class UI
    {
        public UI()
        {
            string fileName = "vendingmachine.csv";
            string stockingResult = vendingMachine.InitialStocking(fileName);
            Start(stockingResult);
        }
        public VendingMachine vendingMachine { get; } = new VendingMachine();

        private void LogResult(string result)
        {
            if (result != "")
            {
                Console.WriteLine(result);
            }
        }
        private void Start(string result)
        {
            LogResult(result);
            LogResult(vendingMachine.AddToLog("", -1, -1));
            bool machineOn = true;

            Console.WriteLine("Please select a menu option.");
            while (machineOn)
            {
                Console.WriteLine("\n> (1) Display Vending Machine Items\n> (2) Purchase\n> (3) Exit");
                string userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1": //Display Items
                        DisplayItems(vendingMachine.Inventory);
                        break;
                    case "2": //Make Purchase
                        PurchaseMenu();
                        break;
                    case "3": // Exit Program
                        LogResult(vendingMachine.SalesReport()); //Keep track of all purchases made when turned off
                        machineOn = false;
                        break;
                    case "4": // Report
                        Console.WriteLine("Generating Sales Report.");
                        LogResult(vendingMachine.SalesReport());
                        break;
                    default: // Error Statement
                        Console.WriteLine("The option you entered does not exist please try again");
                        break;

                }
            }
        }
        private void PurchaseMenu()
        {
            Console.WriteLine("Please select a menu option.");
            bool currentTransaction = true;
            while (currentTransaction)
            {
                Console.WriteLine($"\n>(1) Feed Money \n>(2) Select Product \n>(3) Finish Transaction \n\n> Current Money Provided: {vendingMachine.CurrentBalance:C2}");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "1": //Feed Money
                        Console.WriteLine("Please enter whole dollar amount.($1, $2, $5, or $10)");
                        string  moneyInput = Console.ReadLine();
                        LogResult(vendingMachine.FeedMoney(moneyInput));
                        break;
                    case "2": //Select Product
                        DisplayItems(vendingMachine.Inventory);
                        Console.Write("Please enter your selection: ");
                        string purchaseInput = Console.ReadLine().ToUpper();
                        LogResult(vendingMachine.MakePurchase(purchaseInput));
                        break;
                    case "3": // Finish Transaction
                        LogResult(vendingMachine.DispenseChange(vendingMachine.CurrentBalance));
                        currentTransaction = false;
                        break;
                    default: // Error Statement
                        Console.WriteLine("Option provided was not valid. Please try again.");
                        break;

                }
            }
        }

        private void DisplayItems(Dictionary<string, ItemContainer> inventory)
        {
            int maxLength = 0;
            foreach (ItemContainer item in inventory.Values)
            {
                if (item.GetProductName().Length > maxLength)
                {
                    maxLength = item.GetProductName().Length;
                }
            }
            foreach (KeyValuePair<string, ItemContainer> kvp in inventory)
            {
                string stockText = $"{kvp.Value.Stock} remaining";
                if (kvp.Value.Stock == 0)
                {
                    stockText = "SOLD OUT";
                }
                Console.WriteLine($"{kvp.Key} --- {kvp.Value.GetProductName().PadRight(maxLength)} {$"{kvp.Value.GetProductPrice():C2}".PadRight(6)}--- {stockText.PadRight(5)}");
            }
            Console.WriteLine("\n");
           
        }
    }
}

