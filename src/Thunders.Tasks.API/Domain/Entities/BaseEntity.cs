namespace Thunders.Tasks.API.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public required DateTime CreatedAt { get; set; }
    public required DateTime? UpdatedAt { get; set; }
}