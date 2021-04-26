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
    public class BeerController : ControllerBase
    {
        private readonly IBeerDAO beerDAO;

        public BeerController(IBeerDAO _beerDAO)
        {
            beerDAO = _beerDAO;
        }
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Beer> GetBeer(int id)
        {
            Beer existingBeer = beerDAO.GetBeer(id);
            if (existingBeer != null)
            {
                return Ok(existingBeer);
            }
            else
            {
                return NotFound();
            }
            
        }
        //[AllowAnonymous]
        [HttpGet]
        public List<Beer> GetBeers()
        {
            List<Beer> beerList = beerDAO.GetBeers();
            return beerList;
        }
        //[Authorize(Roles="admin, brewer")]
        [HttpPost]
        public ActionResult<Beer> AddBeer(Beer beer)
        {
            Beer newBeer = beerDAO.AddBeer(beer);
            return Created($"/beer/{newBeer}", newBeer);

        }
        //[Authorize(Roles = "admin, brewer")]
        [HttpPut("{id}/delete")]
        public ActionResult<Beer> DeleteBeer(int id, Beer beer)
        {
            Beer existingBeer = beerDAO.GetBeer(id);
            if (existingBeer == null)
            {
                return NotFound("Beer not found");
            }
            Beer updateBeer = beerDAO.DeleteBeer(id, beer);
            return Ok(updateBeer);
        }
        [HttpPut("{id}/update")]
        public ActionResult<Beer> UpdateBeer(int id, Beer beer)
        {
            Beer existingBeer = beerDAO.GetBeer(id);
            if (existingBeer == null)
            {
                return NotFound("Beer not found");
            }
            Beer updatedBeer = beerDAO.UpdateBeer(id, beer);
            return Created($"/beer/{id}/update", updatedBeer);
        }

    }
}
