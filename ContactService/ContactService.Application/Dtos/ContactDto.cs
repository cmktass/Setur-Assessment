using ContactService.Domain.Entities;
using System;

namespace ContactService.Application.Dtos
{
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
        public string Content { get; set; }
        public int ContactTypeId { get; set; }
    }
}
