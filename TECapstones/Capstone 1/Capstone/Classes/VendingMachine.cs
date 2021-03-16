using System;
using System.Collections.Generic;
using System.Text;
using System.IO;



namespace Capstone.Classes
{
    public class VendingMachine
    {
        public Dictionary<string, ItemContainer> Inventory { get; private set; } = new Dictionary<string, ItemContainer>();
        public decimal CurrentBalance { get; private set; } = 0.00M;
        public string InitialStocking(string fileName)
        {
            string directory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(directory, fileName); ;
            string returnValue = "";
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {

                        string currentline = sr.ReadLine();
                        if (currentline.Contains("|"))
                        {
                            string[] lineArray = currentline.Split("|", StringSplitOptions.RemoveEmptyEntries);
                            string slot = lineArray[0].Trim();
                            string name = lineArray[1].Trim();
                            decimal price = 0m;
                            try
                            {
                                price = Math.Abs(decimal.Parse(lineArray[2].Trim()));
                                switch (lineArray[3].Trim().ToLower())
                                {
                                    case "drink":
                                        Inventory[slot] = new ItemContainer(new Beverage(name, price));
                                        break;

                                    case "candy":
                                        Inventory[slot] = new ItemContainer(new Candy(name, price));
                                        break;

                                    case "chip":
                                        Inventory[slot] = new ItemContainer(new Chips(name, price));
                                        break;

                                    case "gum":
                                        Inventory[slot] = new ItemContainer(new Gum(name, price));
                                        break;
                                    default: // Error Statement
                                        returnValue +=  $"Failure stocking {name} at {slot}. {lineArray[3].Trim()} is not a valid type\n";
                                        break;

                                }
                            }
                            catch
                            {
                               returnValue += $"Failure stocking {name} at {slot}\n";
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                returnValue = "Error reading file.\n" + e.Message;
            }
            return returnValue;
        }
        public string FeedMoney(string userInput)
        {
            string error = "";
            int result = 0;
            try
            {
                if (userInput.Contains("$"))
                {
                    result = Int32.Parse(userInput.Substring(1));
                }
                else
                {
                    result = Int32.Parse(userInput);
                }
            }
            catch (FormatException)
            {
                return $"Unable to parse '{userInput}'";
            }
          
            if (result == 1 || result == 2 || result == 5 || result == 10)
            {
                CurrentBalance += result;
                error = AddToLog("FEED MONEY:", (decimal)result, CurrentBalance);
            }
            else
            {
                return "Amount entered was invalid. Please enter $1, $2, $5, or $10";
            }
            return error;
        }

        public string MakePurchase(string userInput)
        {
            string result = "";
            if (Inventory.ContainsKey(userInput))
            {
                ItemContainer target = Inventory[userInput];
                string name = target.GetProductName();
                decimal price = target.GetProductPrice();
                if (target.Stock > 0 && price <= CurrentBalance)
                {
                    decimal oldBalance = CurrentBalance;
                    CurrentBalance -= price;
                    result = $"\nDispensing {name}: {price:C2} - Remaining Balance: {CurrentBalance:C2}\n";
                    result += target.RemoveStock();

                    string logEntry = $"{name} {userInput}";
                    result += "\n" + AddToLog(logEntry, oldBalance, CurrentBalance);

                }
                else
                {
                    if (target.Stock == 0)
                    {
                        return ("Sorry, this item is sold out.");
                    }
                    else
                    {
                        return ("Sorry, the item costs more than the current balance. Please enter more money.");
                    }
                }
            }
            else
            {
                return ("Sorry, but the slot you entered does not exist. Please try again.");
            }
            Console.WriteLine();
            return result;
        }

        public string DispenseChange(decimal currentBalance)
        {
            if(currentBalance > 0)
            {
                string result = AddToLog("GIVE CHANGE:", currentBalance, 0);
                int quarters = (int)Math.Floor(currentBalance / .25m);
                currentBalance -= .25m * quarters;
                int dimes = (int)Math.Floor(currentBalance / .10m);
                currentBalance -= .10m * dimes;
                int nickels = (int)Math.Floor(currentBalance / .05m);
                currentBalance -= .05m * nickels;
                CurrentBalance = currentBalance;
                return result + ($"\nDispensing {quarters} quarters, {dimes} dimes, and {nickels} nickels.");
            }
            else
            {
                return ("\nNo change to dispense.");
            }

        }

         public string AddToLog(string transactionDetail, decimal number, decimal balanceAfter)
        {
            string result = "";
            string date = System.DateTime.Now.ToString();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
            bool keepFile = (balanceAfter != -1);

            try
            {
                using (StreamWriter wr = new StreamWriter(filePath, keepFile))
                {
                    if (keepFile)
                    {
                        wr.WriteLine($"{date} {transactionDetail} {number:C2} {balanceAfter:C2}");
                    }
                    else
                    {
                        wr.Write("");
                    }
                }
            }
            catch (IOException e)
            {
                result  = "Error writing to file.\n" + e.Message;
            }
            catch (Exception e)
            {
                result = "Unknown Error\n" + e.Message;
            }
            return result;
        }

        public string SalesReport()
        {
            string directory = Directory.GetCurrentDirectory();
            string fullPath = Path.Combine(directory, "SalesReport.txt");
            decimal originalTotal = 0m;
            decimal total = 0m;
            Dictionary<string, int> salesDictionary = new Dictionary<string, int>();
            if (File.Exists(fullPath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(fullPath))
                    {
                        while (!sr.EndOfStream)
                        {
                            string line = sr.ReadLine();
                            if (line.Contains("|"))
                            {
                                string[] array = line.Split("|");
                                salesDictionary[array[0]] = Int32.Parse(array[1]);
                            }
                            else if (!String.IsNullOrEmpty(line))
                            {
                                originalTotal = Decimal.Parse(line.Split("$")[1]);
                            }
                        }
                    }
                }
                catch (IOException e)
                {
                    return ("Error reading in SalesReport.\n" + e.Message);
                }
            }
            foreach (ItemContainer slot in Inventory.Values)
            {
                string name = slot.GetProductName();
                decimal price = slot.GetProductPrice();
                int retrieved = slot.RetrieveForReport();
                if (salesDictionary.ContainsKey(name))
                {
                    salesDictionary[name] += retrieved;
                }
                else
                {
                    salesDictionary.Add(name, retrieved);
                }
                total += price * retrieved;
            }
            total += originalTotal;
            try
            {
                using (StreamWriter wr = new StreamWriter(fullPath,false))
                {
                    foreach(KeyValuePair<string,int> kvp in salesDictionary)   
                    {
                        wr.WriteLine($"{kvp.Key}|{kvp.Value}");
                    }
                    wr.WriteLine($"\n **TOTAL SALES** : {total:C2}");
                }
            }
            catch (IOException e)
            {
                return ("Error writing in SalesReport.\n" + e.Message);
            }
            return "";
        }
    }
}
