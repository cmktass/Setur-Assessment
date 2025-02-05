
using FluentValidation;

namespace ContactService.Application.Queries
{
    public record GetContactsQuery() : IRequest<List<ContactDto>>;
    public class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
    {
        private readonly IContactServiceDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetContactsQueryHandler(IContactServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _dbContext.Contacts.Where(c => !c.IsDeleted).ToListAsync(cancellationToken);
            if(contacts is null)
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            return _mapper.Map<List<ContactDto>>(contacts);
        }
    }

    public class GetContactsQueryValidator : AbstractValidator<GetContactsQuery>
    {
        
    }
}
