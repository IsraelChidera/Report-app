using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Report.DAL.Entities
{
    public class ReportDbContext : DbContext
    {
        public ReportDbContext(DbContextOptions<ReportDbContext> options):base(options)
        {

        }

        public DbSet<RiskReport> Reports { get; set;}
        public DbSet<Employee> Employees { get; set;}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Employee>()
                .Property(e => e.FullName)
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(e=>e.Title) 
                .HasMaxLength(50)
                .IsRequired();

            builder.Entity<Employee>()
                .Property(e => e.Password)
                .HasMaxLength(50);

            builder.Entity<Employee>()
                .HasIndex(e => e.Email, "IX_UniqueEmail")
                .IsUnique();

            builder.Entity<RiskReport>()
                .Property(r => r.HazardDescription)
            .HasMaxLength(1000);

            base.OnModelCreating(builder);
        }

        /*public static IList<Employee> GetEmployeesWithReports()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    ID= 1,
                    FullName="Israel Chidera",
                    Age= 34,
                    Title="Customer Relationship",
                    Email="Israelchidera54@gmail.com",
                    Password="Israel1234",
                    Reports= new List<Report>()
                    {
                        new Report()
                        {
                            ID= 1,
                            Location="Ikeja, Lagos",
                            HazardDescription="Environment is ...",
                            ResourceAtRisk="Environment",
                            RiskProbability= RiskProbability.Medium,
                            RiskImpact=RiskImpact.middle,
                            PreventiveMeasure="To ... ...",
                            HazardRating=HazardRating.medium,
                            AdditionalInfo="Jo .."
                        }
                    }
                },

                new Employee()
                {
                    ID= 2,
                    FullName="Israel Chidera",
                    Age= 24,
                    Title="Software",
                    Email="softwareguy54@gmail.com",
                    Password="ICguyel1234",
                    Reports= new List<Report>()
                    {
                        new Report()
                        {
                            ID= 2,
                            Location="Lekki, Lagos",
                            HazardDescription="Softweare ...",
                            ResourceAtRisk="Software",
                            RiskProbability= RiskProbability.Medium,
                            RiskImpact=RiskImpact.middle,
                            PreventiveMeasure="To ... ...",
                            HazardRating=HazardRating.low,
                            AdditionalInfo="Jo .."
                        }
                    }
                },

            };
        }*/

    }

}
