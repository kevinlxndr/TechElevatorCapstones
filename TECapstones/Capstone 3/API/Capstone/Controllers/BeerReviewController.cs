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
    public class BeerReviewController : ControllerBase
    {
        private readonly IBeerReviewDAO beerReviewDAO;

        public BeerReviewController(IBeerReviewDAO _beerReviewDAO)
        {
            beerReviewDAO = _beerReviewDAO;
        }

        [HttpGet]
        public ActionResult<List<BeerReview>> GetBeerReviews()
        {
            List<BeerReview> existingReview = beerReviewDAO.GetBeerReviews();
            if (existingReview != null)
            {
                return Ok(existingReview);
            }
            else
            {
                return NotFound("Reviews not found");
            }
            
        }
        
        [HttpPost]
        public ActionResult<Beer> AddBeerReview(BeerReview review)
        {
            BeerReview newBeerReview = beerReviewDAO.AddBeerReview(review);
            return Created($"/beerreview/{newBeerReview}", newBeerReview);

        }
    }
}
