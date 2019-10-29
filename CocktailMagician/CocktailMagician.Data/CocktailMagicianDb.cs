using Microsoft.EntityFrameworkCore;

namespace CocktailMagician.Data
{
    public class CocktailMagician : DbContext
    {
        public CocktailMagician()
        {

        }
        public CocktailMagician(DbContextOptions<CocktailMagician> options) : base(options)
        {

        }

    }
}
