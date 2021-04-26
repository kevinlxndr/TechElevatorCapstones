using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;

namespace Capstone.DAO
{
    public interface IBreweryDAO
    {
        Brewery GetBrewery(int id);
        List<Brewery> GetBreweries();
        Brewery AddBrewery(Brewery brewery);
        Brewery UpdateBrewery(int id, Brewery brewery);
    }
}
