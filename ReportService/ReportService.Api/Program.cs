using Core.MasstransitConfiguration;
using MassTransit;
using ReportService.Api.EventHandling.IntegrationEvent;
using System.Reflection;
namespace ReportService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddMessageBroker(Assembly.GetExecutingAssembly(), builder.Configuration, (cfg, context) =>
            {
                cfg.ReceiveEndpoint("my_queue", e =>
                {
                    e.ConfigureConsumer<LocationReportRequestedEventHandler>(context);
                });
            });

            var app = builder.Build();

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
