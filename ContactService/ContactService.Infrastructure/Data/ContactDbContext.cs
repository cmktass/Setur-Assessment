using ContactService.Domain.Entities;
using Core.BaseDbContext;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Data
{
    public class ContactDbContext : BaseDbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfos { get; set; }
        public DbSet<ContactType> ContactTypes { get; set; }
    }
}
