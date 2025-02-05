using ContactService.Application;
using ContactService.Infrastructure.Data.DummyData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.Data.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ContactDbContext context, CancellationToken cancellationToken = default)
        {
            if (!context.Contacts.Any())
            {
                await context.Contacts.AddRangeAsync(DummyContact.GetDummyContactData());
                await context.ContactTypes.AddRangeAsync(ContactType.List());
                await context.ContactInfos.AddRangeAsync(DummyContact.GetDummyContactInfo());
                var result = await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
