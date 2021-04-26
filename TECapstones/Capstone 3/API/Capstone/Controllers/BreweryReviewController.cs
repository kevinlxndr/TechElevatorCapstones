using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Controllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class BreweryReviewController : ControllerBase
    {
        private readonly IBreweryReviewDAO breweryReviewDAO;

        public BreweryReviewController(IBreweryReviewDAO _breweryReviewDAO)
        {
            breweryReviewDAO = _breweryReviewDAO;
        }

        [HttpGet]
        public ActionResult<List<BreweryReview>> GetBreweryReviews()
        {
            List<BreweryReview> existingReview = breweryReviewDAO.GetBreweryReviews();
            if (existingReview != null)
            {
                return Ok(existingReview);
            }
            else
            {
                return NotFound("Review not found");
            }
        }

        [HttpPost]
        public ActionResult<Brewery> AddBreweryReview(BreweryReview review)
        {
            BreweryReview newBreweryReview = breweryReviewDAO.AddBreweryReview(review);
            return Created($"/breweryreview/{newBreweryReview}", newBreweryReview);

        }
    }

}
