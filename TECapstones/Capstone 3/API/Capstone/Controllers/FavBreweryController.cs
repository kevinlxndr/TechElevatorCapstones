using Capstone.DAO;
using Capstone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Capstone.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FavBreweryController : ControllerBase
    {
        private readonly IFavBreweriesDAO favBreweryDAO;

        public FavBreweryController(IFavBreweriesDAO _favBreweryDAO)
        {
            favBreweryDAO = _favBreweryDAO;
        }

        [HttpGet("{id}")]
        public ActionResult<List<FavBreweries>> GetFavoriteBreweries(int id)
        {
            List<FavBreweries> favsList = favBreweryDAO.GetFavoriteBreweries(id);
            if (favsList != null)
            {
                return Ok(favsList);
            }
            else
            {
                return NotFound("Reviews not found");
            }
            
        }
        [HttpPost]
        public ActionResult<FavBreweries> AddFavoriteBrewrey(FavBreweries favBrewery)
        {
            FavBreweries newFave = favBreweryDAO.AddFavoriteBrewery(favBrewery);
            return Created($"/beer/{newFave}", newFave);
        }

        [HttpDelete("{id}/{breweryId}")]
        public ActionResult<FavBreweries> DeleteFav(int id, int breweryId)
        {
            FavBreweries favBreweries = favBreweryDAO.GetFav(id, breweryId);
            if (favBreweries == null)
            {
                return NotFound("Id does not exist");
            }
            favBreweryDAO.DeleteFav(id, breweryId);
            return NoContent();
        }


    }
}
