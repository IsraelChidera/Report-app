using Report.BLL.Models;
using Report.DAL.Entities;

namespace Report.BLL.Interfaces
{
    public interface IReportListService
    {
        Task<(bool check, string message)> AddOrUpdateAsync(AddOrUpdateReportVM model);
        Task<(bool check, string message)> DeleteAsync(int employeeId, int reportId);
        Task<(bool successful, string msg)> EditProductAsync(ReportVM report);
        Task<ReportVM> GetReportById(int reportId);
        IEnumerable<RiskReport> GetReports();
    }
}
