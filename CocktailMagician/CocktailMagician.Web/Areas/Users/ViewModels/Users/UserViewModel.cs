﻿using System;
using System.Collections.Generic;

namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string NewPassword { get; set; }
        public string BannedReason { get; set; }
        public DateTime BannedEndTime { get; set; }
    }
}
