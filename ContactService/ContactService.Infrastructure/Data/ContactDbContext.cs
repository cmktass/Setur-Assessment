using ContactService.Application;
using ContactService.Infrastructure.Data.DummyData;
using Core.BaseDbContext;
using System.Reflection;

namespace ContactService.Infrastructure.Data
{
    public class ContactDbContext : BaseDbContext, IContactServiceDbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
    }
}
