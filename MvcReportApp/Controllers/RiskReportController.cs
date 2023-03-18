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

        public async Task<IActionResult> Details(int id)
        {
            var reportExist = await _reportService.GetReportById(id);
            if (reportExist == null && !(id>0))
            {
                return NotFound();
            }

            return View(reportExist);
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!(id > 0))
            {
                return NotFound();
            }

            var reportExists = await _reportService.GetReportById(id);

            if(reportExists == null)
            {
                return NotFound();
            }

            return View(reportExists);
        }

        public async Task<IActionResult> Alter(ReportVM report)
        {
            if (ModelState.IsValid)
            {
                var (successful, msg) = await _reportService.EditProductAsync(report);

                if (successful)
                {
                    TempData["SuccessMsg"] = msg;

                    return RedirectToAction("Index");
                }
                ViewBag.ErrMsg = msg;
                return View("Edit");
            }

            return View("Edit");
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

        [HttpPost]
        public async Task<IActionResult> Delete(int Id, int EmployeeId)
        {
            var (check, message) = await _reportService.DeleteAsync(EmployeeId, Id);
            if (check)
            {
                TempData["SuccessMsg"] = message;
                return RedirectToAction("Index");
            }
            

            @TempData["ErrorMsg"] = message;
            return View("Details");
        }

    }
}
