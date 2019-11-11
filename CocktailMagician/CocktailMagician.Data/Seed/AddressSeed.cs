using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    public static class AddressSeed
    {
        public static readonly List<Address> addresses = new List<Address>
        {
            new Address
            {
                Id = 1,
                Name = "18 str. Aksakov",
                CityId = 1,
                Latitude = 42.693881,
                Longitude = 23.330115,
            },
            new Address
            {
                Id = 2,
                Name= "28 str. Serdika",
                CityId = 1,
                Latitude = 42.701141,
                Longitude = 23.324477,
            },
            new Address
            {
                Id = 3,
                Name = "12 str. Tsar Shishman",
                CityId = 1,
                Latitude = 42.692791,
                Longitude = 23.330869
            },
            new Address
            {
                Id = 4,
                Name = "17 blvd. Yanko Sakazov",
                CityId = 1,
                Latitude = 42.698544,
                Longitude = 23.341951
            },
            new Address
            {
                Id = 5,
                Name = "201 blvd. Slivnitsa",
                CityId = 2,
                Latitude = 43.226818,
                Longitude = 27.886537
            },
            new Address
            {
                Id = 6,
                Name = "1 str. Batcho Kiro",
                CityId = 2,
                Latitude = 43.203813,
                Longitude = 27.908376
            },
            new Address
            {
                Id = 7,
                Name = "12 str. Knyaginya Maria Luiza",
                CityId = 3,
                Latitude = 42.144929,
                Longitude = 24.755650
            }
        };
    }
}
