using System;
using System.Collections.Generic;
using CocktailMagician.Services.DTOs;
using CocktailMagician.Web.Mapper;

namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class AllUsersViewModel
    {
        public AllUsersViewModel()
        {
            this.ListOfUsers = new List<UserViewModel>();
        }
        public List<UserViewModel> ListOfUsers { get; set; }
        public AllUsersViewModel(IEnumerable<UserDTO> listOfUsers)
        {
            this.ListOfUsers = new List<UserViewModel>();
            foreach (var user in listOfUsers)
            {
                if (user.IsDeleted == false)
                {
                    this.ListOfUsers.Add(user.MapToViewModel());
                }
            }
        }
    }
}
