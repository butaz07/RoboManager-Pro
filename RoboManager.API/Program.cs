using Microsoft.EntityFrameworkCore;
using RoboManager.Application.Contracts;
using RoboManager.Application.Services;
using RoboManager.Infraestructura.Data;
using RoboManager.Infrastructure.Repositories;
using Scalar.AspNetCore; 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();


builder.Services.AddDbContext<RoboManagerApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<RoboManager.Application.MappingProfiles.TeamProfile>();
    config.AddProfile<RoboManager.Application.MappingProfiles.AppProfiles>();
});


builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IMemberService, MemberService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IComponentService, ComponentService>();
builder.Services.AddScoped<IProjectTaskService, ProjectTaskService>();
builder.Services.AddScoped<IMeetingService, MeetingService>();


builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 

    
    app.UseSwaggerUI(options =>
    {
        
        options.SwaggerEndpoint("/openapi/v1.json", "RoboManager API v1");
        options.RoutePrefix = "swagger"; 
    });

    app.MapScalarApiReference(); 
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();