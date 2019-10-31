using System;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.ViewModels.Users;
using static CocktailMagician.Web.Areas.ViewModels.Users.BannViewModel;

namespace CocktailMagician.Web.Mapper
{
    public static class MapperExtensions
    {
        
        public static UserViewModel MapToViewModel(this UserDTO member)
        {
            var vm = new UserViewModel
            {
                LoggerId = member.Id,
                UserName = member.UserName,
                RoleName = member.RoleName,
                


            };
            if (member.BannId != null)
            {

                vm.BannedReason = member.BannReason;
                vm.BannedEndTime = member.BannEndTime;
            }
            return vm;
        }
        public static BannedViewModel MapToBanViewModel(this UserDTO member)
        {
            var vm = new BannedViewModel
            {
                UserName = member.UserName,
                RoleName = member.RoleName
            };
            if (member.BannId != null)
            {
                vm.Reason = member.BannReason;
                vm.EnDateTime = member.BannEndTime;
            }
            return vm;
        }
    }
}
