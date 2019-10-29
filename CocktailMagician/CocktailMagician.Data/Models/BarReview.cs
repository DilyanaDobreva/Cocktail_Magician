namespace CocktailMagician.Data.Models
{
    public class BarReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int BarId { get; set; }
        public Bar Bar { get; set; }
        public bool IsDeleted { get; set; }
    }
}
