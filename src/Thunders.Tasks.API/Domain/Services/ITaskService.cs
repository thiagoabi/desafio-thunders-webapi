using OneOf;
using Thunders.Tasks.API.Application.DTOs;

namespace Thunders.Tasks.API.Domain.Services;

public interface ITaskItemService
{
    Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> GetTaskItemByIdAsync(Guid id);
    Task<OneOf<IEnumerable<TaskItemResponseDTO>, IEnumerable<AppError>>> GetAllTaskItemsAsync();

    Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> CreateTaskItemAsync(TaskItemRequestCreateDTO taskItem);
    Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> UpdateTaskItemAsync(Guid id, TaskItemRequestUpdateDTO taskItem);

    Task<OneOf<bool, IEnumerable<AppError>>> DeleteTaskItemAsync(Guid id);
}
