namespace Thunders.Tasks.API.Application.DTOs;

public record TaskItemResponseDTO(Guid Id, string Title, string Description, bool IsCompleted, DateOnly DueDate, DateTime CreatedDate, DateTime? UpdatedDate);
