using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.BLL.Models
{
    public class EmployeeWithReportVM
    {
        public string FullName { get; set; }
        public IEnumerable<ReportVM> Reports { get; set; }
    }
}
