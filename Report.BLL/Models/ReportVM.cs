using Report.DAL.Entities;
using Report.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Report.DAL.Enums.HazardRatingEnum;
using static Report.DAL.Enums.RiskImpactEnum;
using static Report.DAL.Enums.RiskProbabilityEnum;

namespace Report.BLL.Models
{
    public class ReportVM
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public string HazardDescription { get; set; }
        public string ResourceAtRisk { get; set; }
        public RiskProbability RiskProbability { get; set; }
        public RiskImpact RiskImpact { get; set; }
        public string PreventiveMeasure { get; set; }
        public HazardRating HazardRating { get; set; }
        public string AdditionalInfo { get; set; }
        public string EmployeeId { get; set; }
    }
}
