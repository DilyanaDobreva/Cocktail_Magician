using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class CocktailReviewServices : ICocktailReviewServices
    {
        private readonly ICocktailReviewFactory reviewCocktailFactory;
        private readonly CocktailMagicianDb context;

        public CocktailReviewServices(ICocktailReviewFactory reviewCocktailFactory, CocktailMagicianDb context)
        {
            this.reviewCocktailFactory = reviewCocktailFactory;
            this.context = context;
        }

        public async Task AddReviewAsync(string comment, int? rating, User member, int cocktailId)
        {
            var cocktail = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                    .ThenInclude(i => i.Ingredient)
                .Include(c => c.BarCocktails)
                    .ThenInclude(b => b.Bar)
                        .ThenInclude(b => b.Address)
                            .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(c => c.Id == cocktailId && c.IsDeleted == false);

            var review = reviewCocktailFactory.Create(comment, rating, member, cocktail);

            context.CocktailReviews.Add(review);
            await context.SaveChangesAsync();
        }

        public async Task<List<CocktailReviewDTO>> AllReviewsAsync(int cocktailId)
        {
            var allReviews = await context.CocktailReviews
                .Include(u => u.User)
                .Include(c => c.Cocktail)
                .Where(c => c.Id == cocktailId)
                .ToListAsync();

            var allReviewsDTO = allReviews.Select(b => b.MapToDTO()).ToList();
            return allReviewsDTO;
        }

        public Task<double?> GetMidRatingAsync(int cocktailId)
        {
            var allRatings = context.CocktailReviews
                .Where(c => c.Id == cocktailId)
                .Where(c => c.Rating != null).Select(r => r.Rating);
            var avgRating = allRatings.AverageAsync();

            return avgRating;
        }

        public async Task<CocktailReviewDTO> GetPoorestReviewAsync(int cocktailId)
        {
            var numberOfReviews = await context.CocktailReviews.Where(c => c.Id == cocktailId).CountAsync();

            if (numberOfReviews > 1)
            {
                var minRating = await context.CocktailReviews.MinAsync(r => r.Rating);
                var poorestReview = await context.CocktailReviews
                    .Include(u => u.User)
                    .LastOrDefaultAsync(u => u.Rating == minRating);

                return poorestReview.MapToDTO();
            }
            return null;
        }

        public async Task<CocktailReviewDTO> GetTopReviewAsync(int cocktailId)
        {
            var numberOfReviews = await context.CocktailReviews.Where(c => c.Id == cocktailId).CountAsync();

            if (numberOfReviews > 1)
            {
                var maxRating = await context.CocktailReviews.MaxAsync(r => r.Rating);
                var topReview = await context.CocktailReviews
                    .Include(u => u.User)
                    .FirstOrDefaultAsync(r => r.Rating == maxRating);

                return topReview.MapToDTO();
            }
            else if (numberOfReviews == 1)
            {
                var topReview = await context.CocktailReviews.Include(u => u.User).FirstAsync(c => c.Id == cocktailId);
                return topReview.MapToDTO();
            }
            return null;
        }

        public Task<int> NumberOfRatingsAsync(int cocktailId)
        {
            return context.CocktailReviews.Where(c => c.CocktailId == cocktailId).CountAsync();
        }
    }
}
