using FCB.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Globalization;
using System.Text.RegularExpressions;
using FCB.Repositories;
using FCB.Services;

namespace FCB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // money culture
            var cultureInfo = new CultureInfo("tr-TR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
