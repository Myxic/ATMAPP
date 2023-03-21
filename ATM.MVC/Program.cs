using ATM.BLL.Interface;
using ATM.BLL.Implementation;
using ATM.DAL.Repository;
using ATM.DAL.Data;
using Microsoft.EntityFrameworkCore;

namespace ATM.MVC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        //IConfigurationBuilder configurationBuilder = builder.Configuration
        //    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("secrets.json");

        //builder.Services.AddDbContext<ATM.DAL.Data.AtmDbContext>(options =>
        //        options.UseSqlServer(builder.Configuration.GetConnectionString("myConxStr")));


        // Add services to the container.
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AtmDbContext>(opts =>
        {
            // this will only work if there's a section called ConnectionStrings on the appSettings
            // var defaultConn = builder.Configuration.GetConnectionString("DefaultConn");

            var defaultConn = builder.Configuration.GetSection("ConnectionString")["DefaultConn"];

            opts.UseSqlServer(defaultConn);

        });

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork<AtmDbContext>>();

        builder.Services.AddScoped<ICustomerOperation, CustomerOperation>();

        builder.Services.AddScoped<IAdminOperations, AdminOperation>();

        //builder.Services.AddScoped<IRepository, Repository>();

        //builder.Services.AddI


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