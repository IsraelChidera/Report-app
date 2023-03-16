using Report.BLL.Models;
using Report.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Interfaces
{
    public interface IUserService
    {
        Task<(bool check, string message)> UserRegisterAsync(UserViewModel model);
        Task<(bool check, string message, Employee user)> UserLoginAsync(LoginViewModel user);
        Task<UserProfileViewModel> GetEmployeeDetailAsync(int id);
        Task<(bool check, string message)> UpdateEmployeeProfileAsync(UserProfileViewModel model);  
    }
}
