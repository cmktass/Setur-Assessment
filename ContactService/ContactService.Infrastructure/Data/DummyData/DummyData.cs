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
        "Ali", "Ayşe", "Mehmet", "Zeynep", "Mustafa", "Fatma", "Emre", "Elif", "Burak", "Selin",
        "Ahmet", "Hasan", "Hüseyin", "İbrahim", "Osman", "Kemal", "Ömer", "Süleyman", "Barış",
        "Serkan", "Murat", "Hakan", "Ramazan", "Sami", "Onur", "Taha", "Faruk", "Özgür", "Sinan",
        "Levent", "Uğur", "Kaan", "Can", "Deniz", "Bora", "Eren", "Furkan", "Mete", "Rıza",
        "Merve", "Derya", "Esra", "Gizem", "Ceyda", "Büşra", "İrem", "Seda", "Aylin", "Gamze",
        "Aslı", "Ceren", "Neslihan", "Özge", "Sevgi", "Yasemin", "Sinem", "Hande", "Gül", "Cem"
        };

        private static readonly List<string> LastNames = new()
        {
            "Yılmaz", "Kaya", "Demir", "Çelik", "Şahin", "Öztürk", "Koç", "Aydın", "Eren", "Aksoy",
            "Arslan", "Polat", "Güneş", "Bozkurt", "Keskin", "Kurt", "Aslan", "Yıldırım", "Can", "Tan",
            "Erdoğan", "Özkan", "Avcı", "Doğan", "Tekin", "Bulut", "Gül", "Yüksel", "Çetin", "Aktaş"
        };
        private static readonly List<string> Companies = new()
        {
            "TechCorp", "SoftTech", "Global Solutions", "DataWorks", "CyberNet", "InnovateX", "GreenTech", "Cloudify", "NeoSoft", "NextGen", "Setur"
        };

        private static readonly List<string> PhoneNumbers = new()
        {
            "+90 532 123 45 67", "+90 533 987 65 43", "+90 534 555 12 34",
            "+90 535 678 90 12", "+90 536 432 10 98", "+90 537 111 22 33",
            "+90 538 246 80 50", "+90 539 369 74 85", "+90 530 852 63 14",
            "+90 531 963 25 48", "+90 532 741 36 92", "+90 533 654 82 37",
            "+90 534 159 47 26", "+90 535 789 65 12", "+90 536 258 94 63",
            "+90 537 753 14 68", "+90 538 135 79 46", "+90 539 987 32 15",
            "+90 530 456 28 93", "+90 531 678 53 29", "+90 532 892 41 67",
            "+90 533 369 85 24", "+90 534 987 65 31", "+90 535 543 21 89",
            "+90 536 753 96 42"
        };

        private static readonly List<string> Regions = new()
        {
            "Istanbul", "Ankara", "Izmir", "Bursa",
            "Antalya", "Konya", "Adana", "Mersin",
            "Trabzon", "Samsun", "Erzurum", "Diyarbakir"
        };
        public static List<Contact> Contats { get; set; } = new List<Contact>();
        public static List<ContactInfo> ContactInfos { get; set; } = new List<ContactInfo>();
        public static List<Contact> GetDummyContactData()
        {
            for (int i = 0; i < 50; i++)
            {
                var random = new Random();
                var contact = new Contact(FirstNames[random.Next(FirstNames.Count)], LastNames[random.Next(LastNames.Count)], Companies[random.Next(Companies.Count)]);
                Contats.Add(contact);
            }
            return Contats;
        }

        public static List<ContactInfo> GetDummyContactInfo()
        {
            for (int i = 0; i < 50; i++)
            {
                var random = new Random();
                var contactInfo1 = new ContactInfo(PhoneNumbers[random.Next(PhoneNumbers.Count)], (int)ContactTypesEnum.Phone);

                contactInfo1.ContactId = Contats[i].Id;
                var contactInfo2 = new ContactInfo((Contats[i].FirstName + "." + Contats[i].LastName + "@gmail.com"), (int)ContactTypesEnum.EMail);

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
