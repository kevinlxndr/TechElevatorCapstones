using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Models
{
    public class BeerReview
    {
        public int BeerReviewId { get; set; }
        public int UserId { get; set; }
        public int BeerId { get; set; }
        public int BeerRating { get; set; }
        public string Title { get; set; }
        public string Review { get; set; }
        public int isPrivate { get; set; }
    }
}
