using System;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class LoginViewModel
    {
        [MinLength(5)]
        [Display(Name = "Username")]
        public string LoginUsername { get; set; }

        [MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string LoginPassword { get; set; }

        [Display(Name = "Keep me logged in?")]
        public bool RememberLogin { get; set; }
    }
}
