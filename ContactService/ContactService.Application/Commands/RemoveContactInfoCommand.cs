using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Application.Commands
{
    public record RemoveContactInfoCommand(Guid Id, int InfoId) : IRequest;

    public class RemoveContactInfoCommandHandler : IRequestHandler<RemoveContactInfoCommand>
    {
        private readonly IContactServiceDbContext _dbContext;
        public RemoveContactInfoCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Handle(RemoveContactInfoCommand request, CancellationToken cancellationToken)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == request.Id && !c.IsDeleted);
            if (contact is null)
                throw new Exception("Contact not found");
            var contactInfo = contact.ContactInfos.FirstOrDefault(ci => ci.Id == request.InfoId && !ci.IsDeleted);
            if (contactInfo is null)
                throw new Exception("Contact info not found");
            contact.ContactInfos.Remove(contactInfo);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }    
    }
}
