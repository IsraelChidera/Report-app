using Report.BLL.Interfaces;
using Report.BLL.Models;
using Report.DAL.Entities;
using Report.DAL.Repository;

namespace Report.BLL.Implementation
{
    public class UserAuthService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<Employee> _userRepo;

        public UserAuthService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepo = _unitOfWork.GetRepository<Employee>();
        }

        public async Task<UserProfileViewModel> GetEmployeeDetailAsync(int id)
        {
            Employee employee = await _userRepo.GetByIdAsync(id);

            return new UserProfileViewModel
            {
                FullName = employee.FullName,
                Email = employee.Email,
                Title = employee.Title,
                EmployeeId = employee.Id,
                Password = employee.Password
            };
        }

        public async Task<(bool check, string message)> UpdateEmployeeProfileAsync(UserProfileViewModel model)
        {
            Employee employee = await _userRepo.GetByIdAsync(model.EmployeeId);

            if (employee == null)
            {
                return (false, $"User with {model.EmployeeId} was not found");
            }

            employee.FullName = model.FullName;
            employee.Email = model.Email;
            employee.Title = model.Title;
            employee.Password = model.Password;

            return await _unitOfWork.SaveChangesAsync() > 0 ? (true, "updated sucessfully") : (false, "Failed operation");
        }

        public async Task<(bool check, string message, Employee user)> UserLoginAsync(LoginViewModel user)
        {
            Employee employee = await _userRepo.GetSingleByAsync(e => e.Email == user.Email && e.Password == user.Password, tracking: true);

            if (employee == null)
            {
                return (false, $"User not found. Incorrect password and email", null);
            }

            var empOject = new Employee
            {
                Id = employee.Id,
                FullName = employee.FullName,
                Email = employee.Email,
                Title = employee.Title
            };

            return (true, "Logged In succesfully ", empOject);
        }

        public async Task<(bool check, string message)> UserRegisterAsync(UserViewModel model)
        {

            if (model.Password == null && model.Password != model.ConfirmPassword)
            {
                return (false, $"Unable to login. Password does not watch");
            }

            var newUser = new Employee
            {
                FullName = model.FullName,
                Title = model.Title,
                Email = model.Email,
                Password = model.Password
            };

            Employee userCreated = await _userRepo.AddAsync(newUser);
            return userCreated != null ? (true, $"{model.FullName} is created successfully") : (false, $"Operation failed");

        }

    }
}
