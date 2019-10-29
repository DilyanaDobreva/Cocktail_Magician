using System;
namespace CocktailMagician.Services.DTOs
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string BannId { get; set; }
        public string BannReason { get; set; }
        public DateTime BannEndTime { get; set; }
    }
}
