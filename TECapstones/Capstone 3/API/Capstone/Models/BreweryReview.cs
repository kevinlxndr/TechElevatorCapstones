using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BreweryReview
    {
        public int BreweryReviewId { get; set; }
        public int UserId { get; set; }
        public int BreweryId { get; set; }
        public int BreweryRating { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public int isPrivate { get; set; }
    }
}
