using Report.BLL.Models;
using Report.DAL.Entities;

namespace Report.BLL.Interfaces
{
    public interface IEmployeeService
    {
        void Create(CreateUserVM model);
        IEnumerable<Employee> GetEmployees();
        Task<IEnumerable<EmployeeWithReportVM>> GetEmployeeWithReportAsync();
    }
}
