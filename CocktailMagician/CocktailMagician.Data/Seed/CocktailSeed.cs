using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    public static class CocktailSeed
    {
        public static readonly List<Cocktail> cocktails = new List<Cocktail>
        {
            new Cocktail
            {
                Id = 1,
                Name = "Old Fashioned",
                ImagePath = "/cocktailImages/OldFashioned.jpg"
            },
            new Cocktail
            {
                Id = 2,
                Name = "Margarita",
                ImagePath = "/cocktailImages/Margarita.jpg"
            },
            new Cocktail
            {
                Id = 3,
                Name = "Cosmopolitan",
                ImagePath = "/cocktailImages/Cosmopolitan.jpg"
            },
            new Cocktail
            {
                Id = 4,
                Name = "Negroni",
                ImagePath = "/cocktailImages/Negroni.jpg"
            },
            new Cocktail
            {
                Id = 5,
                Name = "Moscow Mule",
                ImagePath = "/cocktailImages/MoscowMule.jpg"
            },
            new Cocktail
            {
                Id = 6,
                Name = "Mojito",
                ImagePath = "/cocktailImages/Mojito.jpg"
            },
            new Cocktail
            {
                Id = 7,
                Name = "Whiskey Sour",
                ImagePath = "/cocktailImages/WhiskeySour.jpg"
            },
            new Cocktail
            {
                Id = 8,
                Name = "Manhattan",
                ImagePath = "/cocktailImages/Manhattan.jpg"
            },
            new Cocktail
            {
                Id = 9,
                Name = "Mimosa",
                ImagePath = "/cocktailImages/Mimosa.jpg"
            },
            new Cocktail
            {
                Id = 10,
                Name = "Gimlet",
                ImagePath = "/cocktailImages/Gimlet.jpg"
            }
        };
    }
}
