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
using CocktailMagician.Services.SearchFilter;

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

        //TODO D: Check if found elemnt is deleted and if it is not allow to send it to controller
        public async Task AddAsync(string name, string imageURL, AddressDTO address)
        {
            var bar = barFactory.Create(name, imageURL, address);

            context.Bars.Add(bar);
            await context.SaveChangesAsync();
        }
        public async Task<List<BarInListDTO>> GetAllDTOAsync(int itemsPerPage, int currentPage)
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
                    City = b.Address.City.Name,
                    AverageRating = b.BarReviews
                        .Where(r => r.Rating != null)
                        .Select(r => r.Rating)
                        .Average()
                })
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();
            return allBars;
        }
        public async Task<List<BarBasicDTO>> GetAllNotIncludedDTOAsync(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(c => c.Cocktail)
                .Where(b => b.IsDeleted == false && !(b.BarCocktails.Any(c => c.CocktailId == cocktailId)))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;
        }
        public async Task<List<BarBasicDTO>> GetBarsOfCocktailAsync(int cocktailId)
        {
            var allBars = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(c => c.Cocktail)
                .Where(b => b.IsDeleted == false && b.BarCocktails.Any(c => c.CocktailId == cocktailId))
                .Select(b => new BarBasicDTO
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToListAsync();

            return allBars;

        }
        public async Task<BarDetailsDTO> GetDetailedDTOAsync(int id)
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
        public async Task<BarToEditDTO> GetBarToEditDTOAsync(int id)
        {
            var barToEdit = await context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.City)
                .Where(b => b.Id == id && b.IsDeleted == false)
                .FirstOrDefaultAsync();

            var barToEditDTO = barToEdit.MapToEditDTO();
            return barToEditDTO;
        }
        public Task<string> GetNameAsync(int id)
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
        public async Task<List<CocktailBasicDTO>> GetPresentCocktailsAsync(int id)
        {
            var presentCocktails = await context.BarCocktails
                .Include(c => c.Cocktail)
                .Where(b => b.BarId == id)
                .Select(b => new CocktailBasicDTO
                {
                    Id = b.CocktailId,
                    Name = b.Cocktail.Name
                })
                .ToListAsync();

            return presentCocktails;
        }
        public async Task<List<CocktailBasicDTO>> NotPresentCocktailsAsync(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var cocktails = await context.Cocktails
                .Include(c => c.BarCocktails)
                .Where(b => b.IsDeleted == false && !b.BarCocktails.Any(bc => bc.BarId == id))
                .Select(c => new CocktailBasicDTO
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return cocktails;
        }
        public async Task AddCocktailsAsync(int barId, List<int> cocktailsToAdd)
        {
            if (barId == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            foreach (var cocktailId in cocktailsToAdd)
            {
                var barCocktail = await context.BarCocktails.FirstOrDefaultAsync(b => b.BarId == barId && b.CocktailId == cocktailId);

                barCocktail = barCocktailFactory.Create(barId, cocktailId);
                context.BarCocktails.Add(barCocktail);

            }
            await context.SaveChangesAsync();
        }
        public async Task RemoveCocktailsAsync(int barId, List<int> cocktailsToRemove)
        {
            if (barId == 0)
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            foreach (var cocktailId in cocktailsToRemove)
            {
                var barCocktail = await context.BarCocktails
                    .FirstOrDefaultAsync(b => b.BarId == barId && b.CocktailId == cocktailId);

                if (barCocktail != null)
                {
                    context.BarCocktails.Remove(barCocktail);
                }
            }
            await context.SaveChangesAsync();
        }
        public async Task EditAsync(BarToEditDTO newBarInfo)
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
        public async Task DeleteAsync(int id)
        {
            var bar = await context.Bars
                .Include(b => b.BarCocktails)
                .FirstAsync(b => b.Id == id && b.IsDeleted == false);

            context.BarCocktails.RemoveRange(bar.BarCocktails);

            bar.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public async Task<List<BarInListDTO>> SearchAsync(string name, int? cityId, int? minRating)
        {
            var result = await context.Bars
                .Include(b => b.Address)
                .Include(b => b.BarReviews)
                .FilterBuName(name)
                .FilterByCity(cityId)
                .FilterByRating(minRating)
                .ToListAsync();

            var resultDTO = result.Select(b => b.MapToDTO()).ToList();
            return resultDTO;
        }
        public async Task<int> AllBarsCountAsync()
        {
            var count = await context.Bars.Where(c => c.IsDeleted == false).CountAsync();
            return count;
        }
    }
}
