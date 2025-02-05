
using ContactService.Application;
using ContactService.Infrastructure;
using ContactService.Infrastructure.Data;
using ContactService.Infrastructure.Data.DataSeeder;
using Core.MasstransitConfiguration;
using MapsterMapper;
using MassTransit;
using System;
using System.Reflection;

namespace ContactService.Api
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<Publisher>();
            builder.Services.AddInfrastructureServices(builder.Configuration).AddApplicationServices();
            builder.Services.AddMessageBroker(Assembly.GetExecutingAssembly(), builder.Configuration);
            builder.Services.AddSingleton<IMapper, Mapper>();
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ContactDbContext>();
                await DataSeeder.SeedAsync(dbContext);
            }

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
