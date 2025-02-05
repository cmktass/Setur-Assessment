
namespace ContactService.Domain.Entities
{
    public class ContactInfo : Entity<int>
    {
        public Guid ContactId { get; set; }
        public Contact Contact { get; set; }
        public string Content { get; set; }
        public ContactType ContactType { get; set; }
        public int ContactTypeId { get; set; }

        public ContactInfo(string content, int contactTypeId)
        {
            Content = content;
            ContactTypeId = contactTypeId;
        }
        public ContactInfo(int id, Guid contactId, string content, int contactTypeId)
        {
            Id = id;
            ContactId = contactId;
            Content = content;
            ContactTypeId = contactTypeId;
        }
    }
}
