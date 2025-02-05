
using FluentValidation;

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
            var contact = await _dbContext.Contacts.Include(ci => ci.ContactInfos.Where(x => !x.IsDeleted)).FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (contact is null)
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            var contactInfo = contact.ContactInfos.FirstOrDefault(ci => ci.Id == request.InfoId && !ci.IsDeleted);
            if (contactInfo is null)
                throw new BusinessException("Contact info not found", System.Net.HttpStatusCode.NotFound);
            contact.ContactInfos.Remove(contactInfo);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }    
    }

    public class RemoveContactInfoCommandValidator : AbstractValidator<RemoveContactInfoCommand>
    {
        public RemoveContactInfoCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
            RuleFor(x => x.InfoId).NotEmpty().GreaterThan(0).NotNull();
        }
    }
}
