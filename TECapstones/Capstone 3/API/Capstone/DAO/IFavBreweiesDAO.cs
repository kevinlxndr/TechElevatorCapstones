using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.DAO
{
    public interface IFavBreweriesDAO
    {
        List<FavBreweries> GetFavoriteBreweries(int id);
        void DeleteFav(int id, int breweryId);
        FavBreweries GetFav(int id, int breweryId);
        FavBreweries AddFavoriteBrewery(FavBreweries favBrewery);
    }
}
