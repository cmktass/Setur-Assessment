using AutoMapper;
using ContactService.Application.Dtos;
using ContactService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
                throw new Exception("Contacts not found");
            return _mapper.Map<List<ContactDto>>(contacts);
        }
    }
}
