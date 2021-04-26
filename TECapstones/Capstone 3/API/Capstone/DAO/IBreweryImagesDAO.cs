using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IBreweryImagesDAO
    {
        BreweryImages GetBreweryImages(int id);
        void UpdateBreweryImage(BreweryImages img);
    }
}
