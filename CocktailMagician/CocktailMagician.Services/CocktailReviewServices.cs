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
        private readonly CocktailMagicianDb context;

        public CocktailReviewServices(ICocktailReviewFactory reviewCocktailFactory, CocktailMagicianDb context)
        {
            this.reviewCocktailFactory = reviewCocktailFactory;
            this.context = context;
        }

        public async Task AddReviewAsync(string comment, int? rating, string userName, int cocktailId)
        {
            string[] rudeWords ={ "ass", "arse", "asshole", "bastard", "bitch", "bollocks", "child-fucker", "Christ on a bike", "Christ on a cracker", "crap", "cunt", "damn", "frigger", "fuck", "goddamn", "godsdamn", "hell", "holy shit", "horseshit", "Jesus Christ", "Jesus fuck", "Jesus H. Christ", "Jesus Harold Christ", "Jesus wept", "Jesus, Mary and Joseph", "Judas Priest", "motherfucker", "nigga", "nigger", "prick", "shit", "shit ass", "shitass", "slut", "son of a bitch", "son of a motherless goat", "son of a whore", "sweet Jesus" };

            var userId = await context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();
            var cocktailIdFound = await context.Cocktails.Where(c => c.Id == cocktailId).Select(c => c.Id).FirstAsync();
            //var cocktail = await context.Cocktails
            //    .Include(c => c.CocktailIngredients)
            //        .ThenInclude(i => i.Ingredient)
            //    .Include(c => c.BarCocktails)
            //        .ThenInclude(b => b.Bar)
            //            .ThenInclude(b => b.Address)
            //                .ThenInclude(a => a.City)
            //    .FirstOrDefaultAsync(c => c.Id == cocktailId && c.IsDeleted == false);

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
            var review = reviewCocktailFactory.Create(comment, rating, userId, cocktailIdFound);

            context.CocktailReviews.Add(review);
            await context.SaveChangesAsync();
        }
        public async Task<List<CocktailReviewDTO>> AllReviewsAsync(int cocktailId)
        {

            var allReviews = await context.CocktailReviews
                .Include(c => c.Cocktail)
                .Include(u => u.User)
                .Where(r => r.CocktailId == cocktailId)
                .Select(r => new CocktailReviewDTO
                {
                    Comment = r.Comment,
                    Rating = r.Rating,
                    UserName = r.User.UserName,
                    IsDeleted = r.IsDeleted
                })
                .ToListAsync();
            return allReviews;
        }

    }
}
