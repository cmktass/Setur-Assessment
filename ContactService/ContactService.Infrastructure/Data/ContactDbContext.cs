using ContactService.Application;
using Core.BaseDbContext;

namespace ContactService.Infrastructure.Data
{
    public class ContactDbContext : BaseDbContext, IContactServiceDbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
