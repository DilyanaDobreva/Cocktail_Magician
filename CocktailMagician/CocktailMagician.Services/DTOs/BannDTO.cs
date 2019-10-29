using System;
namespace CocktailMagician.Services.DTOs
{
    public class BannDTO
    {
        public string Id { get; set; }
        public string Reason { get; set; }
        public DateTime EnDateTime { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
