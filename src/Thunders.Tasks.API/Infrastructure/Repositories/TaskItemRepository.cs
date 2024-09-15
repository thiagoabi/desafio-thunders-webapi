using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.API.Domain.Entities;
using Thunders.Tasks.API.Domain.Services;
using Thunders.Tasks.API.Infrastructure.Data;

namespace Thunders.Tasks.API.Infrastructure.Repositories;

public class TaskItemRepository(ThundersTasksDbContext context) : ITaskItemRepository
{
    private readonly ThundersTasksDbContext _context = context;

    public async Task<TaskItem?> GetByIdAsync(Guid id)
    {
        return await _context.TaskItems.FindAsync(id);
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.TaskItems.ToListAsync();
    }

    public async Task<TaskItem> AddAsync(TaskItem taskItem)
    {
        taskItem.CreatedAt = DateTime.UtcNow;

        await _context.TaskItems.AddAsync(taskItem);
        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem taskItem)
    {
        taskItem.UpdatedAt = DateTime.UtcNow;

        _context.TaskItems.Update(taskItem);
        await _context.SaveChangesAsync();

        return taskItem;
    }

    public async Task DeleteAsync(Guid id)
    {
        var taskItem = await GetByIdAsync(id);

        if (taskItem != null)
        {
            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _context.TaskItems.AnyAsync(t => t.Id == id);
    }
}
