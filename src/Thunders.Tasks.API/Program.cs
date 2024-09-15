using System.Text.Json.Serialization;
using Thunders.Tasks.API.Application.DTOs;
using Thunders.Tasks.API.Application.Enumerators;
using Thunders.Tasks.API.Domain.Services;
using Thunders.Tasks.API.Extensions.IoC;
using Thunders.Tasks.API.Infrastructure.Data;

namespace Thunders.Tasks.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
        });

        builder.Services.AddDatabaseServices(builder);
        builder.Services.AddAutoMapperServices();
        builder.Services.AddValidationServices();
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ThundersTasksDbContext>();
            dbContext.Database.EnsureCreated();
        }

        #region Minimal APIs

        // 1. Get All TaskItems
        app.MapGet("/api/taskitems", async (ITaskItemService taskItemService) =>
        {
            var result = await taskItemService.GetAllTaskItemsAsync();

            return result.Match(
                taskItems => Results.Ok(taskItems),
                error => Results.NotFound(error.ToResponse())
            );
        });

        // 2. Get TaskItem by ID
        app.MapGet("/api/taskitems/{id:guid}", async (Guid id, ITaskItemService taskItemService) =>
        {
            var result = await taskItemService.GetTaskItemByIdAsync(id);

            return result.Match(
                taskItem => Results.Ok(taskItem),
                error => Results.NotFound(error.ToResponse())
            );
        });

        // 3. Create TaskItem
        app.MapPost("/api/taskitems", async (TaskItemRequestCreateDTO taskItem, ITaskItemService taskItemService) =>
        {
            var result = await taskItemService.CreateTaskItemAsync(taskItem);

            return result.Match(
                createdItem => Results.Created($"/api/taskitems/{createdItem.Id}", createdItem),
                error => Results.BadRequest(error.ToResponse())
            );
        });

        // 4. Update TaskItem
        app.MapPut("/api/taskitems/{id:guid}", async (Guid id, TaskItemRequestUpdateDTO taskItem, ITaskItemService taskItemService) =>
        {
            if (id != taskItem.Id)
            {
                return Results.BadRequest(new[] { new AppError("Id inválido.", EnumErrorType.Validation) }.ToResponse());
            }

            var result = await taskItemService.UpdateTaskItemAsync(id, taskItem);

            return result.Match(
                updatedItem => Results.Ok(updatedItem),
                error => Results.BadRequest(error.ToResponse())
            );
        });

        // 5. Delete TaskItem
        app.MapDelete("/api/taskitems/{id:guid}", async (Guid id, ITaskItemService taskItemService) =>
        {
            var result = await taskItemService.DeleteTaskItemAsync(id);

            return result.Match(
                success => Results.Ok(),
                error => Results.BadRequest(error.ToResponse())
            );
        });

        #endregion

        app.Run();
    }
}


[JsonSerializable(typeof(TaskItemRequestCreateDTO[]))]
[JsonSerializable(typeof(TaskItemRequestUpdateDTO[]))]
[JsonSerializable(typeof(TaskItemResponseDTO))]
[JsonSerializable(typeof(IEnumerable<TaskItemResponseDTO>))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
