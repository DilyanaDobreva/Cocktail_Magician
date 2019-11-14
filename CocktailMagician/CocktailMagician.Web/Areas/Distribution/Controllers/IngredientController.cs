using System;
using System.Linq;
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
        private const int itemsPerPage = 9;

        public IngredientsController(IIngredientServices ingredientServices)
        {
            this.ingredientServices = ingredientServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]IngredientBasicViewModel vm)
        {
            try
            {
                var ingredient = (await ingredientServices.AddAsync(vm.Name, vm.Unit)).MapToViewModel();

                return Json(ingredient);
            }
            catch (ArgumentException ex)
            {
                TempData["Status"] = ex.Message;
                return RedirectToAction("Add", "Cocktails");
            }

        }
        public async Task<IActionResult> Index(int id = 1)
        {
            var listOfIngredients = new IngredientsListViewModel();

            listOfIngredients.Paging.Count = await ingredientServices.AllIngredientsCountAsync();
            listOfIngredients.Paging.ItemsPerPage = itemsPerPage;
            listOfIngredients.Paging.CurrentPage = id;

            listOfIngredients.Ingredients = (await ingredientServices.GetAllPagedDTOAsync(listOfIngredients.Paging.ItemsPerPage, listOfIngredients.Paging.CurrentPage))
                .Select(c => c.MapToViewModel());

            return View(listOfIngredients);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await ingredientServices.DeleteAsync(id);
                return Ok();
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

    }
}