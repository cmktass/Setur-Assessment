using Core.CoreDomain;

namespace ContactService.Domain.Entities
{
    public class ContactInfo : Entity<int>
    {
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public string Content { get; set; }
        public ContactType ContactType { get; set; }
        public int ContactTypeId { get; set; }
    }
}
