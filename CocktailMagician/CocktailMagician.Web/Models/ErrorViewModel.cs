using CocktailMagician.Web.Areas.Distribution.Models.Bars;
using CocktailMagician.Web.Areas.Distribution.Models.Cocktails;
using System.Collections.Generic;

namespace CocktailMagician.Web.Models
{
    public class ErrorViewModel
    {
        public string Message { get; set; }
        public string Code { get; set; }

        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
