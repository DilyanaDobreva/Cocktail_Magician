using System;
using System.Linq;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Web.Areas.ViewModels.Users;
using CocktailMagician.Web.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CocktailMagician.Web.Areas.ViewModels.Users.BannViewModel;

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
                    return RedirectToAction("Index", "User", "Users");
                }
                await this.userService.RegisterAdminAsync(vm.RegisterUsername, vm.RegisterPassword);
                return RedirectToAction("Index", "User", "Users");
            }
            catch (Exception ex)
            {
                this.TempData["Status"] = ex.Message;
                return RedirectToAction("Create", "User", "Users");
            }
        }
        [Authorize]
        public async Task<IActionResult> Edit(string id)
        {
            if (User.IsInRole("admin"))
            {
                var user = await userService.GetUserInfoAsync(id);

                var allRoles = await this.userService.GetAllRoles();
                var viewModel = new UpdateUserViewMode()
                {
                    Role = allRoles.Select(r => new SelectListItem(r.RoleName, r.Id.ToString())).ToList()
                };

                if (user.RoleName == "admin")
                    viewModel.Role.Reverse();
                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
            else
            {
                var currentMember = User.Identity.Name;
                var user = await userService.FindUserDTOAsync(currentMember);
                id = user.Id;
                var viewModel = new UpdateUserViewMode()
                {
                    Id = id
                };

                if (viewModel == null)
                {
                    return NotFound();
                }
                return View(viewModel);
            }
        }
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserViewMode vm)
        {
            if (!this.ModelState.IsValid)
            {
                throw new Exception();
            }

            
            if (User.IsInRole("user"))
            {
                await this.userService.UpdateUserAsync(vm.Id, vm.Password, vm.NewPassword, vm.RoleId);
                return RedirectToAction("Index", "Home");
            }
            await this.userService.UpdateUserAsync(vm.Id, vm.Password, vm.NewPassword, vm.RoleId);
            return RedirectToAction("Index", "User", "Users");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Ban(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var user = await userService.GetUserInfoAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                var viewModel = user.MapToBanViewModel();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "User", "Users");
            }
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Ban(BannViewModel vm)
        {
            if (!this.ModelState.IsValid)
            {
                throw new Exception();
            }
            var userDTO = await userService.FindUserDTOAsync(vm.UserName);
            await this.userService.BanAsync(vm.Reason, userDTO);
            return RedirectToAction("Index", "User", "Users");
        }
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userService.GetUserInfoAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var viewModel = user.MapToViewModel();
            return View(viewModel);
        }

        [HttpPost,ActionName("Delete")]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (!this.ModelState.IsValid)
            {
                throw new Exception();
            }
            await this.userService.DeleteUserAsync(id);
            return RedirectToAction("Index", "User", "Users");
        }
    }
}
