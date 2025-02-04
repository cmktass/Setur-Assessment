using AutoMapper;
using ContactService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ContactService.Application.Dtos
{
    public class ContactProfile : Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactInfo, ContactInfoDto>();
        }
    }
    public class ContactDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public List<ContactInfoDto> ContactInfos { get; set; }
    }

    public class ContactInfoDto
    {
        public int Id { get; set; }
        public string Type { get; set; } // "phone", "email", "location"
        public string Value { get; set; }
    }
}
