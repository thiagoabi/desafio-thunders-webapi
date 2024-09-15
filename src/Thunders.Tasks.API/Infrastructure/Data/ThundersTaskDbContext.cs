using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.API.Domain.Entities;
using Thunders.Tasks.API.Infrastructure.Data.ConfigBuilders;

namespace Thunders.Tasks.API.Infrastructure.Data;

public class ThundersTasksDbContext(DbContextOptions<ThundersTasksDbContext> options) : DbContext(options)
{
    public DbSet<TaskItem> TaskItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TaskItemConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
