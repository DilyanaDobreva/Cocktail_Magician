using CocktailMagician.Data;
using CocktailMagician.Services;
using CocktailMagician.Services.Contracts;
using CocktailMagician.Services.Contracts.Factories;
using CocktailMagician.Services.Factories;
using CocktailMagician.Services.Hasher;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CocktailMagician.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CocktailMagicianDb>(
                options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));


            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped<ICocktailServices, CocktailServices>();
            services.AddScoped<IIngredientServices, IngredientServices>();
            services.AddScoped<IReviewServices, ReviewServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IBannFactory, BannFactory>();
            services.AddScoped<IIngredientFactory, IngredientFactory>();
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IHasher, Hasher>();
            services.AddScoped<ICocktailServices, CocktailServices>();
            services.AddScoped<IBarFactory, BarFactory>();
            services.AddScoped<ICityFactory, CityFactory>();
            services.AddScoped<ICocktailFactory, CocktailFactory>();
            services.AddScoped<ICocktailIngredientFactory, CocktailIngredientFactory>();
            services.AddScoped<IBarCocktailFactory, BarCocktailFactory>();
            services.AddScoped<IIngredientServices, IngredientServices>();
            services.AddScoped<IBarServices, BarServices>();
            services.AddScoped<ICityServices, CityServices>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            //app.UseStatusCodePagesWithRedirects("/Home/Error?code={0}");


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
