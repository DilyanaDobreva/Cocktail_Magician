using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Factories
{
    public class CocktailReviewFactory : ICocktailReviewFactory
    {
        public CocktailReview Create(string comment, int? rating, string userId, int cocktailId)
        {
            var review = new CocktailReview
            {
                Comment = comment,
                Rating = rating,
                UserId = userId,
                CocktailId = cocktailId

            };
            return review;
        }
    }
}
