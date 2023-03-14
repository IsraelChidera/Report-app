using Report.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Report.DAL.Enums.HazardRatingEnum;
using static Report.DAL.Enums.RiskImpactEnum;
using static Report.DAL.Enums.RiskProbabilityEnum;

namespace Report.BLL.Models
{
    public class AddOrUpdateReportVM
    {
        public int EmployeeId { get; set; }
        public int ReportId { get; set; }
        [Required, StringLength(50, ErrorMessage ="character should be more than 3 and not exceed 50", MinimumLength = 3)]
        public string Location { get; set; }
        [Required, StringLength(1000, ErrorMessage = "character should be more than 3 and not exceed 100 characters", MinimumLength = 3)]
        public string HazardDescription { get; set; }
        [Required]
        public string ResourceAtRisk { get; set; }
        public RiskProbability? RiskProbability { get; set; }
        public RiskImpact? RiskImpact { get; set; }
        [Required]
        public string PreventiveMeasure { get; set; }
        public HazardRating? HazardRating { get; set; }
        [Required]
        public string AdditionalInfo { get; set; }
    }
}
