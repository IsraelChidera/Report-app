using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Report.BLL.Interfaces;
using Report.BLL.Models;
using Report.DAL.Entities;
using Report.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Report.DAL.Enums.HazardRatingEnum;
using static Report.DAL.Enums.RiskImpactEnum;
using static Report.DAL.Enums.RiskProbabilityEnum;

namespace Report.BLL.Implementation
{
    public class ReportListService : IReportListService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IRepository<Employee> _employeeRepo;
        private readonly IRepository<RiskReport> _reportRepo;

        public ReportListService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork= unitOfWork;
            _mapper= mapper;
            _employeeRepo=_unitOfWork.GetRepository<Employee>();
            _reportRepo=_unitOfWork.GetRepository<RiskReport>();
        }


        public async Task<(bool check, string message)> AddOrUpdateAsync(AddOrUpdateReportVM model)
        {
            Employee employee = await _employeeRepo.GetSingleByAsync(u => u.Id == model.EmployeeId, include: e => e.Include(x => x.ReportList), tracking: true);  

            if (employee == null)
            {
                return (false, $"User with id:{model.EmployeeId} wasn't found");
            }

            RiskReport report = employee.ReportList.SingleOrDefault(r=>r.Id == model.ReportId);

            if(report != null)
            {
                _mapper.Map(model, report);
                await _unitOfWork.SaveChangesAsync();
                return (true, "Update successful");
            }

            var newReport = _mapper.Map<RiskReport>(model);
            employee.ReportList.Add(newReport);

            var rowChanges = await _unitOfWork.SaveChangesAsync();
            return rowChanges > 0 ? (true, $"Task: {model.ReportId} was successfully created!") : (false, "Failed To save changes!");
        }


        public async Task<(bool check, string message)> DeleteAsync(int employeeId, int reportId)
        {         
            Employee employee = await _employeeRepo.GetSingleByAsync(e=>e.Id==employeeId, 
                include: e=>e.Include(r=>r.ReportList.Where(x=>x.EmployeeId==employeeId)),tracking:true );

            if (employee == null)
            {
                return (false, $"User with id:{employeeId} does not exist");
            }

            RiskReport employeereport = employee.ReportList.FirstOrDefault(x=>x.Id == reportId);

            if(employeereport != null)
            {
                employee.ReportList.Remove(employeereport);

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Report with id:{reportId} have been successfully deleted") :
                    (false, $"Failed to delete report");
            }

            return (false, $"Report with Id:{reportId} was not found");
        }

        public async Task<(bool successful, string msg)> EditProductAsync(ReportVM report)
        {

            Employee employee = await _employeeRepo.GetSingleByAsync(e => e.Id == report.Id,
               include: e => e.Include(r => r.ReportList.Where(x => x.EmployeeId == report.Id)), tracking: true);

            if(employee == null)
            {
                return (false, $"User with id:{report.EmployeeId} does not exist");
            }

            RiskReport employeereport = employee.ReportList.FirstOrDefault(x => x.Id == report.Id);
            
            if (employeereport != null)
            {
                employeereport.Location = report.Location;
                employeereport.HazardDescription = report.HazardDescription;
                employeereport.ResourceAtRisk = report.ResourceAtRisk;
                employeereport.RiskProbability = report.RiskProbability;
                employeereport.RiskImpact = report.RiskImpact;
                employeereport.PreventiveMeasure = report.PreventiveMeasure;
                employeereport.HazardRating = report.HazardRating;
                employeereport.AdditionalInfo = report.AdditionalInfo;

                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Report with id:{report.Id} have been successfully deleted") :
                   (false, $"Failed to delete report");
            }

            return (false, $"Report with Id:{report.EmployeeId} was not found");
        }

        public async Task<ReportVM> GetReportById(int id)
        {
            RiskReport report = await _reportRepo.GetByIdAsync(id);

            return new ReportVM()
            {
                Id = report.Id,
                Location = report.Location,
                HazardDescription = report.HazardDescription,
                ResourceAtRisk = report.ResourceAtRisk,
                RiskProbability = report.RiskProbability,
                RiskImpact = report.RiskImpact,
                PreventiveMeasure = report.PreventiveMeasure,
                HazardRating = report.HazardRating,
                AdditionalInfo = report.AdditionalInfo,
                EmployeeId = report.EmployeeId.ToString(),
            };
        }

        public IEnumerable<RiskReport> GetReports()
        {
            List<RiskReport> riskReports= new List<RiskReport>();

            var reports = riskReports;

            return reports;
        }
    }
}
