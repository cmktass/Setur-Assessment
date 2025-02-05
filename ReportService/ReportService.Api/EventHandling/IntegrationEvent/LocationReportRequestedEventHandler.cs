using Core.Events;
using MassTransit;

namespace ReportService.Api.EventHandling.IntegrationEvent
{
    public class LocationReportRequestedEventHandler : IConsumer<LocationReportRequestedEvent>
    {
        public async Task Consume(ConsumeContext<LocationReportRequestedEvent> context)
        {
            var report = context.Message;
            // Do something with the report
        }
    }
}
