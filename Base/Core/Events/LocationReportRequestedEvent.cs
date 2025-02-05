using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class LocationReportRequestedEvent
    {
        public List<ContactEventDto> ContactEventDtos { get; set; }
    }
    public class ContactEventDto 
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<ContactInfoDto> ContactInfos{ get; set; }
    }
    public class ContactInfoDto
    {
        public int Id { get; set; }
        public string Content { get; set; } // "phone", "email", "location"
        public int ContactTypeId { get; set; }
    }
}
