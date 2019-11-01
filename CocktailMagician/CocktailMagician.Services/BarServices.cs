using CocktailMagician.Data;
using CocktailMagician.Services.Contracts;
using System.Threading.Tasks;

namespace CocktailMagician.Services
{
    public class BarServices :IBarServices
    {
        private readonly CocktailMagicianDb context;
        private readonly IBarServices barServices;

        public BarServices(CocktailMagicianDb context, IBarServices barServices)
        {
            this.context = context;
            this.barServices = barServices;
        }

        //public Task  Add(string Name, string address, int cityId )
        //{

        //}
    }
}
