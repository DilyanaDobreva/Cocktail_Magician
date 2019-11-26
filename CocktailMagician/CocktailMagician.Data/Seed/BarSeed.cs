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
                ImagePath = "/barImages/Motto.jpg",
                PhoneNumber = "+35929872723",
            },
            new Bar
            {
                Id = 2,
                Name = "French 75",
                AddressId = 2,
                ImagePath = "/barImages/French75.jpg",
                PhoneNumber = "+359887044557"
            },
            new Bar
            {
                Id = 3,
                Name = "One More Bar",
                AddressId = 3,
                ImagePath = "/barImages/OneMoreBar.jpg",
                PhoneNumber = "+359877693735"
            },
            new Bar
            {
                Id = 4,
                Name = "Rakia Raketa Bar",
                AddressId = 4,
                ImagePath = "/barImages/Raketa.jpg",
                PhoneNumber = "+35924446111"
            },
            new Bar
            {
                Id = 5,
                Name = "Moda Bar My Place",
                AddressId = 5,
                ImagePath = "/barImages/ModaBarMyPlace.jpeg",
                PhoneNumber = "+359876000056"
            },
            new Bar
            {
                Id = 6,
                Name = "The Martini Bar",
                AddressId = 6,
                ImagePath = "/barImages/MartiniBar.jpg",
                PhoneNumber = "+359893374437"
            },
            new Bar
            {
                Id = 7,
                Name = "Kriloto",
                AddressId = 7,
                ImagePath = "/barImages/Kriloto.jpg",
                PhoneNumber = "+359879924799"
            }
        };

    }
}
