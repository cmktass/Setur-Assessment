using Core.Exception;
using Core.MasstransitConfiguration;
using MassTransit;
using ReportService.Api.EventHandling.IntegrationEvent;
using System.Reflection;
using Core.Web;
using Core.Validation;
using Carter;
using ReportService.Api.Reports;
using ReportService.Api.PrepareReportBackgroundService;
namespace ReportService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Publisher>();
            string connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
            builder.Services.AddSingleton<IMapper, Mapper>();
            builder.Services.AddDbContext<ReportDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            builder.Services.AddMessageBroker(Assembly.GetExecutingAssembly(), builder.Configuration, (cfg, context) =>
            {
                cfg.ReceiveEndpoint("my_queue", e =>
                {
                    e.ConfigureConsumer<LocationReportRequestedEventHandler>(context);
                });
            });
            builder.Services.AddCarter();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddValidatorsFromAssemblyContaining<GetAllReportQueryValidator>();
            builder.Services.AddHostedService<PrepareReportService>();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ReportDbContext>();
                dbContext.Database.Migrate();
            }
            app.UseMiddleware<ExceptionMiddleware>(); // Hata yönetimi için kullanýlan middleware
            app.UseCorrelationId();
            app.MapCarter();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
