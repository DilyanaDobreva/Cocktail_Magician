using CocktailMagician.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Contracts.Factories
{
    public interface IBarReviewFactory
    {
        BarReview Create(string comment, int? rating, User user, Bar bar);
    }
}
