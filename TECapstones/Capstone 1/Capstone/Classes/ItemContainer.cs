using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class ItemContainer
    {
        public ItemContainer(Item product)
        {
            Product = product;
        }
        private Item Product { get; }

        private static int InitialStock = 5;
        public int Stock { get; private set; } = InitialStock;
        private int BoughtSinceLastReport { get; set; } = 0;


        public string RemoveStock()
        {
            string str = "";
            if (Stock > 0)
            {
                Stock--;
                BoughtSinceLastReport++;
                str = Product.VendItem();
            }
            return str;
        }

        public int RetrieveForReport()
        {
            int temp = BoughtSinceLastReport;
            BoughtSinceLastReport = 0;
            return temp;
        }
        public string GetProductName()
        {
            return Product.Name;
        }
        public decimal GetProductPrice()
        {
            return Product.Price;
        }
    }
}
