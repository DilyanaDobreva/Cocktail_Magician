using CocktailMagician.Data;
using CocktailMagician.Data.Models;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Services.Factories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Services.UnitTests.FactoriesTests.BarReviewFatoryTests
{
    [TestClass]
    public class Create_Should
    {
        [TestMethod]
        public void ReturnInstanceOfType()
        {
            var comment = "New comment";
            var rating = 2;
            var userId = "1";
            var barId = 1;

            var sut = new BarReviewFactory();
            var barReview = sut.Create(comment, rating, userId,barId);

            Assert.IsInstanceOfType(barReview, typeof(BarReview));
            Assert.AreEqual(comment, barReview.Comment);
            Assert.AreEqual(rating, barReview.Rating);
            Assert.AreEqual(userId, barReview.UserId);
            Assert.AreEqual(barId, barReview.BarId);
        }
    }
}
