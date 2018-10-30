using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using InternetStore.Models.Repository;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using InternetStore.Infrastructure;
using Microsoft.AspNetCore.Http;
using InternetStore.Infrastructure.CustomModelBinder;
using Microsoft.AspNetCore.Identity;
using InternetStore.Models;
using InternetStore.Models.Identity;

namespace InternetStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(mvcOption => mvcOption.ModelBinderProviders.Insert(0, new CustomIntegerModelBinderProvider()));
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            string connection = Configuration["Data:InternetStoreProducts:ConnectionString"];
            services.AddDbContext<AppDbContext>(optionsAction => optionsAction.UseSqlServer(connection));

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(
                    Configuration["Data:InternetStoreIdentity:ConnectionString"]));

            services.AddIdentity<AppUser, AppRole>(identityOptions =>
            {
                identityOptions.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequiredLength = 4;
                identityOptions.Password.RequireDigit = false;
                identityOptions.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();

            services.AddMemoryCache();
            services.AddSession();

            services.AddScoped(provider => SessionCart.GetCart(provider));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeine.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
            app.UseStatusCodePages();
            app.UseSession();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseBrowserLink();
            app.UseAuthentication();
            app.UseMvc(route =>
            {
                route.MapRoute(null, "{category}/Page{page}", new { controller = "Home", action = "List" });
                route.MapRoute(null, "Page{page}", new { controller = "Home", action = "List", page = 1 });
                route.MapRoute(null, "{category}", new { controller = "Home", action = "List", page = 1 });
                route.MapRoute(null, "", new { controller = "Home", action = "List", page = 1 });

                route.MapRoute(null, "{controller}/{action}/{id?}");
            });

            AppIdentityDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
            SeedData.EnsurePopulated(app.ApplicationServices);
        }
    }
}
