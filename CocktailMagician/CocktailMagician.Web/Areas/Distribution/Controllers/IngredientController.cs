using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Ingredients;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class IngredientController : Controller
    {
        private readonly IIngredientServices ingredientServices;

        public IngredientController(IIngredientServices ingredientServices)
        {
            this.ingredientServices = ingredientServices;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            var vm = new IngredientBasicViewModel();
            return PartialView("_AddIngredient", vm);
        }
        [HttpPost]
        public async Task<IActionResult> Add(IngredientBasicViewModel vm)
        {
            var ingredient = (await ingredientServices.Add(vm.Name)).MapToViewModel();

            return Json(ingredient);
        }
    }
}