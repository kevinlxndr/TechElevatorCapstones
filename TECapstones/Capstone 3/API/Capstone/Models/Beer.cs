using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class Beer
    {
        public int BeerId { get; set; }
        public int BreweryId { get; set; }
        public int BeerTypeId { get; set; }
        public string Name { get; set; }
        public decimal Abv { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }

        public int IsActive { get; set; }

    }
}
