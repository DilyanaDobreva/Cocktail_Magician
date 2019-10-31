﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class UpdateUserViewMode
    {
        public string Id { get; set; }
        public string Password { get; set; }
        [MinLength(5)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string NewPassword { get; set; }
        public List<SelectListItem> Role { get; set; }
        public int RoleId { get; set; }
    }
}
