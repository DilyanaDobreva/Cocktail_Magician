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
    public class CocktailReviewServices : ICocktailReviewServices
    {
        private readonly ICocktailReviewFactory reviewCocktailFactory;
        private readonly IUserServices userServices;
        private readonly CocktailMagicianDb context;

        public CocktailReviewServices(ICocktailReviewFactory reviewCocktailFactory, IUserServices userServices, CocktailMagicianDb context)
        {
            this.reviewCocktailFactory = reviewCocktailFactory;
            this.userServices = userServices;
            this.context = context;
        }

        public async Task AddReviewAsync(string comment, int? rating, string userName, int cocktailId)
        {
            string[] rudeWords ={ "ass", "arse", "asshole", "bastard", "bitch", "bollocks", "child-fucker", "Christ on a bike", "Christ on a cracker", "crap", "cunt", "damn", "frigger", "fuck", "goddamn", "godsdamn", "hell", "holy shit", "horseshit", "Jesus Christ", "Jesus fuck", "Jesus H. Christ", "Jesus Harold Christ", "Jesus wept", "Jesus, Mary and Joseph", "Judas Priest", "motherfucker", "nigga", "nigger", "prick", "shit", "shit ass", "shitass", "slut", "son of a bitch", "son of a motherless goat", "son of a whore", "sweet Jesus" };

            var user = await userServices.FindUserAsync(userName);
            var cocktail = await context.Cocktails
                .Include(c => c.CocktailIngredients)
                    .ThenInclude(i => i.Ingredient)
                .Include(c => c.BarCocktails)
                    .ThenInclude(b => b.Bar)
                        .ThenInclude(b => b.Address)
                            .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(c => c.Id == cocktailId && c.IsDeleted == false);

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
            var review = reviewCocktailFactory.Create(comment, rating, user, cocktail);

            context.CocktailReviews.Add(review);
            await context.SaveChangesAsync();
        }

        public Task<double?> GetMidRatingAsync(int cocktailId)
        {
            var allRatings = context.CocktailReviews
                .Where(c => c.Id == cocktailId)
                .Where(c => c.Rating != null).AverageAsync(r => r.Rating);

            return allRatings;
        }
        public async Task<List<CocktailReviewDTO>> AllReviewsAsync(int cocktailId)
        {

            var allReviews = await context.CocktailReviews
                .Include(c => c.Cocktail)
                .Include(u => u.User)
                .Where(r => r.Id == cocktailId)
                .ToListAsync();

            var allReviewsDTO = allReviews.Select(b => b.MapToDTO()).ToList();
            return allReviewsDTO;
        }

    }
}
