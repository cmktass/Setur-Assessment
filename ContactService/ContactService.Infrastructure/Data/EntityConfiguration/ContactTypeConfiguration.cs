
namespace ContactService.Infrastructure.Data.EntityConfiguration
{
    public class ContactTypeConfiguration : IEntityTypeConfiguration<ContactType>
    {
        public void Configure(EntityTypeBuilder<ContactType> builder)
        {
           builder.HasKey(ct => ct.Id);
           builder.Property(ct => ct.Name).IsRequired().HasMaxLength(50);
           builder.HasData(
            ContactType.List().Select(s => new { s.Id, s.Name }));
        }
    }
}
