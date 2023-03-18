using Microsoft.EntityFrameworkCore;
using Report.BLL.Interfaces;
using Report.BLL.Models;
using Report.DAL.Entities;
using Report.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<RiskReport> _reportRepo;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork= unitOfWork;
            _employeeRepo = _unitOfWork.GetRepository<Employee>();
        }

        public void Create(CreateUserVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportVM> GetEmployeesReport(int Id)
        {
            var report =  _reportRepo.GetQueryable(r => r.EmployeeId == Id);

            return report.Select(r => new ReportVM
            {
                Id= r.Id,
                Location =r.Location,
                HazardDescription= r.HazardDescription,
                ResourceAtRisk=r.ResourceAtRisk,
                RiskProbability=r.RiskProbability,
                RiskImpact=r.RiskImpact,
                PreventiveMeasure=r.PreventiveMeasure,
                HazardRating=r.HazardRating,
                AdditionalInfo=r.AdditionalInfo,
                EmployeeId=r.EmployeeId.ToString(),
            });
        }

        public async Task<IEnumerable<EmployeeWithReportVM>> GetEmployeeWithReportAsync()
        {            

            return (await _employeeRepo.GetAllAsync(include:u=>u.Include(t=>t.ReportList))).Select(u=> new EmployeeWithReportVM
            {
                FullName= u.FullName,
                Reports=u.ReportList.Select(t=>new ReportVM
                {
                    Id = t.Id,
                    Location = t.Location,
                    ResourceAtRisk = t.ResourceAtRisk,
                    HazardDescription = t.HazardDescription,
                    HazardRating = t.HazardRating,
                    AdditionalInfo = t.AdditionalInfo,
                    RiskImpact = t.RiskImpact,
                    RiskProbability = t.RiskProbability,
                    PreventiveMeasure = t.PreventiveMeasure,
                })
            });
        }
    }
}
