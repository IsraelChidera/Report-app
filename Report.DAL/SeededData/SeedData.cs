using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Report.DAL.Entities;
using static Report.DAL.Enums.HazardRatingEnum;
using static Report.DAL.Enums.RiskImpactEnum;
using static Report.DAL.Enums.RiskProbabilityEnum;

namespace Report.DAL.SeededData
{
    public class SeedData
    {

        public static async Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            ReportDbContext context = app.ApplicationServices.CreateScope().ServiceProvider
                .GetRequiredService<ReportDbContext>();

            //var check = ;
            if (!await context.Employees.AnyAsync())
            {
                await context.Employees.AddRangeAsync(EmployeesWithReport());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Employee> EmployeesWithReport()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    FullName = "Israel Chidera",
                    Password="Joshua123",
                    Email="Israelchidera43@gmail.com",
                    Title="Front end guy",
                    ReportList= new List<RiskReport>()
                    {
                        new RiskReport()
                        {
                             Location="Ikeja, Lagos",
                             HazardDescription="Environment is ...",
                             ResourceAtRisk="Environment",
                             RiskProbability= RiskProbability.Medium,
                             RiskImpact=RiskImpact.middle,
                             PreventiveMeasure="To ... ...",
                             HazardRating=HazardRating.low,
                             AdditionalInfo="Jo .."
                        }
                    }
                },

                new Employee()
                {
                    FullName = "Israel C. Chidera",
                    Password="Joshua6123",
                    Email="Israelchidguyera43@gmail.com",
                    Title="software dev",
                    ReportList= new List<RiskReport>()
                    {
                        new RiskReport()
                        {
                             Location="Enugu, Enugu",
                             HazardDescription="Environment is ...",
                             ResourceAtRisk="Environment",
                             RiskProbability= RiskProbability.Medium,
                             RiskImpact=RiskImpact.middle,
                             PreventiveMeasure="To ... ...",
                             HazardRating=HazardRating.low,
                             AdditionalInfo="Jo .."
                        }
                    }
                },
            };
        }

    }
}
