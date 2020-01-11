using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SportStore.Models;
using Microsoft.EntityFrameworkCore;


namespace SportStore
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
            services.AddTransient<IProductRepository, FakeProductRepository>();
            services.AddDbContext<ApplicatioDbContext>(options => options.UseSqlServer(Configuration["Data:SportStoreProducts:ConnectionString"]));
            services.AddTransient<IProductRepository, EFProductRepository>();
            services.AddScoped<Cart>(c => SessionCart.GetCart(c));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMemoryCache();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseSession();

            app.UseMvc(routes => {

                routes.MapRoute(
                name: null,
                template: "{category}/Page{productPage:int}",
                defaults: new { Controller = "Product", action = "List" });

                routes.MapRoute(
                name: null,
                template: "Page{productPage:int}",
                defaults: new { Controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(
                name: null,
                template: "{category}",
                defaults: new { Controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(
                name: null,
                template: "",
                defaults: new { Controller = "Product", action = "List", productPage = 1 });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Product}/{action=List}/{id?}"
                    );
            });

            SeedData.EnsurePopulated(app);
        }
    }
}
