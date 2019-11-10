using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CocktailMagician.Services.Mapper
{
    public static class CocktailReviewDTOMapperExtensions
    {
        public static CocktailReviewDTO MapToDTO(this CocktailReview review)
        {
            var dto = new CocktailReviewDTO
            {
                Comment = review.Comment,
                Rating = review.Rating,
                UserName = review.User.UserName,
                IsDeleted = review.IsDeleted
            };

            return dto;
        }
    }
}
