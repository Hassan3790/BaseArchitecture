using _04.EndPoints.API.Configs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString =
    builder.Configuration
        .GetValue<string>("connectionString");

builder.Services
    .RegisterDbContext(connectionString!)
    .RegisterMessageDispatcher()
    .RegisterDependency()
    .RegisterHangfire(connectionString!);

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