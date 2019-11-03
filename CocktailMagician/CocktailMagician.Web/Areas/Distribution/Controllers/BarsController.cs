using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class BarsController : Controller
    {
        private readonly IBarServices barServices;

        public BarsController(IBarServices barServices)
        {
            this.barServices = barServices;
        }
        public async Task<IActionResult> Index()
        {
            var listOfBars = new BarsListViewModel();
            listOfBars.AllBars = (await barServices.GetAllDTO())
                .Select(c => c.MapToViewModel());

            return View(listOfBars);
        }
    }
}