
namespace ContactService.Application
{
    public interface IContactServiceDbContext
    {
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
