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
    public class BreweryController : ControllerBase
    {
        private readonly IBreweryDAO breweryDAO;

        public BreweryController(IBreweryDAO _breweryDAO)
        {
            breweryDAO = _breweryDAO;
        }
        //[AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<Brewery> GetBrewery(int id)
        {
            Brewery existingBrewery = breweryDAO.GetBrewery(id);
            if (existingBrewery != null)
            {
                return Ok(existingBrewery);
            }
            else
            {
                return NotFound("Brewery not found");
            }
        }
        //[AllowAnonymous]
        [HttpGet]
        public List<Brewery> GetBreweries()
        {
            List<Brewery> breweryList = breweryDAO.GetBreweries();
            return breweryList;
        }
        //[Authorize(Roles = "admin, brewer")]
        [HttpPost]
        public ActionResult<Brewery> AddBrewery(Brewery brewery)
        {
            Brewery newBrewery = breweryDAO.AddBrewery(brewery);
            return Created($"/brewery/{newBrewery}", newBrewery);

        }
        //[Authorize(Roles = "admin, brewer")]
        [HttpPut("{id}")]
        public ActionResult<Brewery> UpdateBrewery(int id, Brewery brewery)
        {
            Brewery existingBrewery = breweryDAO.GetBrewery(id);
            if (existingBrewery == null)
            {
                return NotFound("Brewery not found");
            }
            Brewery updatedBrewery = breweryDAO.UpdateBrewery(id, brewery);
            return Created($"/brewery/{id}",updatedBrewery);
        }
    }
}
