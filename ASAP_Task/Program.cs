using ASAP.Application.Common;
using ASAP.Infrastructure;
using ASAP.Presistance;
using ASAP_Task.Extensions;
using ASAP_Task.WepApi.Authentication.Extensions;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.BackgroundService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureAuthConnection(builder.Configuration);
builder.Services.ConfigureApiBehavior();
builder.Services.ConfigureCorsPolicy();
builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

//builder.Services.ConfigurePersistence(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseErrorHandler();
app.UseCors();

app.UseAuthorization();

app.UseHangfireDashboard();
app.UseHangfireServer();

app.MapControllers();

app.Run();
