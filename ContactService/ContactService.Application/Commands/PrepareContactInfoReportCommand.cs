using Core.Events;
using Core.MasstransitConfiguration;

namespace ContactService.Application.Commands
{
    public record PrepareContactInfoReportCommand : IRequest<string>;

    public record PrepareContactInfoReportCommandHandler : IRequestHandler<PrepareContactInfoReportCommand, string>
    {
        private readonly IContactServiceDbContext _context;
        private readonly Publisher _publisher;
        private readonly IMapper _mapper;

        public PrepareContactInfoReportCommandHandler(IContactServiceDbContext context, Publisher publisher, IMapper mapper)
        {
            _context = context;
            _publisher = publisher;
            _mapper = mapper;
        }
        public async Task<string> Handle(PrepareContactInfoReportCommand request, CancellationToken cancellationToken)
        {
            var contats = await _context.Contacts.Include(c => c.ContactInfos.Where(x => !x.IsDeleted)).Where(x => !x.IsDeleted).ToListAsync();
            if(contats is null)
                throw new Exception("Contacts not found");
            var event1 = new LocationReportRequestedEvent();
            event1.ContactEventDtos = PrepareEvent(contats);
            await _publisher.Publish<LocationReportRequestedEvent>(event1);
            _context.Reports.Add(new Report { CreatedDate = DateTime.Now, CreatedId = Guid.NewGuid() });
            return "Report prepering";
        }
        private List<ContactEventDto> PrepareEvent(List<Contact> contact)
        {
            return _mapper.Map<List<ContactEventDto>>(contact);
        }
    }
}
