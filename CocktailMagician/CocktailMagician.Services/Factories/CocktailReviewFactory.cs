using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts.Factories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Factories
{
    public class CocktailReviewFactory : ICocktailReviewFactory
    {
        public CocktailReview Create(string comment, int? rating, User user, Cocktail cocktail)
        {
            var review = new CocktailReview
            {
                Comment = comment,
                Rating = rating,
                User = user,
                Cocktail = cocktail

            };
            return review;
        }
    }
}
