using System.Collections.Generic;
using System.Linq;
using CocktailMagician.Data.Models;

namespace CocktailMagician.Services.SearchFilter
{
    public static class BarsSearchFilterExtensions
    {
        public static IQueryable<Bar> FilterByName(this IQueryable<Bar> list, string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return list;
            }
            var newList = list.Where(b => b.Name.ToLower().Contains(key.ToLower()));
            return newList;
        }
        public static IQueryable<Bar> FilterByRating(this IQueryable<Bar> list, int? minRating)
        { 
            if(minRating == null)
            {
                return list;
            }
            //TODO D: Test is this working properly when revews are added
            var newList = list.Where(b => b.BarReviews.Any(br => br.Rating !=null) && b.BarReviews.Where(br=> br !=null).Average(r => r.Rating) >= minRating);
            return newList;
        }
        public static IQueryable<Bar> FilterByCity(this IQueryable<Bar> list, int? cityId)
        {
            if(cityId == null)
            {
                return list;
            }
            var newList = list.Where(b => b.Address.CityId == cityId);
            return newList;
        }
    }
}
