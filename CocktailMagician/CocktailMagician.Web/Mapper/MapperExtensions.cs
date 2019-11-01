using System;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Areas.ViewModels.Users;

namespace CocktailMagician.Web.Mapper
{
    public static class MapperExtensions
    {
        
        public static UserViewModel MapToViewModel(this UserDTO user)
        {
            var vm = new UserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                RoleName = user.RoleName,
                


            };
            if (user.BannId != null)
            {

                vm.BannedReason = user.BannReason;
                vm.BannedEndTime = user.BannEndTime;
            }
            return vm;
        }
        public static BannViewModel MapToBanViewModel(this UserDTO member)
        {
            var vm = new BannViewModel
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
