using Microsoft.EntityFrameworkCore;
using RoboManager.Application.Contracts;
using RoboManager.Application.Services;
using RoboManager.Infraestructura.Data;
using RoboManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// 🔥 INYECCIÓN DEL DBCONTEXT AQUÍ 🔥
builder.Services.AddDbContext<RoboManagerApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registro de Patrones de Arquitectura
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 🔥 REGISTRO DE AUTOMAPPER (Todos los perfiles) 🔥
builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<RoboManager.Application.MappingProfiles.TeamProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.MemberProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.ProjectProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.ComponentProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.ProjectTaskProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.MeetingProfile>();
});

// 🔥 REGISTRO DE SERVICIOS (Lógica de Negocio) 🔥
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();

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