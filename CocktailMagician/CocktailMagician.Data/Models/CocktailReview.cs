namespace CocktailMagician.Data.Models
{
    public class CocktailReview
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? Rating { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int CocktailId { get; set; }
        public Cocktail Coctail { get; set; }
        public bool IsDeleted { get; set; }
    }
}
