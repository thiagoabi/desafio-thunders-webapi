namespace Thunders.Tasks.API.Application.DTOs;

public record TaskItemRequestUpdateDTO(Guid Id, bool IsCompleted, DateOnly DueDate);

