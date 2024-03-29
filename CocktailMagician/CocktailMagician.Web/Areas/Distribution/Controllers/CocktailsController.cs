﻿using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace CocktailMagician.Web.Areas.Cocktails.Controllers
{
    [Area("Distribution")]
    public class CocktailsController : Controller
    {
        private readonly ICocktailReviewServices cocktailReview;
        private readonly ICocktailServices cocktailServices;
        private readonly IIngredientServices ingredientServices;
        private readonly IHostingEnvironment hostingEnvironment;
        private const int itemsPerPage = 4;

        public CocktailsController(ICocktailReviewServices cocktailReview, ICocktailServices cocktailServices, IIngredientServices ingredientServices, IHostingEnvironment hostingEnvironment)
        {
            this.cocktailReview = cocktailReview;
            this.cocktailServices = cocktailServices;
            this.ingredientServices = ingredientServices;
            this.hostingEnvironment = hostingEnvironment;
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
                cocktailVM.AllIngredients = ingredients.Select(b => new SelectListItem($"{b.Name}, {b.Unit}", $"{b.Name}, {b.Unit}")).ToList();

                return View(cocktailVM);
            }
            if (await cocktailServices.DoesNameExist(cocktailVM.Name))
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
                var fileName = Path.Combine(hostingEnvironment.WebRootPath + "\\cocktailImages", Path.GetFileName(cocktailVM.Image.FileName));
                cocktailVM.Image.CopyTo(new FileStream(fileName, FileMode.Create));

                cocktailVM.ImagePath = "/cocktailImages/" + Path.GetFileName(cocktailVM.Image.FileName);

                var ingredientsQuantityDTO = cocktailVM.IngredientsQuantity.Select(i => i.MapToDTO()).ToList();
                await cocktailServices.AddAsync(cocktailVM.Name, cocktailVM.ImagePath, ingredientsQuantityDTO);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something went wrong...");
            return View(cocktailVM);
        }
        public async Task<IActionResult> Details(int id)
        {
            var listWithReviews = await cocktailReview.AllReviewsAsync(id);
            var cocktail = (await cocktailServices.GetDetailedDTOAsync(id));
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
                var cocktail = await cocktailServices.GetDTOAsync(id);
                vm.Id = cocktail.Id;
                vm.CocktailName = cocktail.Name;
                vm.ImageUrl = cocktail.ImagePath;
                vm.AllOtherBars = (await cocktailServices.GetAllNotIncludedBarsDTOAsync(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
                vm.BarsOfCocktail = (await cocktailServices.GetBarsOfCocktailAsync(id)).Select(b => new SelectListItem(b.Name, b.Id.ToString())).ToList();
                return View(vm);
            }
            catch (InvalidOperationException)
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
            catch (InvalidCastException)
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
                cocktailToEdit = (await cocktailServices.GetDetailedDTOAsync(id)).MapToEditViewModel();

                cocktailToEdit.ListCurrentIngredients = cocktailToEdit.IngredientsQuantity
                    .Select(i => i.Value + i.Unit + " " + i.Name)
                    .ToList();

                var ingredients = await cocktailServices.GetAllNotIncludedIngredientsDTOAsync(id);

                cocktailToEdit.AllNotIncludedIngredients = ingredients
                    .Select(b => new SelectListItem($"{b.Name}, {b.Unit}", $"{b.Name}, {b.Unit}")).ToList();

                cocktailToEdit.CurrentCocktailIngredients = cocktailToEdit.IngredientsQuantity
                    .Select(i => new SelectListItem($"{i.Name}, {i.Unit}", i.Name)).ToList();

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
                catch (InvalidOperationException)
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
        public async Task<IActionResult> Search(int id, [FromQuery] CocktailSearchViewModel vm)
        {
            var allIngredients = (await ingredientServices.GetAllDTOAsync());
            vm.AllIngredients = new List<SelectListItem>();
            vm.AllIngredients.Add(new SelectListItem("Select...", ""));

            allIngredients.ForEach(c => vm.AllIngredients.Add(new SelectListItem(c.Name, c.Id.ToString())));

            if (string.IsNullOrEmpty(vm.NameKey) && vm.MinRating == null && vm.IngredientId == null)
            {
                return View(vm);
            }

            var searchParameters = new CocktailSearchDTO
            {
                NameKey = vm.NameKey,
                MinRating = vm.MinRating,
                IngredientId = vm.IngredientId
            };

            vm.Paging.Count = await cocktailServices.SerchResultCountAsync(searchParameters);
            vm.Paging.ItemsPerPage = itemsPerPage;
            vm.Paging.CurrentPage = id == 0 ? 1 : id;

            vm.Result = (await cocktailServices.SearchAsync(searchParameters, itemsPerPage, vm.Paging.CurrentPage))
                .Select(b => b.MapToViewModel());

            return View(vm);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CocktailReview([FromBody]ReviewViewModel vm)
        {
            if (vm.Rating != null || !string.IsNullOrWhiteSpace(vm.Comment))
            {
                vm.UserName = User.Identity.Name;
                await cocktailReview.AddReviewAsync(vm.Comment, vm.Rating, vm.UserName, vm.Id);
                return PartialView("_ReviewPartial", vm);
            }

            return RedirectToAction("Details", "Cocktails", new { id = vm.Id });
        }

    }
}