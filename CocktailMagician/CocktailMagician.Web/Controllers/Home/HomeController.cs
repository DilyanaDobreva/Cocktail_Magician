using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Models;
using Microsoft.AspNetCore.Mvc;


namespace CocktailMagician.Web.Areas.Home
{
    public class HomeController : Controller
    {
        private readonly IBarServices barServices;
        private readonly ICocktailServices cocktailServices;
        public HomeController(IBarServices barServices, ICocktailServices cocktailServices)
        {
            this.cocktailServices = cocktailServices;
            this.barServices = barServices;
        }
        public async Task<IActionResult> Index()
        {
            var vm = new BarAndCoctailInListViewModel();

            vm.ListOfBars = (await barServices.GetMostPopular(3)).Select(b => b.MapToViewModel()).ToList();
            vm.ListOfCocktails = (await cocktailServices.GetMostPopular(4)).Select(c => c.MapToViewModel()).ToList();
            return View(vm);
        }
    }
}
