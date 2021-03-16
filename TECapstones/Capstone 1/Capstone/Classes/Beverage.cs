using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Beverage:Item
    {
        public Beverage(string name,  decimal price) : base(name, price){}
        public override string VendItem()
        {
            return "Glug Glug, Yum!";
        }

    }
}
