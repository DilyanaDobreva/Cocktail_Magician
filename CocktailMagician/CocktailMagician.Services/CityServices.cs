using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class CityServices : ICityServices
    {
        private readonly CocktailMagicianDb context;
        private readonly ICityFactory cityFactory;

        public CityServices(CocktailMagicianDb context, ICityFactory cityFactory)
        {
            this.context = context;
            this.cityFactory = cityFactory;
        }
        public async Task<CityDTO> Add(string name)
        {
            var doesCityExist = await context.Cities.AnyAsync(c => c.Name == name);
            if (doesCityExist)
                throw new ArgumentException(OutputConstants.CityAlreadyExists);

            var city = cityFactory.Create(name);
            context.Cities.Add(city);
            await context.SaveChangesAsync();

            var cityDTO = (await context.Cities.FirstAsync(c => c.Name == name)).MapToDTO();
            return cityDTO;
        }

        public Task<City> Get(int id)
        {
            var city = context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            return city;
        }

        public async Task<List<CityDTO>> GetAllDTO()
        {
            var allCities = (await context.Cities.Where(c => c.IsDeleted == false).ToListAsync()).Select(c => c.MapToDTO()).ToList();
            return allCities;
        }
    }
}
