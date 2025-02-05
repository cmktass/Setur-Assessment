using Core.BaseDbContext;
using Microsoft.EntityFrameworkCore;

namespace ReportService.Api.Data
{
    public class ReportServiceDbContext : BaseDbContext
    {
        protected ReportServiceDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
