using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Factories
{
    public class BarReviewFactory : IBarReviewFactory
    {

        public BarReview Create(string comment, int? rating, User user, Bar bar)
        {
            var review = new BarReview
            {
                Comment = comment,
                Rating = rating,
                User = user,
                Bar = bar

            };
            return review;
        }
    }
}
