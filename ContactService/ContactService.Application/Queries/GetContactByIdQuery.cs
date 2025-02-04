﻿using AutoMapper;
using ContactService.Application.Dtos;
using ContactService.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
            var contact = await _dbContext.Contacts.Include(c => c.ContactInfos.Where(ci => !ci.IsDeleted)).FirstOrDefaultAsync(x => x.Id == request.Id && !x.IsDeleted);
            if(contact is null)
                throw new Exception("Contact not found");
            return _mapper.Map<ContactDto>(contact);
        }
    }
}
