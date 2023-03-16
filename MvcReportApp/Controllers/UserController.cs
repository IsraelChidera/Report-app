using Microsoft.AspNetCore.Mvc;
using Report.BLL.Implementation;
using Report.BLL.Interfaces;
using Report.BLL.Models;

namespace MvcReportApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        public UserController(IUserService userService, IEmployeeService employeeService)
        {
            _userService = userService;
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.GetEmployeeWithReportAsync();
            return View(model);            
        }

        public IActionResult Register()
        {
            return View(new UserViewModel());
        }

        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> New(UserViewModel user)
        {
            if (ModelState.IsValid)
            {


                var (check, msg) = await _userService.UserRegisterAsync(user);

                if (check)
                {
                    TempData["SuccessMsg"] = msg;
                    return RedirectToAction("Login");

                }

                ViewBag.ErrMsg = msg;
                return View("Register");

            }

            return View("Register");



        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg, user) = await _userService.UserLoginAsync(model);

                if (successful)
                {
                    ViewBag.UserLoggedID = user.Id;
                    ViewBag.UserLoggedName = user.FullName;
                    TempData["successMsg"] = msg;
                    return RedirectToAction("Index");
                }

                ViewBag.ErrMsg = msg;
                return View("Login");

            }
            return View("Login");
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!(id > 0))
            {
                return NotFound();
            }

            var employeeObj = await _userService.GetEmployeeDetailAsync(id);

            if (employeeObj == null)
            {
                return NotFound();
            }

            return View(employeeObj);
        }


        public async Task<IActionResult> UpdateProfile(UserProfileViewModel model)
        {
            var (check, msg) = await _userService.UpdateEmployeeProfileAsync(model);
            if (check)
            {
                ViewBag.SuccMsg = msg;
                return View("Edit", model);
            }

            ViewBag.ErrMsg = msg;
            return View("Edit", model);
        }

    }
}
