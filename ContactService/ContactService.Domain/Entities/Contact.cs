
namespace ContactService.Domain.Entities
{
    public class Contact : Aggregate<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Company { get; private set; }

        public List<ContactInfo> ContactInfos { get; private set; }

        public Contact(string firstName, string lastName, string company)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Company = company;
        }
        public Contact(Guid id, string firstName, string lastName, string company)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Company = company;
        }

        public static Contact Create(string firstName, string lastName, string company)
        {
            return new Contact (firstName, lastName, company);
        }

        public void AddContactInfo(ContactInfo contactInfo)
        {   ContactInfos = new List<ContactInfo>();
            ContactInfos.Add(contactInfo);
        }
    }
}
