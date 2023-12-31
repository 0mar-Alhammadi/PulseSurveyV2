using Microsoft.EntityFrameworkCore;
using PulseSurveyV2.Models;
using PulseSurveyV2.Interfaces;
using Threenine.Data.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<UnifiedContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))
    .AddUnitOfWork<UnifiedContext>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
