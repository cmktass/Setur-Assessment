﻿
using FluentValidation;

namespace ContactService.Application.Queries
{
    public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto>;

    public class GetContactByIdQueryHandler : IRequestHandler<GetContactByIdQuery, ContactDto>
    {
        private readonly IContactServiceDbContext _dbContext;
        private readonly IMapper _mapper;
        public GetContactByIdQueryHandler(IContactServiceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ContactDto> Handle(GetContactByIdQuery request, CancellationToken cancellationToken)
        {
            var contact = await _dbContext.Contacts.FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if(contact is null)
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            return _mapper.Map<ContactDto>(contact);
        }
    }
    public class GetContactByIdQueryValidator : AbstractValidator<GetContactByIdQuery>
    {
        public GetContactByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
}
