using CocktailMagician.Data.Models;
using System.Collections.Generic;

namespace CocktailMagician.Data.Seed
{
    public static class CocktailSeed
    {
        public static readonly List<Cocktail> cocktails = new List<Cocktail>
        {
            new Cocktail
            {
                Id = 1,
                Name = "Old Fashioned",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-164770405-1-1508961546.jpg?crop=1xw:1xh;center,top&resize=980:*"
            },
            new Cocktail
            {
                Id = 2,
                Name = "Margarita",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-516883622-1508961864.jpg?crop=0.44377777777777777xw:1xh;center,top&resize=980:*"
            },
            new Cocktail
            {
                Id = 3,
                Name = "Cosmopolitan",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/an-alcoholic-cosmopolitan-cocktail-is-on-the-bar-royalty-free-image-890771104-1557247368.jpg?crop=0.447xw:1.00xh;0.446xw,0&resize=980:*"
            },
            new Cocktail
            {
                Id = 4,
                Name = "Negroni",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/cocktail-negroni-on-a-old-wooden-board-drink-with-royalty-free-image-922744216-1557251200.jpg?crop=0.447xw:1.00xh;0.434xw,0&resize=980:*"
            },
            new Cocktail
            {
                Id = 5,
                Name = "Moscow Mule",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-834848932-1508962243.jpg?crop=0.9998698425094363xw:1xh;center,top&resize=980:*"
            },
            new Cocktail
            {
                Id = 6,
                Name = "Mojito",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/close-up-of-mojito-on-table-royalty-free-image-998866018-1557246957.jpg?crop=1xw:1xh;center,top&resize=980:*"
            },
            new Cocktail
            {
                Id = 7,
                Name = "Whiskey Sour",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-126551868-1-1508962528.jpg?crop=1.00xw:0.949xh;0,0.0259xh&resize=980:*"
            },
            new Cocktail
            {
                Id = 8,
                Name = "Manhattan",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-89804127-1508971287.jpg?crop=0.864516129032258xw:1xh;center,top&resize=980:*"
            },
            new Cocktail
            {
                Id = 9,
                Name = "Mimosa",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/homemade-refreshing-orange-mimosa-cocktails-royalty-free-image-538644352-1557251390.jpg?crop=0.447xw:1.00xh;0.111xw,0&resize=980:*"
            },
            new Cocktail
            {
                Id = 10,
                Name = "Gimlet",
                ImageUrl = "https://hips.hearstapps.com/hmg-prod.s3.amazonaws.com/images/gettyimages-86159584-1508963306.jpg?crop=0.785xw:0.785xh;0,0.176xh&resize=980:*"
            }
        };
    }
}
