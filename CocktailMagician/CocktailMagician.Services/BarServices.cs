using System;
using CocktailMagician.Data;
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
    public class BarServices : IBarServices
    {
        private readonly CocktailMagicianDb context;
        private readonly IBarFactory barFactory;

        public BarServices(CocktailMagicianDb context, IBarFactory barFactory)
        {
            this.context = context;
            this.barFactory = barFactory;
        }

        public async Task Add(string name, string imageURL, AddressDTO address)
        {
            var barAddress = address.MapFromDTO();
            var bar = barFactory.Create(name, imageURL, barAddress);

            context.Bars.Add(bar);
            await context.SaveChangesAsync();
        }
        public async Task<List<BarInListDTO>> GetAllDTO()
        {
            var allBars = await context.Bars
                .Include(b => b.Address)
                    .ThenInclude(a => a.City)
                .Where(b => b.IsDeleted == false)
                .Select(b => new BarInListDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                    ImageURL = b.ImageUrl,
                    Address = b.Address.Name,
                    City = b.Address.City.Name
                })
                .ToListAsync();

            return allBars;
        }
        public async Task<List<BarBasicDTO>> GetAllNotIncludedDTO(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(c => c.Cocktail)
                .Where(b => b.IsDeleted == false && !(b.BarCocktails.Any(c => c.CocktailId == cocktailId && c.IsDeleted == false)))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;
        }
        public async Task<List<BarBasicDTO>> GetBarsOfCocktail(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(c => c.Cocktail)
                .Where(b => b.IsDeleted == false && b.BarCocktails.Any(c => c.CocktailId == cocktailId && c.IsDeleted==false))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;

        }

        public async Task<BarDetailsDTO> GetDTO(int id)
        {
            if(id == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var bar = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(bc => bc.Cocktail)
                .Include(b => b.Address)
                    .ThenInclude(b => b.City)
                .Where(b => b.Id == id && b.IsDeleted == false)
                //.Select(b => new BarDetailsDTO
                //{
                //    Id = b.Id,
                //    Name = b.Name,
                //    ImageURL = b.ImageUrl,
                //    Address = b.Address.MapToDTO(),
                //    Cocktails = b.BarCocktails.Select(bc => bc.Cocktail.MapToDTO())
                //})
                .FirstOrDefaultAsync();
            var barDTO = bar.MapToDetailsDTO();

            return barDTO;
        }
    }
}
