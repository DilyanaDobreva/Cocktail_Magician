using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Data;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
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
            List<string> rudeWords = new List<string>{ "ass", "arse", "asshole", "bastard", "bitch", "bollocks", "child-fucker", "Christ on a bike", "Christ on a cracker", "crap", "cunt", "damn", "frigger", "fuck", "goddamn", "godsdamn", "hell", "holy shit", "horseshit", "jesus christ", "jesus fuck", "jesus h. christ", "jesus harold christ", "jesus wept", "Jesus, Mary and Joseph", "Judas Priest", "motherfucker", "nigga", "nigger", "prick", "shit", "shit ass", "shitass", "slut", "son of a bitch", "son of a motherless goat", "son of a whore", "sweet Jesus" };

            var userId = await context.Users.Where(u => u.UserName == userName).Select(u => u.Id).FirstAsync();
            var bar = await context.Bars.Where(b => b.Id == barId).Select(b => b.Id).FirstAsync();

            var newComment = comment.Split(' ');

            newComment = newComment
                .Select(r => rudeWords
                    .Contains(r.ToLower()) ? r = "I'm happy" : r)
                .ToArray();

            comment = string.Join(' ', newComment);
            var review = barReviewFactory.Create(comment, rating, userId, bar);

            context.BarReviews.Add(review);
            await context.SaveChangesAsync();
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
