using Microsoft.AspNetCore.Diagnostics;
using WebAPI.ServiceExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureDatabaseContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureLoggingService(builder.Configuration);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Configuring global exception handler
app.ConfigureExceptionHandler(app.Environment);

app.UseHttpsRedirection();

app.MapControllers();

//Migration Process, maybe app terminate there
app.MigrateDataBase();

app.Run();