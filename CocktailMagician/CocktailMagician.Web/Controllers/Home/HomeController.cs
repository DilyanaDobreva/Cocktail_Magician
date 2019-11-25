using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using Microsoft.AspNetCore.Mvc;


namespace CocktailMagician.Web.Areas.Home
{
    public class HomeController : Controller
    {
        private readonly IBarServices barServices;
        public HomeController(IBarServices barServices)
        {
            this.barServices = barServices;
        }
        public async Task<IActionResult> Index()
        {
            var list = await barServices.GetMostPopular(3);
            var catalogVM = list.Select(b => b.MapToViewModel()).ToList();

            return View(catalogVM);
        }
    }
}
