using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Areas.Distribution.Models.Cocktails
{
    public class CocktailReviewViewModel
    {
        public int? Rating { get; set; }
        public string Comment { get; set; }
        public ReviewViewModel Cocktail { get; set; }
        public string UserName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
