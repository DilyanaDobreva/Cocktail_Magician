using System;
namespace CocktailMagician.Web.Areas.ViewModels.Users
{
    public class BannViewModel
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
        public string Reason { get; set; }
        public DateTime EnDateTime { get; set; }
    }
}
