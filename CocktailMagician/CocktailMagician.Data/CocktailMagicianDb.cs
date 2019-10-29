using CocktailMagician.Data.Models;
using CocktailMagician.Data.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

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

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CocktailReview> CocktailReviews { get; set; }
        public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
        public DbSet<Cocktail> Cocktails { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BarReview> BarReviews { get; set; }
        public DbSet<BarCocktail> BarCocktails { get; set; }
        public DbSet<Bar> Bars { get; set; }
        public DbSet<Address> Addresses { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder
                    .ConfigureWarnings(options => options.Throw(RelationalEventId.QueryClientEvaluationWarning))
                    //.UseSqlServer(@"Server=localhost\KristiqnTashev, 1433;Database=LMSTEST;User=sa;Password=Whocanbe1;Trusted_Connection=False;");
                    .UseSqlServer("Server=.\\SQLEXPRESS;Database=CocktailMagician;Trusted_Connection=True;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(RoleSeed.roleSeed);
            modelBuilder.Entity<User>().HasData(UserSeed.userSeed);
        }
    }
}
