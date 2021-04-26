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
    [Route("images")]
    [ApiController]
    public class BreweryImagesController : ControllerBase
    {
        private readonly IBreweryImagesDAO breweryImagesDAO;

        public BreweryImagesController(IBreweryImagesDAO _breweryImagesDAO)
        {
            breweryImagesDAO = _breweryImagesDAO;
        }
        [HttpGet("{id}")]
        public BreweryImages GetBreweryImages(int id)
        {
            BreweryImages breweryImage = breweryImagesDAO.GetBreweryImages(id);
            return breweryImage;
        }

        [HttpPut]
        public ActionResult UpdateBreweryImages(BreweryImages img)
        {
            breweryImagesDAO.UpdateBreweryImage(img);
            return Ok();
        }
    }
}
