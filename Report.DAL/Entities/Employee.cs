namespace Report.DAL.Entities
{
    public class Employee : BaseEntity
    {
        public string FullName { get; set; }
        public string Title { get; set; }        
        public string Email { get; set; } 
        public string Password { get; set; } 
        public IList<RiskReport> ReportList { get; set; }
    }
}
