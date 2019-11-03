using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
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
    }
}