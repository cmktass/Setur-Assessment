using Carter;
using ReportService.Api.Reports;        

namespace ReportService.Api.Reports
{
    public record GetAllReportsResponse(Guid Id, string ReportState, List<GetAllReportResponse> ReportDetails);
    public record GetAllReportResponse(string Region, int RegisteredContactCount, int RegisteredPhoneNumberCount);
    public record GetReportByIdResponse(string Region, int RegisteredContactCount, int RegisteredPhoneNumberCount);
    public class ReportEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/report/getall", async (IMediator _mediator) =>
            {
                var response = await _mediator.Send(new GetAllReportQuery());
                return Results.Ok(response);
            })
            .WithName("GetAllReport")
            .Produces<GetAllReportResponse>(StatusCodes.Status200OK)
            .WithDescription("Get All Report");

            app.MapGet("/report/getbyReportId/{reportId}", async (Guid reportId, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetReportByIdQuery(reportId));
                return Results.Ok(response);
            })
            .WithName("GetByReportId")
            .Produces<GetReportByIdResponse>(StatusCodes.Status200OK)
            .WithDescription("Get Report by ReportId");
        }
    }
}
