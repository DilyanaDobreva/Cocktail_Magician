using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Factories
{
    public class BarReviewFactory : IBarReviewFactory
    {

        public BarReview Create(string comment, int? rating, string userId, int barId)
        {
            var review = new BarReview
            {
                Comment = comment,
                Rating = rating,
                UserId = userId,
                BarId = barId

            };
            return review;
        }
    }
}
