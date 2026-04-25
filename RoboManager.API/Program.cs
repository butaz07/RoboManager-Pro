using Microsoft.EntityFrameworkCore;
using RoboManager.Application.Contracts;
using RoboManager.Application.Services;
using RoboManager.Infraestructura.Data;
using RoboManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔥 INYECCIÓN DEL DBCONTEXT AQUÍ (Justo antes del Build) 🔥
builder.Services.AddDbContext<RoboManagerApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(RoboManager.Infrastructure.Repositories.GenericRepository<>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Registrar AutoMapper escaneando todos los perfiles en el dominio actual
// Registrar AutoMapper manualmente con el perfil específico
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<RoboManager.Application.MappingProfiles.TeamProfile>();
});
// Registrar el TeamService
builder.Services.AddScoped<ITeamService, TeamService>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();