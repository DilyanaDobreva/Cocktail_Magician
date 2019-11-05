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
        private readonly IBarServices barServices;

        public CocktailsController(ICocktailServices cocktailServices, IIngredientServices ingredientServices, IBarServices barServices)
        {
            this.cocktailServices = cocktailServices;
            this.ingredientServices = ingredientServices;
            this.barServices = barServices;
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
            if (ModelState.IsValid)
            {
                var ingredientsQuantityDTO = cocktailVM.IngredientsQuantity.Select(i => i.MapToDTO()).ToList();
                await cocktailServices.Add(cocktailVM.Name, cocktailVM.ImageURL, ingredientsQuantityDTO);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something went wrong...");
            return View(cocktailVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var cocktail = (await cocktailServices.GetDTO(id));
            var cocktailVM = cocktail.MapToViewModel();

            return View(cocktailVM);
        }
        public async Task<IActionResult> AddBars(int id)
        {
            var vm = new AddBarsViewModel();
            vm.CocktailName = await cocktailServices.GetName(id);
            vm.AllBars = (await barServices.GetAllNotIncludedDTO(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> AddBars(int id, AddBarsViewModel vm)
        {
            await cocktailServices.AddBarsAsync(id, vm.SelectedBars);
            return RedirectToAction("Details", new { id = id });
        }
        public async Task<IActionResult> RemoveBars(int id)
        {
            var vm = new RemoveBarsViewModel();
            vm.CocktailName = await cocktailServices.GetName(id);
            vm.BarsOfCocktail = (await barServices.GetBarsOfCocktail(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveBars(int id, RemoveBarsViewModel vm)
        {
            await cocktailServices.RemoveBarsAsync(id, vm.BarsToRemove);
            return RedirectToAction("Details", new { id = id });
        }
    }
}