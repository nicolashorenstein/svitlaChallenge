using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using svitlaChallenge.Application.Persons.Queries;
using svitlaChallenge.Application.Validators.Persons;
using svitlaChallenge.Domain.Interfaces;
using svitlaChallenge.Infrastructure.Persistence;
using svitlaChallenge.Infrastructure.Services;

namespace svitlaChallenge.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("logs/myAppLogs.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            builder.Host.UseSerilog();

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionDatabase"));
            });

            builder.Services.AddScoped<IPersonService, PersonService>();

            builder.Services.AddMediatR(typeof(AddPersonHandler).Assembly);
            builder.Services.AddValidatorsFromAssemblyContaining<AddPersonValidator>();


            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin();
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

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}

