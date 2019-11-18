using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Mapper;
using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Services
{
    public class BarReviewServices : IBarReviewServices
    {
        private readonly IBarReviewFactory barReviewFactory;
        private readonly CocktailMagicianDb context;

        public BarReviewServices(IBarReviewFactory barReviewFactory, CocktailMagicianDb context)
        {
            this.barReviewFactory = barReviewFactory;
            this.context = context;
        }

        public async Task AddReviewAsync(string comment, int? rating, string userName, int barId)
        {
            string[] rudeWords ={ "ass" };

            var userId = await context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();
            var bar = await context.Bars.Where(b => b.Id == barId).Select(b => b.Id).FirstAsync();

            //TODO K better way for replace.
            var newComment = comment.Split(' ');

            for (int i = 0; i < newComment.Length; i++)
            {
                for (int j = 0; j < rudeWords.Length; j++)
                {
                    if (newComment[i].Contains(rudeWords[j],StringComparison.OrdinalIgnoreCase))
                    {
                        newComment[i] = "I'm happy";
                    }
                }
            }
            comment = string.Join(' ', newComment);
            var review = barReviewFactory.Create(comment, rating, userId, bar);

            context.BarReviews.Add(review);
            await context.SaveChangesAsync();
        }

        public Task<double?> GetMidRatingAsync(int cocktailId)
        {
            var allRatings = context.CocktailReviews
                .Where(c => c.Id == cocktailId)
                .Where(c => c.Rating != null).Select(r => r.Rating);
            var avgRating = allRatings.AverageAsync();

            return avgRating;
        }
        public async Task<List<BarReviewDTO>> AllReviewsAsync(int barId)
        {

            var allReviews = await context.BarReviews
                .Include(c => c.Bar)
                .Include(u => u.User)
                .Where(b => b.BarId == barId)
                .Select(b => new BarReviewDTO
                {
                    Comment = b.Comment,
                    Rating = b.Rating,
                    UserName = b.User.UserName,
                    IsDeleted = b.IsDeleted
                })
                .ToListAsync();
            return allReviews;
        }
    }
}
