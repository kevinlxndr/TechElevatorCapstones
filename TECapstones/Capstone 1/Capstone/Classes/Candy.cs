using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class Candy : Item
    {
        public Candy(string name, decimal price) : base(name, price) { }
        public override string VendItem()
        {
            return "Munch Munch, Yum!";
        }
    }
}
