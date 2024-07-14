using Ecommerce.Application.Features.Products.Queries;
using Ecommerce.Persistence;
using Microsoft.Extensions.DependencyInjection;
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
