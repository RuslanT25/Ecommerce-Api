using Ecommerce.Application.Behaviours;
using Ecommerce.Application.Exceptions;
using Ecommerce.Application.Features.Products.Commands.Create;
using Ecommerce.Application.Features.Products.Queries.GetAll;
using Ecommerce.Persistence;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace Ecommerce.Api;

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
        builder.Services.AddPersistance(builder.Configuration);

        // MediatR'ı ekle ve iki assembly'yi tara
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly(), // Şu an çalışmakta olan assembly
            typeof(GetAllProductsQueryHandler).Assembly // GetAllProductsQueryHandler türünün bulunduğu assembly
        ));

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddTransient<ExceptionMiddeware>();

        builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidation).Assembly);

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionMiddeware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
