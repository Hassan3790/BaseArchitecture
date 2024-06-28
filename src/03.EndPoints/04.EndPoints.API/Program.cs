using _04.EndPoints.API;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseArchitecture.Infrastructures.Configs;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString =
    builder
        .Configuration
        .GetValue<string>("connectionString");

builder
    .Services
    .RegisterMessageDispatcher()
    .RegisterHangfire(connectionString!)
    .RegisterDbContext(connectionString!);

builder
    .Host
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder
            .RegisterRepository()
            .RegisterICommandHandler()
            .RegisterMessageHandler();
    });

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseHangfire();

app.Run();