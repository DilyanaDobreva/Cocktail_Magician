using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.Distribution.Mapper;
using CocktailMagician.Web.Areas.Distribution.Models.City;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Distribution.Controllers
{
    [Area("Distribution")]
    public class CityController : Controller
    {
        private readonly ICityServices cityServices;

        public CityController(ICityServices cityServices)
        {
            this.cityServices = cityServices;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CityViewModel vm)
        {
            var city = (await cityServices.AddAsync(vm.Name)).MapToViewModel();

            return Json(city);
        }

    }
}