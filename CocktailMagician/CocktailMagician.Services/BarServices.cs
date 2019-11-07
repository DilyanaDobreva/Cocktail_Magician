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
        private readonly IBarCocktailFactory barCocktailFactory;

        public BarServices(CocktailMagicianDb context, IBarFactory barFactory, IBarCocktailFactory barCocktailFactory)
        {
            this.context = context;
            this.barFactory = barFactory;
            this.barCocktailFactory = barCocktailFactory;
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
                .Where(b => b.IsDeleted == false && b.BarCocktails.Any(c => c.CocktailId == cocktailId && c.IsDeleted == false))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;

        }

        public async Task<BarDetailsDTO> GetDetailedDTO(int id)
        {
            if (id == 0)
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
        public async Task<BarToEditDTO> GetBarToEditDTO(int id)
        {
            var barToEdit = await context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.City)
                .Where(b => b.Id == id && b.IsDeleted == false)
                .FirstOrDefaultAsync();

            var barToEditDTO = barToEdit.MapToEditDTO();
            return barToEditDTO;
        }
        public Task<string> GetName(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var name = context.Bars
                .Where(b => b.Id == id && b.IsDeleted == false)
                .Select(b => b.Name)
                .FirstOrDefaultAsync();
            return name;
        }
        public async Task<List<CocktailBasicDTO>> GetPresentCocktails(int id)
        {
            var presentCocktails = await context.BarCocktails
                .Include(c => c.Cocktail)
                .Where(b => b.BarId == id && b.IsDeleted == false)
                .Select(b => new CocktailBasicDTO
                {
                    Id = b.CocktailId,
                    Name = b.Cocktail.Name
                })
                .ToListAsync();

            return presentCocktails;
        }

        public async Task<List<CocktailBasicDTO>> NotPresentCocktails(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var cocktails = await context.Cocktails
                .Include(c => c.BarCocktails)
                .Where(b => b.IsDeleted == false && !b.BarCocktails.Any(bc => bc.BarId == id && bc.IsDeleted == false))
                .Select(c => new CocktailBasicDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return cocktails;
        }
        public async Task EditCocktails(EditCocktailsDTO editCocktails)
        {
            if (editCocktails.BarId == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            foreach (var cocktailId in editCocktails.CocktailsToAdd)
            {
                var barCocktail = await context.BarCocktails.FirstOrDefaultAsync(b => b.BarId == editCocktails.BarId && b.CocktailId == cocktailId);
                if (barCocktail != null)
                {
                    barCocktail.IsDeleted = false;
                }
                else
                {
                    barCocktail = barCocktailFactory.Create(editCocktails.BarId, cocktailId);
                    context.BarCocktails.Add(barCocktail);
                }
            }
            foreach (var cocktailId in editCocktails.CocktailsToRemove)
            {
                var barCocktail = await context.BarCocktails
                    .FirstOrDefaultAsync(b => b.BarId == editCocktails.BarId && b.CocktailId == cocktailId && b.IsDeleted == false);

                if (barCocktail != null)
                {
                    barCocktail.IsDeleted = true;
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task Edit(BarToEditDTO newBarInfo)
        {
            var bar = await context.Bars
                .Include(b => b.Address)
                .Where(b => b.Id == newBarInfo.Id && b.IsDeleted == false)
                .FirstOrDefaultAsync();

            bar.Name = newBarInfo.Name;
            bar.ImageUrl = newBarInfo.ImageURL;
            bar.Address.Name = newBarInfo.Address.Name;
            bar.Address.Latitude = newBarInfo.Address.Latitude;
            bar.Address.Longitude = newBarInfo.Address.Longitude;
            bar.Address.CityId = newBarInfo.Address.CityId;

            await context.SaveChangesAsync();
        }
    }
}
