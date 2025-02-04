using AutoMapper;
using ContactService.Application.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Queries
{
    public record GetContactWithInfosQuery(Guid Id) : IRequest<ContactDto>;

    public class GetContactWithInfosQueryHandler : IRequestHandler<GetContactWithInfosQuery, ContactDto>
    {
        private readonly IContactServiceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetContactWithInfosQueryHandler(IContactServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ContactDto> Handle(GetContactWithInfosQuery request, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts.AsNoTracking().Include(ci => ci.ContactInfos.Where(x => !x.IsDeleted)).FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);
            if (contact is null)
                throw new Exception("Contact not found");
            return _mapper.Map<ContactDto>(contact);
        }
    }

}
