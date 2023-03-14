using AutoMapper;
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

        public Task<(bool check, string message)> DeleteAsync(int employeeId, int reportId)
        {
            throw new NotImplementedException();
        }

        public (RiskReport to, string message) GetReport(int employeeId, int taskId)
        {
            Employee employee = null;

            if(employee == null)
            {
                return (null, "User not found");
            }

            RiskReport report = employee.ReportList.FirstOrDefault(r => r.Id == taskId);

            if(report == null)
            {
                return (null, "Task not found");
            }

            return (report, "");
        }

        public IEnumerable<RiskReport> GetReports()
        {
            List<RiskReport> riskReports= new List<RiskReport>();

            var reports = riskReports;

            return reports;
        }
    }
}
