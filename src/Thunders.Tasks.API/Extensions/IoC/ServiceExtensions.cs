using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.API.Application.Mappings;
using Thunders.Tasks.API.Application.Services;
using Thunders.Tasks.API.Application.Validations;
using Thunders.Tasks.API.Domain.Services;
using Thunders.Tasks.API.Infrastructure.Data;
using Thunders.Tasks.API.Infrastructure.Repositories;

namespace Thunders.Tasks.API.Extensions.IoC;

public static class ServiceExtensions
{
    public static void AddDatabaseServices(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ThundersTasksDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    public static void AddAutoMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddAutoMapper(typeof(TaskItemProfile));
    }

    public static void AddValidationServices(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<TaskItemValidator>();
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskItemService, TaskItemService>();
    }

    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskItemRepository, TaskItemRepository>();
    }
}
