using static Report.DAL.Enums.HazardRatingEnum;
using static Report.DAL.Enums.RiskImpactEnum;
using static Report.DAL.Enums.RiskProbabilityEnum;

namespace Report.DAL.Entities
{
    public class RiskReport : BaseEntity
    {
        public string Location { get; set; }
        public string HazardDescription { get; set; }
        public string ResourceAtRisk { get; set; }
        public RiskProbability RiskProbability { get; set; } = RiskProbability.Low;
        public RiskImpact RiskImpact { get; set; } = RiskImpact.low;
        public string PreventiveMeasure { get; set; }
        public HazardRating HazardRating { get; set; }
        public string AdditionalInfo { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }

}