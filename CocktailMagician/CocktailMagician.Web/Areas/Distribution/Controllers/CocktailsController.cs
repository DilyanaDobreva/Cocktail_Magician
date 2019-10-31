using System.Linq;
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
        public async Task<IActionResult> Add()
        {
            var cocktailVM = new AddCocktailViewModel();
            var ingredients = await ingredientServices.GetAllDTO();
            cocktailVM.AllIngredients = ingredients.Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
                                
            return View(cocktailVM);
        }
        //[HttpPost]
        //public async Task<IActionResult> Add(AddCocktailViewModel cocktailVM)
        //{

        //}

    }
}