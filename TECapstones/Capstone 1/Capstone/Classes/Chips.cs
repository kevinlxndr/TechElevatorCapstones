using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Chips : Item
    {
        public Chips(string name, decimal price) : base(name, price) { }
        public override string VendItem()
        {
            return "Crunch Crunch, Yum!";
        }
    }
}

