using Core.Core.Model;

namespace ReportService.Api.Data.Entities
{
    public class Report : Entity<Guid>
    {
        public string ReportState { get; set; }
        public List<ReportDetail> ReportDetails { get; set; }

        public Report()
        {
            Id = new Guid();
            ReportState = "Preparing";
        }
    }
}
