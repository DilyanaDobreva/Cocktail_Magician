using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocktailMagician.Web.Areas.Cocktails.Controllers
{
    [Area("Distribution")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailServices cocktailServices;
        private readonly IIngredientServices ingredientServices;

        public CocktailsController(ICocktailServices cocktailServices, IIngredientServices ingredientServices)
        {
            this.cocktailServices = cocktailServices;
            this.ingredientServices = ingredientServices;
        }

        public async Task<IActionResult> Index()
        {
            var listOfCocktail = new CocktailsListViewModel();
            listOfCocktail.AllCocktails = (await cocktailServices.GetAllDTO())
                .Select(c => c.MapToViewModel());

            return View(listOfCocktail);
        }

        [HttpGet]
        public async Task<IActionResult> Add([FromQuery]AddCocktailViewModel cocktailVM)
        {
            //var cocktailVM = new AddCocktailViewModel();
            if (cocktailVM.CocktilIngredients == null)
            {
                var ingredients = await ingredientServices.GetAllDTO();
                cocktailVM.AllIngredients = ingredients.Select(b => new SelectListItem(b.Name, b.Name)).ToList();

                return View(cocktailVM);
            }
            //cocktailVM.IngredientsQuantity = new Dictionary<string, int>();
            foreach(var ingr in cocktailVM.CocktilIngredients)
            {
                cocktailVM.IngredientsQuantity.Add(new CocktailIngredientViewModel { Name = ingr, Value = 0 });
            }
            return View(cocktailVM);
        }
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> FinalAdd(AddCocktailViewModel cocktailVM)
        {
            var ingredientsQuantityDTO = cocktailVM.IngredientsQuantity.Select(i => i.MapToDTO()).ToList();
            await cocktailServices.Add(cocktailVM.Name, cocktailVM.ImageURL, ingredientsQuantityDTO);
            return RedirectToAction("Index");
        }
    }
}