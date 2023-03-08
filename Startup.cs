using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission9_pthoma24.Models;

namespace Mission9_pthoma24
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }

        public Startup (IConfiguration temp)
        {
            Configuration = temp;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookstoreContext>(options =>
           {
               options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);

           });

            services.AddScoped<IBookstoreRepository, EFBookstoreRepository>();

            services.AddRazorPages(); // Enables use of razor pages (start)

            services.AddDistributedMemoryCache(); // Sets up the ability for the user to use a session everytime they access the site (start)
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            
            // Corresponds to the wwwroot
            app.UseStaticFiles();
            app.UseSession(); // Sets up the ability for the user to use a session everytime they access the site (end)
            app.UseRouting();

            // Controls how the end navigation parameters of the url appear, order matters (not like css, like an if-else statement)
            app.UseEndpoints(endpoints =>
            {
                // If a type and a page number is specified:
                endpoints.MapControllerRoute(
                    "categorypage",
                    "{bookCategory}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                // specifies the url to return the pageNum integer rather than the default syntax with a question mark
                endpoints.MapControllerRoute(
                    "Paging",
                    "Page{pageNum}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                // If only a type is specified:
                endpoints.MapControllerRoute(
                    "category",
                    "{bookCategory}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                // specifies the url to return default syntax for each page with question mark
                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages(); // Enables use of razor pages (end)
            });
        }
    }
}
