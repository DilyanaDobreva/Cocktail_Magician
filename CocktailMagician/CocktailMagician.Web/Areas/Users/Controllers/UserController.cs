using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CocktailMagician.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class UserController : Controller
    {
        private readonly IUserServices userService;

        public UserController(IUserServices userService)
        {
            this.userService = userService;
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {

            var users = await userService.GetListOfUsersDTO();
            var listOfUsers = new AllUsersViewModel(users);
            return View(listOfUsers);
        }
    }
}
