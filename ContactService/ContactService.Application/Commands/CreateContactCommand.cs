
namespace ContactService.Application.Commands
{
    public record CreateContactCommand(string FirstName, string LastName, string Company) : IRequest<int>;

    public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, int>
    {
        private readonly IContactServiceDbContext _dbContext;
        public CreateContactCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var contact = Contact.Create(request.FirstName, request.LastName, request.Company);
            _dbContext.Contacts.Add(contact);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
