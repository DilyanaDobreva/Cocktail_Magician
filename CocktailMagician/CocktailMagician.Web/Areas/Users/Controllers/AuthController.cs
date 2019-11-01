using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using static CocktailMagician.Web.Areas.ViewModels.Users.BannViewModel;

namespace CocktailMagician.Web.Areas.Users.Controllers
{
    [Area("Users")]
    public class AuthController : Controller
    {
        private readonly IUserServices userService;

        public AuthController(IUserServices userService)
        {
            this.userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            if (!this.ModelState.IsValid)
                return BackToHome();

            try
            {
                var user = await this.userService.RegisterUserAsync(vm.RegisterUsername, vm.RegisterPassword);
                await SignInUser(user, rememberLogin: false);

                return BackToHome();
            }
            catch (Exception ex)
            {
                this.TempData["Status"] = ex.Message;
                return BackToHome();
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {

            if (!this.ModelState.IsValid)
                return BackToHome();

            try
            {
                if (await userService.IsBannedAsync(vm.LoginUsername) == true)
                {
                    var member = await userService.FindUserDTOAsync(vm.LoginUsername);
                    var memberVm = new BannViewModel()
                    {
                        UserName = member.UserName,
                        Reason = member.BannReason,
                        EnDateTime = member.BannEndTime
                    };
                    this.TempData["Legit"] = true;
                    return Banned(memberVm);
                }
                var user = await this.userService.LoginAsync(vm.LoginUsername, vm.LoginPassword);
                await SignInUser(user, vm.RememberLogin);
                return BackToHome();
            }
            catch (Exception ex)
            {
                this.TempData["Status"] = ex.Message;
                return BackToHome();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return BackToHome();
        }
        public async Task<IActionResult> Bans(BannViewModel vm)
        {
            if (TempData["Legit"] == null)
            {
                return BackToHome();
            }
            if (!(await userService.IsBannedAsync(vm.UserName)))
            {
                return Unauthorized();
            }
            return View(vm);
        }

        private async Task SignInUser(UserDTO user, bool rememberLogin)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberLogin
            };


            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private RedirectToActionResult BackToHome()
            => RedirectToAction("Index", "Home");
        private RedirectToActionResult Banned(BannViewModel vm)
            => RedirectToAction("Bans", "Auth", vm);
    }
}