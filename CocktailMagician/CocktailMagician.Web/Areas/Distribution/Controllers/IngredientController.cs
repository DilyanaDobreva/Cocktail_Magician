﻿using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Ingredients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientServices ingredientServices;

        public IngredientsController(IIngredientServices ingredientServices)
        {
            this.ingredientServices = ingredientServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]IngredientBasicViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ingredient = (await ingredientServices.Add(vm.Name, vm.Unit)).MapToViewModel();

                return Json(ingredient);
            }
            TempData["Message"] = "Fileds are required!";
            return View("~/Views/Cocktails/Add.cshtml");

        }
        public async Task<IActionResult> Index()
        {
            var listOfIngredients = new IngredientsListViewModel();
            listOfIngredients.Ingredients = (await ingredientServices.GetAllDTO())
                .Select(c => c.MapToViewModel());

            return View(listOfIngredients);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await ingredientServices.Delete(id);
            return RedirectToAction("Index");
        }

    }
}