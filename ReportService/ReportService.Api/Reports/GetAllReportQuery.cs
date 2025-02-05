
namespace ReportService.Api.Reports
{
    public record GetAllReportQuery() : IRequest<List<GetAllReportsResponse>>;

    public class GetAllReportQueryHandler : IRequestHandler<GetAllReportQuery, List<GetAllReportsResponse>>
    {
        private readonly ReportDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllReportQueryHandler(ReportDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<GetAllReportsResponse>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            var repots = await _dbContext.Reports.Include(x => x.ReportDetails).OrderByDescending(x => x.CreatedDate).ToListAsync(cancellationToken: cancellationToken);
            if(repots is null || repots.Count == 0)
                throw new BusinessException("No reports found", HttpStatusCode.NotFound);
            return _mapper.Map<List<GetAllReportsResponse>>(repots);
        }
    }

    public class GetAllReportQueryValidator : AbstractValidator<GetAllReportQuery>
    { 
    }
}
