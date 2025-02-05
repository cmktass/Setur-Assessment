
using ContactService.Infrastructure.Data.DummyData;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Infrastructure.Data.EntityConfiguration
{
    public class ContactInfoConfiguration : IEntityTypeConfiguration<ContactInfo>
    {
        public void Configure(EntityTypeBuilder<ContactInfo> builder)
        {
            builder.HasKey(ci => ci.Id);
            builder.Property(ci => ci.Content).IsRequired().HasMaxLength(150);
            builder.HasOne(ci => ci.ContactType).WithMany().HasForeignKey(ci => ci.ContactTypeId);
        }
    }
}
