using Report.DAL.Entities;
using Report.DAL.SeededData;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Report.DAL.Repository;
using Report.BLL.Interfaces;
using Report.BLL.Implementation;

namespace MvcReportApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ReportDbContext>(option =>
            {
                var defaultConn = builder.Configuration.GetSection("ConnectionStrings")["DefaultConn"];
                option.UseSqlServer(defaultConn);
            });
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork<ReportDbContext>>();


            builder.Services.AddScoped<IEmployeeService, EmployeeService>();//todo: show other life-cycles
            builder.Services.AddScoped<IReportListService, ReportListService>();

            builder.Services.AddAutoMapper(Assembly.Load("Report.BLL"));


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


            await SeedData.EnsurePopulatedAsync(app);


            await app.RunAsync();
        }
    }
}