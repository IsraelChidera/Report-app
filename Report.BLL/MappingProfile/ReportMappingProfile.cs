using AutoMapper;
using Report.BLL.Models;
using Report.DAL.Entities;

namespace Report.BLL.MappingProfile
{
    public class ReportMappingProfile : Profile
    {
        public ReportMappingProfile()
        {
            CreateMap<AddOrUpdateReportVM, RiskReport>();
        }
    }
}
