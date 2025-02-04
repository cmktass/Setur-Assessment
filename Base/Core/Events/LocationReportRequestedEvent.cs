using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class LocationReportRequestedEvent
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<ContactInfo> ContactInfos { get; set; }
    }
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Type { get; set; } // "phone", "email", "location"
        public string Value { get; set; }
    }
}
