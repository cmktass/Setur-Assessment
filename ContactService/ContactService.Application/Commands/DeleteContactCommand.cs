
using FluentValidation;

namespace ContactService.Application.Commands
{
    public record DeleteContactCommand(Guid Id) : IRequest<int>;

    public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, int>
    {
        private readonly IContactServiceDbContext _dbContext;
        public DeleteContactCommandHandler(IContactServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts.Include(c => c.ContactInfos).FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if (contact is null)
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            _dbContext.Contacts.Remove(contact);
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public class DeleteContactCommandValidator : AbstractValidator<DeleteContactCommand>
    {
        public DeleteContactCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().NotNull();
        }
    }

}
