namespace CocktailMagician.Services
{
    public static class OutputConstants
    {
        //Exception Messages
        public const string IngredientPartOfCocktails = "Ingredient you're trying to delete is part of some coktail.";
        public const string CocktailWithNoIngredients = "This cocktail contains no ingredients.";
        public const string CoctailIncludeIngredient = "This ingredient is part of some cocktail and cannot be deleted.";
        public const string IngredientAlreadyExists = "This ingredient already exists in data base.";
        public const string CityAlreadyExists = "This city already exists in data base.";
        public const string InvalidId = "Invalid id";
        public const string NoIngredientQuantity = "Missing ingredient quantity.";
        public const string CocktailExists = "Cocktail with this name already exists.";
        public const string BarExists = "Bar with this name already exists.";
        public const string InvalidIngredient = "Invalid ingredient.";
        public const string MissingIngredient = "Missing ingredient {0}.";


    }
}
