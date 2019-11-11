using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    public static class BarSeed
    {
        public static readonly List<Bar> bars = new List<Bar>
        {
            new Bar
            {
                Id = 1,
                Name = "Motto",
                AddressId = 1,
                ImageUrl = "http://mysofiaapartments.com/wp-content/uploads/2015/12/Motto.jpg",
                PhoneNumber = "+35929872723",
            },
            new Bar
            {
                Id = 2,
                Name = "French 75",
                AddressId = 2,
                ImageUrl = "https://vijmag.bg/service/image?wEckYaFmQsLCeWsoS5ZNo40cnQ8JsnuTGOIWPRSIWSM_",
                PhoneNumber = "+359887044557"
            },
            new Bar
            {
                Id = 3,
                Name = "One More Bar",
                AddressId = 3,
                ImageUrl = "http://mysofiaapartments.com/wp-content/uploads/2015/11/One-more-bar.jpg",
                PhoneNumber = "+359877693735"
            },
            new Bar
            {
                Id = 4,
                Name = "Rakia Raketa Bar",
                AddressId = 4,
                ImageUrl = "http://funkt.eu/wp-content/uploads/2012/12/PAKETA-04.jpg",
                PhoneNumber = "+35924446111"
            },
            new Bar
            {
                Id = 5,
                Name = "Moda Bar My Place",
                AddressId = 5,
                ImageUrl = "https://d32swnnyen7sbd.cloudfront.net/projects/0001/27/6a4e56f5a0e66ed7a10f3ca4e611de64942568d3.jpeg",
                PhoneNumber = "+359876000056"
            },
            new Bar
            {
                Id = 6,
                Name = "The Martini Bar",
                AddressId = 6,
                ImageUrl = "http://martini.bg/wp-content/uploads/2017/01/martini_food_cocktails_varna_bulgaria_interior_7.jpg",
                PhoneNumber = "+359893374437"
            },
            new Bar
            {
                Id = 7,
                Name = "Kriloto",
                AddressId = 7,
                ImageUrl = "https://lostinplovdiv.com/media/images/4691dddb3a.jpg",
                PhoneNumber = "+359879924799"
            }
        };

    }
}
