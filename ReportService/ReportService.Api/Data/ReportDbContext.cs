using Core.BaseDbContext;
using ReportService.Api.Data.Entities;

namespace ReportService.Api.Data
{
    public class ReportDbContext : BaseDbContext
    {
        public DbSet<ReportDetail> ReportDetails { get; set; }
        public DbSet<Report> Reports { get; set; }
        public ReportDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
