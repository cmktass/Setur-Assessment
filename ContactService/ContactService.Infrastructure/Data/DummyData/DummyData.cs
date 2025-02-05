using ContactService.Domain.Entities;
using MassTransit.MessageData.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.Data.DummyData
{
    public static class DummyContact
    {
        private static readonly List<string> FirstNames = new()
        {
            "Ali", "Ayşe", "Mehmet", "Zeynep", "Mustafa", "Fatma", "Emre", "Elif", "Burak", "Selin"
        };

        private static readonly List<string> LastNames = new()
        {
            "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Öztürk", "Koç", "Aydın", "Eren", "Aksoy"
        };
        private static readonly List<string> Companies = new()
        {
            "TechCorp", "SoftTech", "Global Solutions", "DataWorks", "CyberNet", "InnovateX", "GreenTech", "Cloudify", "NeoSoft", "NextGen"
        };

        private static readonly List<string> PhoneNumbers = new()
        {
            "+90 532 123 45 67", "+90 533 987 65 43", "+90 534 555 12 34",
            "+90 535 678 90 12", "+90 536 432 10 98", "+90 537 111 22 33"
        };

        private static readonly List<string> Emails = new()
        {
            "ali.yilmaz@techcorp.com", "ayse.kaya@softtech.com", "mehmet.demir@global.com",
            "zeynep.sahin@dataworks.com", "mustafa.aydin@cybernet.com", "fatma.erden@innovatix.com"
        };
        private static readonly List<string> Regions = new()
        {
            "Istanbul", "Ankara", "Izmir", "Bursa",
            "Antalya", "Konya", "Adana"
        };
        public static List<Contact> Contats { get; set; } = new List<Contact>();
        public static List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
        public static List<Contact> GetDummyContactData()
        {
            
            for (int i = 0; i < 5; i++)
            {
                var random = new Random();
                var contact = new Contact(FirstNames[random.Next(FirstNames.Count)], LastNames[random.Next(FirstNames.Count)], Companies[random.Next(Companies.Count)]);
                Contats.Add(contact);
            }
            return Contats;
        }

        public static List<ContactInfo> GetDummyContactInfo()
        {
            
            for (int i = 0; i < 5; i++)
            {
                var random = new Random();
                var contactInfo1 = new ContactInfo(PhoneNumbers[random.Next(PhoneNumbers.Count)], (int)ContactTypesEnum.Phone);

                contactInfo1.ContactId = Contats[i].Id;
                var contactInfo2 = new ContactInfo(Emails[random.Next(Emails.Count)], (int)ContactTypesEnum.EMail);

                contactInfo2.ContactId = Contats[i].Id;
                var contactInfo3 = new ContactInfo(Regions[random.Next(Regions.Count)], (int)ContactTypesEnum.Region);
                contactInfo3.ContactId = Contats[i].Id;

                ContactInfos.Add(contactInfo1);
                ContactInfos.Add(contactInfo2);
                ContactInfos.Add(contactInfo3);
            }
            return ContactInfos;
        }
    }
}
