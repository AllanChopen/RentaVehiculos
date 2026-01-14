using Microsoft.EntityFrameworkCore;
using Renta.Data;
using Renta.Models;

var builder = WebApplication.CreateBuilder(args);

// Application configuration
// Configure Postgres connection (Neon provided)
var connectionString = "Host=ep-dawn-mouse-ahfrf22a-pooler.c-3.us-east-1.aws.neon.tech;Username=neondb_owner;Password=npg_YowN5OIk4zaW;Database=neondb;Port=5432;Ssl Mode=Require;Trust Server Certificate=true;";
builder.Services.AddDbContext<ApplicationDbContext>(opts => opts.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// HTTP pipeline configuration
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
