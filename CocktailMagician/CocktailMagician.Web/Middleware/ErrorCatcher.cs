using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CocktailMagician.Web.Middleware
{
    public class ErrorCatcher
    {
        private readonly RequestDelegate next;

        public ErrorCatcher(RequestDelegate next)
        {
            this.next = next;
        }


        public async Task Invoke(HttpContext httpContext)
        {

            await this.next(httpContext);

            if (httpContext.Response.StatusCode == 404 || httpContext.Response.StatusCode == 500 || httpContext.Response.StatusCode == 400)
            {
                httpContext.Response.Redirect("/home/error");
            }

        }
    }
}
