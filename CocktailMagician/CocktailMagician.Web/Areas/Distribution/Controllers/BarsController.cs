using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using CocktailMagician.Web.Areas.Distribution.Models;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class BarsController : Controller
    {
        private readonly IBarReviewServices barReviewServices;
        private readonly IBarServices barServices;
        private readonly ICityServices cityServices;

        private const int itemsPerPage = 3;

        public BarsController(IBarReviewServices barReviewServices, IBarServices barServices, ICityServices cityServices)
        {
            this.barReviewServices = barReviewServices;
            this.barServices = barServices;
            this.cityServices = cityServices;
        }
        public async Task<IActionResult> Index(int id = 1)
        {
            var listOfBars = new BarsListViewModel();

            listOfBars.Paging.Count = await barServices.AllBarsCountAsync();
            listOfBars.Paging.ItemsPerPage = itemsPerPage;
            listOfBars.Paging.CurrentPage = id;

            listOfBars.AllBars = (await barServices.GetAllDTOAsync(listOfBars.Paging.ItemsPerPage, listOfBars.Paging.CurrentPage))
                .Select(c => c.MapToViewModel());

            return View(listOfBars);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Add()
        {
            var barVM = new AddBarViewModel();

            var cities = await cityServices.GetAllDTOAsync();
            barVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View(barVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddBarViewModel bar)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await barServices.AddAsync(bar.Name, bar.ImageURL, bar.PhoneNumber, bar.Address.MapToDTO());
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["Status"] = ex.Message;
                    return View(bar);
                }
            }

            ModelState.AddModelError("", "Something went wrong...");
            return View(bar);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var listWithReviews = await barReviewServices.AllReviewsAsync(id);
                var bar = (await barServices.GetDetailedDTOAsync(id));
                var barVM = bar.MapToViewModel();

                barVM.BarReviews = listWithReviews.Select(r => r.MapToViewModel());

                return View(barVM);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCocktails(int id)
        {
            try
            {
                var barVM = new EditCocktailsViewModel();
                var dto = await barServices.GetBasicDTOAsync(id);

                barVM.BarName = dto.Name;
                barVM.ImageUrl = dto.ImageURL;

                barVM.PresentCocktails = (await barServices.GetPresentCocktailsAsync(id))
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

                barVM.NotPresentCocktails = (await barServices.NotPresentCocktailsAsync(id))
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

                return View(barVM);
            }
            catch (InvalidCastException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCocktails(int id, EditCocktailsViewModel vm)
        {
            try
            {
                await barServices.AddCocktailsAsync(id, vm.CocktailsToAdd);
                await barServices.RemoveCocktailsAsync(id, vm.CocktailsToRemove);

                return RedirectToAction("Details", new { id = id });
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var barToEditDTO = await barServices.GetBarToEditDTOAsync(id);
                var barToEditVM = barToEditDTO.MapToViewModel();

                var cities = await cityServices.GetAllDTOAsync();
                barToEditVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

                return View(barToEditVM);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddBarViewModel vm)
        {
            try
            {
                var barToEdit = vm.MapToDTO();
                barToEdit.Id = id;

                await barServices.EditAsync(barToEdit);
                return RedirectToAction("Details", new { id = id });
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await barServices.DeleteAsync(id);
                return RedirectToAction("Index");
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> Search(int id, [FromQuery] BarSearchViewModel vm)
        {
            var allCities = (await cityServices.GetAllDTOAsync());
            vm.AllCities = new List<SelectListItem>();
            vm.AllCities.Add(new SelectListItem("Select...", ""));

            allCities.ForEach(c => vm.AllCities.Add(new SelectListItem(c.Name, c.Id.ToString())));

            if (string.IsNullOrEmpty(vm.NameKey) && vm.MinRating == null && vm.CityId == null)
            {
                return View(vm);
            }
            var dto = new BarSearchDTO
            {
                NameKey = vm.NameKey,
                CityId = vm.CityId,
                MinRating = vm.MinRating
            };

            vm.Paging.Count = await barServices.SerchResultCountAsync(dto);
            vm.Paging.ItemsPerPage = itemsPerPage;
            vm.Paging.CurrentPage = id == 0 ? 1 : id;

            vm.Result = (await barServices.SearchAsync(dto, itemsPerPage, vm.Paging.CurrentPage))
                .Select(b => b.MapToViewModel());

            return View(vm);
        }

        [Authorize]
        public async Task<IActionResult> BarReview(int id)
        {
            try
            {
                var bar = await barServices.GetDetailedDTOAsync(id);
                var barViewModel = bar.MapToViewModel();

                var vm = new BarReviewViewModel
                {
                    Bar = barViewModel,
                };
                return View(vm);
            }
            catch (InvalidOperationException)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BarReview([FromBody]ReviewViewModel vm)
        {
            if (vm.Rating != null || !string.IsNullOrWhiteSpace(vm.Comment))
            {
                vm.UserName = User.Identity.Name;
                await barReviewServices.AddReviewAsync(vm.Comment, vm.Rating, vm.UserName, vm.Id);

                return PartialView("_ReviewPartial", vm);
            }

            return RedirectToAction("Details", "Bars", new { id = vm.Id });
        }
    }
}