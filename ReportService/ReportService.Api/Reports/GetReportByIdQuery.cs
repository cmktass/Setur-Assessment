
namespace ReportService.Api.Reports
{
    public record GetReportByIdQuery(Guid ReportId) : IRequest<GetAllReportsResponse>;

    public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, GetAllReportsResponse>
    {
        private readonly ReportDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReportByIdQueryHandler(ReportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<GetAllReportsResponse> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
            var report = await _dbContext.Reports.Include(x => x.ReportDetails.Where(y => !y.IsDeleted)).FirstOrDefaultAsync(x => x.Id == request.ReportId && !x.IsDeleted, cancellationToken: cancellationToken);
            if (report is null)
                throw new BusinessException("Report not found", HttpStatusCode.NotFound);
            return _mapper.Map<GetAllReportsResponse>(report);
        }
    }

    public class GetReportByIdQueryValidator : AbstractValidator<GetReportByIdQuery>
    {
        public GetReportByIdQueryValidator()
        {
            RuleFor(x => x.ReportId).NotEmpty();
        }
    }
}
