namespace ContactService.Infrastructure.Data.EntityConfiguration
{
    public class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(rc => rc.Id);
            builder.Property(rc => rc.FirstName).IsRequired().HasMaxLength(50);
            builder.Property(rc => rc.LastName).IsRequired().HasMaxLength(50);
            builder.Property(rc => rc.Company).HasMaxLength(50);

            builder.HasMany(rc => rc.ContactInfos).WithOne(ci => ci.Contact).HasForeignKey(ci => ci.ContactId);
        }
    }
}
