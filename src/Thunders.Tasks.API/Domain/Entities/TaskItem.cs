namespace Thunders.Tasks.API.Domain.Entities;

public class TaskItem : BaseEntity
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateOnly DueDate { get; set; }
}
