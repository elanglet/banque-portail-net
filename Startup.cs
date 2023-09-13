using BanqueNET.Dal;
using BanqueNET.Service;
using BanqueNET.Helpers;
using BanqueNET.Middlewares;
using Microsoft.EntityFrameworkCore;

using MySqlConnector;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BanqueNET {

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
            services.AddControllersWithViews();


            /* SECU */
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            /* 1. E.F. - MySQL */
            services.AddDbContext<DataContext>(
                options => options.UseMySql(Configuration.GetConnectionString("BanqueDataBase"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"))
            );
            services.AddScoped<IBanqueService, BanqueServiceEF>();


            /* 3. Connexion MySQL
            services.AddTransient<MySqlConnection>(_ => new MySqlConnection(Configuration["ConnectionStrings:BanqueDataBase"]));
            services.AddScoped<IBanqueService, BanqueServiceMySQL>();
             */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env , ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                /* SECU */
                app.UseHsts();
            }

            /* SECU */
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            
            app.UseSession();
            
            //loggerFactory.AddFile("C:/temp/logs/log-{Date}.txt");
            //app.UseSecuriteMiddleware(); 

            app.UseSessionMiddleware();
            app.UseAntiXSSMiddleware();

            app.UseRouting();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}