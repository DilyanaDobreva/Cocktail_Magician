using CocktailMagician.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface ICocktailReviewFactory
    {
        CocktailReview Create(string comment,int? rating, User user, Cocktail cocktail);
    }
}
