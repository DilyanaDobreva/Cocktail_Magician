using System;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create()
        {
            var allRoles = await this.userService.GetAllRoles();

            var vm = new CreateUserViewModel()
            {
                Role = allRoles.Select(r => new SelectListItem(r.RoleName, r.Id.ToString())).ToList()
            };
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUserViewModel vm)
        {
            try
            {
                if (vm.RoleId == 1)
                {
                    await this.userService.RegisterUserAsync(vm.RegisterUsername, vm.RegisterPassword);
                    return RedirectToAction("Index", "User","Users");
                }
                await this.userService.RegisterAdminAsync(vm.RegisterUsername, vm.RegisterPassword);
                return RedirectToAction("Index", "User", "Users");
            }
            catch (Exception ex)
            {
                this.TempData["Status"] = ex.Message;
                return RedirectToAction("Create", "User","Users");
            }
        }
    }
}
