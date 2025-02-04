using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Commands
{
    public record DeleteContactCommand(Guid id) : IRequest<int>;

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, int>
    {
        private readonly IContactServiceDbContext _dbContext;
        public DeleteContactCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts.Include(c => c.ContactInfos).FirstOrDefaultAsync(x => x.Id == request.id && !x.IsDeleted);
            if (contact is null)
                throw new Exception("Contact not found");
            _dbContext.Contacts.Remove(contact);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
