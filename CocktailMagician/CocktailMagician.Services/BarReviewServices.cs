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
        private readonly IUserServices userServices;
        private readonly CocktailMagicianDb context;

        public BarReviewServices(IBarReviewFactory barReviewFactory, IUserServices userServices, CocktailMagicianDb context)
        {
            this.barReviewFactory = barReviewFactory;
            this.userServices = userServices;
            this.context = context;
        }

        public async Task AddReviewAsync(string comment, int? rating, string userName, int barId)
        {
            string[] rudeWords ={ "ass", "arse", "asshole", "bastard", "bitch", "bollocks", "child-fucker", "Christ on a bike", "Christ on a cracker", "crap", "cunt", "damn", "frigger", "fuck", "goddamn", "godsdamn", "hell", "holy shit", "horseshit", "Jesus Christ", "Jesus fuck", "Jesus H. Christ", "Jesus Harold Christ", "Jesus wept", "Jesus, Mary and Joseph", "Judas Priest", "motherfucker", "nigga", "nigger", "prick", "shit", "shit ass", "shitass", "slut", "son of a bitch", "son of a motherless goat", "son of a whore", "sweet Jesus" };

            var user = await userServices.FindUserAsync(userName);
            var bar = await context.Bars
                .FirstOrDefaultAsync(c => c.Id == barId && c.IsDeleted == false);

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
            var review = barReviewFactory.Create(comment, rating, user, bar);

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

    }
}
