using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BeerImages
    {
        public int BeerImageId { get; set; }
        public int BeerId { get; set; }
        public string BeerImgPath { get; set; }
    }
}
