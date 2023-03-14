using Microsoft.AspNetCore.Mvc;
using Report.BLL.Interfaces;
using Report.BLL.Models;

namespace MvcReportApp.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class RiskReportController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly IReportListService _reportService;

        public RiskReportController(IEmployeeService employeeService, IReportListService reportService)
        {
            _employeeService = employeeService;
            _reportService = reportService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _employeeService.GetEmployeeWithReportAsync();
            return View(model);
        }

        public IActionResult Create()
        {
            return View(new AddOrUpdateReportVM());
        }

        [HttpPost]
        public async Task<IActionResult> Save(AddOrUpdateReportVM model)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _reportService.AddOrUpdateAsync(model);

                if (successful)
                {
                    TempData["SuccessMessage"] = msg;
                    return RedirectToAction("Index");
                }

                ViewBag.ErrMsg = msg;
                return View("Create");
            }

            return View("Create");
        }

    }
}
