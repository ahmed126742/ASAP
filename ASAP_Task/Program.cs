using System.Reflection;
using ASAP.Application.Common;
using ASAP.Infrastructure;
using ASAP.Infrastructure.BackgroudServices;
using ASAP.Presistance;
using ASAP_Task.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.BackgroundService(builder.Configuration);
builder.Services.AddControllers();
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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseErrorHandler();
app.UseCors();

app.UseAuthorization();

app.UseHangfireDashboard();
app.UseHangfireServer();

var stockBackgroundServicecs = app.Services.GetRequiredService<IStockBackgroundService>();
stockBackgroundServicecs.FetchNotifyAndStoreStockDataAsync();

app.MapControllers();

app.Run();
