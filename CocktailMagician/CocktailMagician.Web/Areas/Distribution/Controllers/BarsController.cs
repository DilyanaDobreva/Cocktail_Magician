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

            var cities = await cityServices.GetAllDTOsync();
            barVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View(barVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(AddBarViewModel bar)
        {
            await barServices.Add(bar.Name, bar.ImageURL, bar.Address.MapToDTO());
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {

            var listWithReviews = await barReviewServices.AllReviewsAsync(id);
            var bar = (await barServices.GetDetailedDTOAsync(id));
            var barVM = bar.MapToViewModel();

            barVM.BarReviews = listWithReviews.Select(r => r.MapToViewModel());

            return View(barVM);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> EditCocktails(int id)
        {
            var barVM = new EditCocktailsViewModel();
            barVM.BarName = await barServices.GetNameAsync(id);

            barVM.PresentCocktails = (await barServices.GetPresentCocktailsAsync(id))
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            barVM.NotPresentCocktails = (await barServices.NotPresentCocktailsAsync(id))
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View(barVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCocktails(int id, EditCocktailsViewModel vm)
        {
            await barServices.AddCocktailsAsync(id, vm.CocktailsToAdd);
            await barServices.RemoveCocktailsAsync(id, vm.CocktailsToRemove);

            return RedirectToAction("Details", new { id = id });
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var barToEditDTO = await barServices.GetBarToEditDTOAsync(id);
            var barToEditVM = barToEditDTO.MapToViewModel();

            var cities = await cityServices.GetAllDTOsync();
            barToEditVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View(barToEditVM);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AddBarViewModel vm)
        {
            var barToEdit = vm.MapToDTO();
            barToEdit.Id = id;

            await barServices.EditAsync(barToEdit);
            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (int id)
        {
            await barServices.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Search([FromQuery] BarSearchViewModel vm)
        {
            var allCities = (await cityServices.GetAllDTOsync());
            vm.AllCities = new List<SelectListItem>();
            vm.AllCities.Add(new SelectListItem("Select...", ""));

            allCities.ForEach(c => vm.AllCities.Add(new SelectListItem(c.Name, c.Id.ToString())));

            if (string.IsNullOrEmpty(vm.NameKey) && vm.MinRating == null && vm.CityId == null)
            {
                return View(vm);
            }

            vm.Result = (await barServices.SearchAsync(vm.NameKey, vm.CityId, vm.MinRating))
                .Select(b => b.MapToViewModel());

            return View(vm);
        }

        public async Task<IActionResult> BarReview(int id)
        {
            var bar = await barServices.GetDetailedDTOAsync(id);
            var barViewModel = bar.MapToViewModel();

            var vm = new BarReviewViewModel
            {
                Bar = barViewModel,
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BarReview(int id, BarReviewViewModel vm)
        {
            if (vm.Rating != null || !string.IsNullOrWhiteSpace(vm.Comment))
            {
                var memberName = User.Identity.Name;
                await barReviewServices.AddReviewAsync(vm.Comment, vm.Rating, memberName, id);
            }

            return RedirectToAction("Details", "Bars", new { id = id });
        }
    }
}