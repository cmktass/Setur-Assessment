using Core.Events;
using Core.Exception;
using Core.MasstransitConfiguration;
using FluentValidation;

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
                throw new BusinessException("Contacts not found", System.Net.HttpStatusCode.NotFound);
            var locationReportRequestedEvent = PrepareEvent(contats);
            await _publisher.Publish<LocationReportRequestedEvent>(locationReportRequestedEvent);
            _context.Reports.Add(new Report { CreatedDate = DateTime.Now, CreatedId = Guid.NewGuid() });
            return "Report prepering";
        }
        private LocationReportRequestedEvent PrepareEvent(List<Contact> contact)
        {   
            var locationReportRequestedEvent = new LocationReportRequestedEvent();
            locationReportRequestedEvent.ContactEventDtos = _mapper.Map<List<ContactEventDto>>(contact);
            return locationReportRequestedEvent;
        }

        public class PrepareContactInfoReportCommandValidator : AbstractValidator<PrepareContactInfoReportCommand>
        {

        }
    }
}
