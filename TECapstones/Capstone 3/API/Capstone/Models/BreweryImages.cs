using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BreweryImages
    {
        public int BreweryImageId { get; set; }
        public int BreweryId { get; set; }
        public string BreweryImgPath { get; set; }
    }
}
