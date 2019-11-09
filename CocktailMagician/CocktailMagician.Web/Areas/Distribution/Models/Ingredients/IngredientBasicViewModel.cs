namespace CocktailMagician.Web.Areas.Distribution.Models.Ingredients
{
    public class IngredientBasicViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public bool CanDelete { get; set; }
    }
}
