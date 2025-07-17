using FCB.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using FCB.Repositories;
using FCB.Services;
using Microsoft.AspNetCore.Localization;

namespace FCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            //localization
            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

            builder.Services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new[]
                {
                    //new CultureInfo("en-US"),
                    new CultureInfo("tr-TR")
                };

                options.DefaultRequestCulture = new RequestCulture("tr-TR");
                //options.DefaultRequestCulture = new RequestCulture("en-US");

                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });


            //SQL CONNECTÝON
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("FCBConnection")));

            //REPOSÝTORÝES CONNECTÝON
            builder.Services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPersonRepository, PersonRepository>();  

            // SERVÝCES CONNECTÝON
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IPersonService, PersonService>();
            builder.Services.AddScoped<IFileStorageService, FileStorageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseRequestLocalization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
