﻿using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class AddressServices : IAddressServices
    {
        private readonly CocktailMagicianDb context;
        private readonly ICityFactory cityFactory;
        private readonly IAddressFactory addressFactory;

        public AddressServices(CocktailMagicianDb context, ICityFactory cityFactory, IAddressFactory addressFactory)
        {
            this.context = context;
            this.cityFactory = cityFactory;
            this.addressFactory = addressFactory;
        }
        public async Task AddCity(string name)
        {
            var doesCityExist = await context.Cities.AnyAsync(c => c.Name == name);

            if (!doesCityExist)
            {
                var city = cityFactory.Create(name);
                context.Cities.Add(city);
                await context.SaveChangesAsync();
            }
        }

        public Task<City> GetCity(int id)
        {
            var city = context.Cities.FirstOrDefaultAsync(c => c.Id == id);
            return city;
        }

        public async Task<List<CityDTO>> GetAllCities()
        {
            var allCities = (await context.Cities.Where(c => c.IsDeleted == false).ToListAsync()).Select(c => c.MapToDTO()).ToList();
            return allCities;
        }
    }
}
