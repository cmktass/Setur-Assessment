using Core.Core.Model;

namespace ReportService.Api.Data.Entities
{
    public class ReportDetail : Entity<int>
    {
        public string Region { get; set; }
        public int RegisteredContactCount { get; set; }
        public int RegisteredPhoneNumberCount { get; set; }
        public Guid ReportId { get; set; }
        public Report Report { get; set; }
    }
}
