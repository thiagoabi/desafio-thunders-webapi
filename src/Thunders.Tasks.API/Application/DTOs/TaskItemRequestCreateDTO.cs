namespace Thunders.Tasks.API.Application.DTOs;

public record TaskItemRequestCreateDTO(string? Title, string Description, bool IsCompleted, DateOnly DueDate);

