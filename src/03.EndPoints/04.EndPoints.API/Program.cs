using System.Reflection;
using BaseArchitecture.Infrastructures.InternalMessaging;
using Framework.Domain.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddSingleton<IMessageDispatcher, MessageDispatcher>(
    s => 
        new MessageDispatcher(
            s, 
            typeof(IHandleMessage<>), 
            "Handle", 
            Assembly.Load("BaseArchitecture.ApplicationServices")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();