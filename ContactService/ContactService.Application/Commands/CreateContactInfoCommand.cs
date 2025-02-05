
using Core.Exception;
using FluentValidation;

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
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            contact.AddContactInfo(new ContactInfo(request.Value, request.ContactTypeId));
            return await _dbContext.SaveChangesAsync(cancellationToken);
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
