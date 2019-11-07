﻿using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class BarsController : Controller
    {
        private readonly IBarServices barServices;
        private readonly ICityServices cityServices;

        public BarsController(IBarServices barServices, ICityServices cityServices)
        {
            this.barServices = barServices;
            this.cityServices = cityServices;
        }
        public async Task<IActionResult> Index()
        {
            var listOfBars = new BarsListViewModel();
            listOfBars.AllBars = (await barServices.GetAllDTO())
                .Select(c => c.MapToViewModel());

            return View(listOfBars);
        }

        public async Task<IActionResult> Add()
        {
            var barVM = new AddBarViewModel();

            var cities = await cityServices.GetAllDTO();
            barVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View(barVM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddBarViewModel bar)
        {
            await barServices.Add(bar.Name, bar.ImageURL, bar.Address.MapToDTO());
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int id)
        {
            var bar = (await barServices.GetDetailedDTO(id));
            var barVM = bar.MapToViewModel();

            return View(barVM);
        }
        public async Task<IActionResult> EditCocktails(int id)
        {
            var barVM = new EditCocktailsViewModel();
            barVM.BarName = await barServices.GetName(id);

            barVM.PresentCocktails = (await barServices.GetPresentCocktails(id))
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            barVM.NotPresentCocktails = (await barServices.NotPresentCocktails(id))
                .Select(c => new SelectListItem(c.Name, c.Id.ToString()));

            return View(barVM);
        }
        [HttpPost]
        public async Task<IActionResult> EditCocktails(int id, EditCocktailsViewModel vm)
        {
            var barCocktailDTO = new EditCocktailsDTO()
            {
                BarId = id,
                CocktailsToAdd = vm.CocktailsToAdd,
                CocktailsToRemove = vm.CocktailsToRemove
            };
            await barServices.EditCocktails(barCocktailDTO);

            return RedirectToAction("Details", new { id = id });
        }
        public async Task<IActionResult> Edit(int id)
        {
            var barToEditDTO = await barServices.GetBarToEditDTO(id);
            var barToEditVM = barToEditDTO.MapToViewModel();

            var cities = await cityServices.GetAllDTO();
            barToEditVM.AllCities = cities.Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();

            return View(barToEditVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddBarViewModel vm)
        {
            var barToEdit = vm.MapToDTO();
            barToEdit.Id = id;

            await barServices.Edit(barToEdit);
            return RedirectToAction("Details", new { id = id });
        }
    }
}