using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;

namespace CocktailMagician.Web.Areas.Cocktails.Controllers
{
    [Area("Distribution")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailReviewServices cocktailReview;
        private readonly ICocktailServices cocktailServices;
        private readonly IIngredientServices ingredientServices;

        private const int itemsPerPage = 4;

        public CocktailsController(ICocktailReviewServices cocktailReview, ICocktailServices cocktailServices, IIngredientServices ingredientServices)
        {
            this.cocktailReview = cocktailReview;
            this.cocktailServices = cocktailServices;
            this.ingredientServices = ingredientServices;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            var listOfCocktail = new CocktailsListViewModel();
            
            listOfCocktail.Paging.Count = await cocktailServices.AllCocktailsCountAsync();
            listOfCocktail.Paging.ItemsPerPage = itemsPerPage;
            listOfCocktail.Paging.CurrentPage = id;

            listOfCocktail.AllCocktails = (await cocktailServices.GetAllDTOAsync(listOfCocktail.Paging.ItemsPerPage, listOfCocktail.Paging.CurrentPage))
                .Select(c => c.MapToViewModel());


            return View(listOfCocktail);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add([FromQuery]AddCocktailViewModel cocktailVM)
        {

            if (cocktailVM.CocktilIngredients == null)
            {
                var ingredients = await ingredientServices.GetAllDTOAsync();
                cocktailVM.AllIngredients = ingredients.Select(b => new SelectListItem(b.Name, b.Name)).ToList();

                return View(cocktailVM);
            }
            if(await cocktailServices.DoesNameExist(cocktailVM.Name))
            {
                cocktailVM.Name = null;
                cocktailVM.CocktilIngredients = null;
                TempData["Status"] = "Cocktail with such name alredy exists.";
                return View(cocktailVM);
            }
            foreach (var ingr in cocktailVM.CocktilIngredients)
            {

                cocktailVM.IngredientsQuantity.Add(new CocktailIngredientViewModel { Name = ingr, Value = 0 });
            }
            return View(cocktailVM);
        }
        [HttpPost, ActionName("Add")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalAdd(AddCocktailViewModel cocktailVM)
        {
            if (ModelState.IsValid)
            {
                var ingredientsQuantityDTO = cocktailVM.IngredientsQuantity.Select(i => i.MapToDTO()).ToList();
                await cocktailServices.AddAsync(cocktailVM.Name, cocktailVM.ImageURL, ingredientsQuantityDTO);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something went wrong...");
            return View(cocktailVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var listWithReviews = await cocktailReview.AllReviewsAsync(id);
            var cocktail = (await cocktailServices.GetDTOAsync(id));
            var cocktailVM = cocktail.MapToViewModel();

            cocktailVM.CocktailReviews = listWithReviews.Select(r => r.MapToViewModel());

            return View(cocktailVM);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditBars(int id)
        {
            try
            {
            var vm = new EditBarsViewModel();
            vm.CocktailName = await cocktailServices.GetNameAsync(id);
            vm.AllOtherBars = (await cocktailServices.GetAllNotIncludedBarsDTOAsync(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
            vm.BarsOfCocktail = (await cocktailServices.GetBarsOfCocktailAsync(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
            return View(vm);
            }
            catch(InvalidOperationException)
            {
                return BadRequest();
            }

        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditBars(int id, EditBarsViewModel vm)
        {
            try
            {
                await cocktailServices.AddBarsAsync(id, vm.BarsToAdd);
                await cocktailServices.RemoveBarsAsync(id, vm.BarsToRemove);

                return RedirectToAction("Details", new { id = id });
            }
            catch(InvalidCastException)
            {
                return BadRequest();
            }
        }

        //[Authorize(Roles = "admin")]
        //public async Task<IActionResult> RemoveBars(int id)
        //{
        //    var vm = new RemoveBarsViewModel();
        //    vm.CocktailName = await cocktailServices.GetName(id);
        //    vm.BarsOfCocktail = (await barServices.GetBarsOfCocktail(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();

        //    return View(vm);
        //}

        //[HttpPost]
        //[Authorize(Roles = "admin")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> RemoveBars(int id, RemoveBarsViewModel vm)
        //{
        //    return RedirectToAction("Details", new { id = id });
        //}

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditIngredients(int id, [FromQuery]EditCocktailViewModel cocktailToEdit)
        {

            if (cocktailToEdit.CocktilNewIngredients.Count() == 0 && cocktailToEdit.IngredientsToRemove.Count() == 0)
            {
                cocktailToEdit = (await cocktailServices.GetDTOAsync(id)).MapToEditViewModel();

                cocktailToEdit.ListCurrentIngredients = cocktailToEdit.IngredientsQuantity
                    .Select(i => i.Value + i.Unit + " " + i.Name)
                    .ToList();

                var ingredients = await ingredientServices.GetAllNotIncludedDTOAsync(id);

                cocktailToEdit.AllNotIncludedIngredients = ingredients
                    .Select(b => new SelectListItem(b.Name, b.Name)).ToList();

                cocktailToEdit.CurrentCocktailIngredients = cocktailToEdit.IngredientsQuantity
                    .Select(i => new SelectListItem(i.Name, i.Name)).ToList();

                return View(cocktailToEdit);
            }
            foreach (var ingr in cocktailToEdit.CocktilNewIngredients)
            {
                cocktailToEdit.IngredientsQuantity.Add(new CocktailIngredientViewModel { Name = ingr, Value = 0 });
            }

            if (cocktailToEdit.IngredientsToRemove != null)
            {
                foreach (var ingr in cocktailToEdit.IngredientsToRemove)
                {
                    var iq = cocktailToEdit.IngredientsQuantity.FirstOrDefault(c => c.Name == ingr);
                    cocktailToEdit.IngredientsQuantity.Remove(iq);
                }
            }

            return View(cocktailToEdit);

        }

        [HttpPost, ActionName("EditIngredients")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditQuanities(int id, EditCocktailViewModel cocktailToEdit)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var ingredientsQuantityDTO = cocktailToEdit.IngredientsQuantity.Select(i => i.MapToDTO()).ToList();
                    await cocktailServices.EditIngredientsAsync(id, ingredientsQuantityDTO, cocktailToEdit.IngredientsToRemove);
                    return RedirectToAction("Details", new { id = cocktailToEdit.Id });
                }
                catch(InvalidOperationException)
                {
                    return BadRequest();
                }
            }
            ModelState.AddModelError("", "Something went wrong...");
            return View(cocktailToEdit);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await cocktailServices.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Search([FromQuery] CocktailSearchViewModel vm)
        {
            var allIngredients = (await ingredientServices.GetAllDTOAsync());
            vm.AllIngredients = new List<SelectListItem>();
            vm.AllIngredients.Add(new SelectListItem("Select...", ""));

            allIngredients.ForEach(c => vm.AllIngredients.Add(new SelectListItem(c.Name, c.Id.ToString())));

            if (string.IsNullOrEmpty(vm.NameKey) && vm.MinRating == null && vm.IngredientId == null)
            {
                return View(vm);
            }

            vm.Result = (await cocktailServices.SearchAsync(vm.NameKey, vm.IngredientId, vm.MinRating))
                .Select(b => b.MapToViewModel());

            return View(vm);

        }
        public async Task<IActionResult> CocktailReview(int id)
        {
            var cocktail = await cocktailServices.GetDTOAsync(id);
            var cocktailVM = cocktail.MapToViewModel();

            var vm = new CocktailReviewViewModel
            {
                Cocktail = cocktailVM
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CocktailReview(int id, CocktailReviewViewModel vm)
        {
            if (vm.Rating != null || !string.IsNullOrWhiteSpace(vm.Comment))
            {
                var memberName = User.Identity.Name;
                await cocktailReview.AddReviewAsync(vm.Comment, vm.Rating, memberName, id);
            }

            return RedirectToAction("Details", "Cocktails", new {id = id });
        }

        //public async Task<IActionResult> ShowReviews(int id)
        //{
        //    var list = await cocktailReview.AllReviewsAsync(id);
        //    var cocktail = (await cocktailServices.GetDTO(id));
        //    var vm = new CocktailDetailsViewModel()
        //    {
        //        CocktailReviews = list.Select(r => r.MapToViewModel()),
        //        Name = cocktail.Name,
        //        Id = id
        //    };

        //    return View(vm);
        //}
    }
}