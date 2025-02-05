
using Core.Exception;
using FluentValidation;

namespace ContactService.Application.Commands
{
    public record CreateContactInfoCommand(Guid PersonId, int ContactTypeId, string Value) : IRequest<Guid>;

    public class CreateContactInfoCommandHandler : IRequestHandler<CreateContactInfoCommand, Guid>
    {
        private readonly IContactServiceDbContext _dbContext;
        public CreateContactInfoCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Guid> Handle(CreateContactInfoCommand request, CancellationToken cancellationToken)
        {
            var contact = _dbContext.Contacts.FirstOrDefault(c => c.Id == request.PersonId && !c.IsDeleted);
            if(contact is null)
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            contact.AddContactInfo(new ContactInfo(request.Value, request.ContactTypeId));
            await _dbContext.SaveChangesAsync(cancellationToken);
            return contact.Id;
        }
    }
    public class CreateContactInfoCommandValidator : AbstractValidator<CreateContactInfoCommand>
    {
        public CreateContactInfoCommandValidator()
        {
            RuleFor(x => x.PersonId).NotEmpty().NotNull();
            RuleFor(x => x.ContactTypeId).NotEmpty().GreaterThan(0).NotNull();
            RuleFor(x => x.Value).NotNull().MaximumLength(50);
        }
    }

}
