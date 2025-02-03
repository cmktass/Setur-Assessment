using Core.CoreDomain;

namespace ContactService.Domain.Entities
{
    public class ContactType : Entity<int>
    {
        public string Name { get; private set; }

        private ContactType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static readonly ContactType Phone = new((int)ContactTypesEnum.Phone, ContactTypesEnum.Phone.ToString());
        public static readonly ContactType EMail = new((int)ContactTypesEnum.EMail, ContactTypesEnum.EMail.ToString());
        public static readonly ContactType Region = new((int)ContactTypesEnum.Region, ContactTypesEnum.Region.ToString());

        public static IEnumerable<ContactType> List() =>
        new[] { Phone, EMail, Region };

        public static ContactType FromId(int id) =>
            List().SingleOrDefault(s => s.Id == id) ?? throw new ArgumentException($"Invalid id {id}");


        public static ContactType FromEnum(ContactTypesEnum statusEnum) =>
            FromId((int)statusEnum);
    }
    public enum ContactTypesEnum
    {
        Phone = 1,
        EMail = 2,
        Region = 3
    }
}
