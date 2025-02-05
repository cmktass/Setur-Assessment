using Core.Events;
using MassTransit;

namespace ReportService.Api.EventHandling.IntegrationEvent
{
    public class LocationReportRequestedEventHandler : IConsumer<LocationReportRequestedEvent>
    {
        public enum ContactTypesEnum
        {
            Phone = 1,
            EMail = 2,
            Region = 3
        }
        private readonly ReportDbContext _dbContext;
        public LocationReportRequestedEventHandler(ReportDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Consume(ConsumeContext<LocationReportRequestedEvent> context)
        {
            var data = context.Message;
            var report = new ReportService.Api.Data.Entities.Report();
            var locationReports = data.ContactEventDtos
            .Where(contact => contact.ContactInfos.Any(info => info.ContactTypeId == (int)ContactTypesEnum.Region))
            .Select(contact => new
            {
                Location = contact.ContactInfos.First(info => info.ContactTypeId == (int)ContactTypesEnum.Region).Content,
                PhoneCount = contact.ContactInfos.Count(info => info.ContactTypeId == (int)ContactTypesEnum.Phone)
            })
            .GroupBy(x => x.Location)
            .Select(g => new ReportService.Api.Data.Entities.ReportDetail
            {
                Region = g.Key,
                RegisteredContactCount = g.Count(),
                RegisteredPhoneNumberCount = g.Sum(x => x.PhoneCount)
            })
            .ToList();
            report.ReportDetails = locationReports;
            await _dbContext.AddAsync(report);
            await _dbContext.SaveChangesAsync();
        }
    }
}
