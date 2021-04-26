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
    public class BeerImagesController : ControllerBase
    {
        private readonly IBeerImagesDAO beerImagesDAO;

        public BeerImagesController(IBeerImagesDAO _beerImagesDAO)
        {
            beerImagesDAO = _beerImagesDAO;
        }
        [HttpGet]
        public List<BeerImages> GetBeerImages(int id)
        {
            List<BeerImages> beerImagesList = beerImagesDAO.GetBeerImages(id);
            return beerImagesList;
        }
    }
}
