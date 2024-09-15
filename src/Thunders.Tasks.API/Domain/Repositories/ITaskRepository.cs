using Thunders.Tasks.API.Domain.Entities;

namespace Thunders.Tasks.API.Domain.Services;

public interface ITaskItemRepository
{
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskItem>> GetAllAsync();

    Task<TaskItem> AddAsync(TaskItem taskItem);
    Task<TaskItem> UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(Guid id);

    Task<bool> ExistsAsync(Guid id);
}
