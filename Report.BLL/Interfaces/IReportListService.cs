using Report.BLL.Models;
using Report.DAL.Entities;

namespace Report.BLL.Interfaces
{
    public interface IReportListService
    {
        Task<(bool check, string message)> AddOrUpdateAsync(AddOrUpdateReportVM model);
        Task<(bool check, string message)> DeleteAsync(int employeeId, int reportId);
        (RiskReport to, string message) GetReport(int employeeId, int taskId);
        IEnumerable<RiskReport> GetReports();
    }
}
