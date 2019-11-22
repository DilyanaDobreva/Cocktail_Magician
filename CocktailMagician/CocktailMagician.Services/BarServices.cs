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

        public async Task AddAsync(string name, string imageURL, string phoneNumber, AddressDTO address)
        {
            if (context.Bars.Any(c => c.Name == name && c.IsDeleted == false))
                throw new ArgumentException(OutputConstants.BarExists);

            var bar = barFactory.Create(name, imageURL, phoneNumber, address);

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
        public async Task<BarDetailsDTO> GetDetailedDTOAsync(int id)
        {
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var barDTO = await context.Bars
                .Include(b => b.BarCocktails)
                    .ThenInclude(bc => bc.Cocktail)
                .Include(b => b.Address)
                    .ThenInclude(b => b.City)
                .Where(b => b.Id == id && b.IsDeleted == false)
                .Select(bar => new BarDetailsDTO
                {
                    Id = bar.Id,
                    Name = bar.Name,
                    ImageURL = bar.ImageUrl,
                    PhoneNumber = bar.PhoneNumber,
                    AverageRating = bar.BarReviews
                        .Where(r => r.Rating != null)
                        .Select(r => r.Rating)
                        .Average(),
                    Address = bar.Address.MapToDTO(),
                    Cocktails = bar.BarCocktails.Select(bc => new CocktailInListDTO
                    {
                        Id = bc.Cocktail.Id,
                        Name = bc.Cocktail.Name,
                        ImageURL = bc.Cocktail.ImageUrl
                    })
                })
                .FirstAsync();

            barDTO.Address.CityName = await context.Cities
                .Where(c => c.Id == barDTO.Address.CityId)
                .Select(c => c.Name)
                .FirstAsync();

            return barDTO;
        }
        public async Task<BarToEditDTO> GetBarToEditDTOAsync(int id)
        {
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var barToEditDTO = await context.Bars
                .Include(b => b.Address)
                .ThenInclude(a => a.City)
                .Where(b => b.Id == id && b.IsDeleted == false)
                .Select(bar => new BarToEditDTO
                {
                    Name = bar.Name,
                    ImageURL = bar.ImageUrl,
                    Id = bar.Id,
                    Address = bar.Address.MapToDTO()
                })
                .FirstOrDefaultAsync();

            return barToEditDTO;
        }
        public async Task<string> GetNameAsync(int id)
        {
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var name = context.Bars
                .Where(b => b.Id == id && b.IsDeleted == false)
                .Select(b => b.Name)
                .FirstOrDefaultAsync();

            return await name;
        }
        public async Task<List<CocktailBasicDTO>> GetPresentCocktailsAsync(int id)
        {
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

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
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
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
            if (!await context.Bars.AnyAsync(b => b.Id == barId && b.IsDeleted == false))
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
            if (!await context.Bars.AnyAsync(b => b.Id == barId && b.IsDeleted == false))
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
            if (!await context.Bars.AnyAsync(b => b.Id == newBarInfo.Id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

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
            if (!await context.Bars.AnyAsync(b => b.Id == id && b.IsDeleted == false))
            {
                throw new InvalidOperationException(OutputConstants.InvalidId);
            }

            var bar = await context.Bars
                .Include(b => b.BarCocktails)
                .FirstAsync(b => b.Id == id && b.IsDeleted == false);

            context.BarCocktails.RemoveRange(bar.BarCocktails);

            bar.IsDeleted = true;
            await context.SaveChangesAsync();
        }
        public async Task<List<BarInListDTO>> SearchAsync(BarSearchDTO dto, int itemsPerPage, int currentPage)
        {
            var resultDTO = await context.Bars
                .Include(b => b.Address)
                    .ThenInclude(a => a.City)
                .Include(b => b.BarReviews)
                .Include(b => b.BarReviews)
                .FilterByName(dto.NameKey)
                .FilterByCity(dto.CityId)
                .FilterByRating(dto.MinRating)
                .Select(bar => new BarInListDTO
                {
                    Id = bar.Id,
                    Name = bar.Name,
                    ImageURL = bar.ImageUrl,
                    Address = bar.Address.Name,
                    City = bar.Address.City.Name,
                    AverageRating = bar.BarReviews
                        .Where(r => r.Rating != null)
                        .Select(r => r.Rating)
                        .Average()
                })
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .ToListAsync();

            return resultDTO;
        }
        public async Task<int> SerchResultCountAsync(BarSearchDTO dto)
        {
            var resultCount = await context.Bars
                .Include(b => b.Address)
                    .ThenInclude(a => a.City)
                .Include(b => b.BarReviews)
                .Include(b => b.BarReviews)
                .FilterByName(dto.NameKey)
                .FilterByCity(dto.CityId)
                .FilterByRating(dto.MinRating)
                .CountAsync();

            return resultCount;
        }

        public async Task<int> AllBarsCountAsync()
        {
            var count = await context.Bars.Where(c => c.IsDeleted == false).CountAsync();
            return count;
        }
    }
}
