using ContactService.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Commands
{
    public record CreateContactInfoCommand(Guid PersonId, int ContactTypeId, string Value) : IRequest<int>;

    public class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommand, int>
    {
        private readonly IContactServiceDbContext _dbContext;
        public CreateContactInfoCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == request.PersonId && !c.IsDeleted);
            if(contact is null)
                throw new Exception("Contact not found");
           contact.ContactInfos.Add(new ContactInfo(request.Value, request.ContactTypeId));
           return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
