﻿using CocktailMagician.Data.Models;
using CocktailMagician.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CocktailMagician.Services.Contracts
{
    public interface ICocktailReviewServices
    {
        Task AddReviewAsync(string comment, int? rating, string userName, int cocktailId);
        Task<List<ReviewDTO>> AllReviewsAsync(int cocktailId);
    }
}
