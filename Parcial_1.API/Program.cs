using Microsoft.Extensions.Configuration;
using Parcial_1.Infraestructure.DataBase;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Parcial_1.Application.Services;
using Parcial_1.Shared;
using Parcial_1.Application.Interfaces;
using Parcial_1.Infraestructure.Interfaces;
using Parcial_1.Infraestructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Parcial1Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<PrintRequestService>();
    x.AddConsumer<PrintResponseService>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq://localhost", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.Send<PrintRequestDto>(s =>
        {
            s.UseRoutingKeyFormatter(context => "print-requests");
        });

        cfg.Message<PrintRequestDto>(m => m.SetEntityName("print-requests"));

        cfg.ReceiveEndpoint("print-requests", e =>
        {
            e.UseMessageRetry(r => r.Immediate(5));
            e.ConfigureConsumeTopology = false;
            e.PurgeOnStartup = true;
            e.SetQueueArgument("x-max-priority", 10);
            e.ConfigureConsumer<PrintRequestService>(context);
        });

        cfg.ReceiveEndpoint("print-responses", e =>
        {
            e.ConfigureConsumer<PrintResponseService>(context);
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddScoped<IPrintingService, PrintingService>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<Parcial1Context>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while creating the database." + ex.Message);
    }
}


app.Run();
