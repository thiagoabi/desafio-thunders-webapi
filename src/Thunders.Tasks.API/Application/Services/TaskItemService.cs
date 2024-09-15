using AutoMapper;
using FluentValidation;
using OneOf;
using Thunders.Tasks.API.Application.DTOs;
using Thunders.Tasks.API.Application.Errors.Resources;
using Thunders.Tasks.API.Domain.Entities;
using Thunders.Tasks.API.Domain.Services;

namespace Thunders.Tasks.API.Application.Services;

public class TaskItemService(ITaskItemRepository taskItemRepository, IMapper mapper, IValidator<TaskItem> validator) : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository = taskItemRepository;
    private readonly IMapper _mapper = mapper;
    private readonly IValidator<TaskItem> _validator = validator;

    public async Task<OneOf<IEnumerable<TaskItemResponseDTO>, IEnumerable<AppError>>> GetAllTaskItemsAsync()
    {
        IEnumerable<TaskItem> result = await _taskItemRepository.GetAllAsync();

        return result.Any() ? _mapper.Map<List<TaskItemResponseDTO>>(result) : new[] { new TaskNoneFoundError() };
    }

    public async Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> GetTaskItemByIdAsync(Guid id)
    {
        var result = await _taskItemRepository.GetByIdAsync(id);

        return result is { } ? _mapper.Map<TaskItemResponseDTO>(result) : new[] { new TaskNotFoundByIdError() };
    }

    public async Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> CreateTaskItemAsync(TaskItemRequestCreateDTO taskItem)
    {
        var entity = _mapper.Map<TaskItem>(taskItem);

        var validationResult = _validator.Validate(entity);

        if (!validationResult.IsValid)
        {
            return OneOf<TaskItemResponseDTO, IEnumerable<AppError>>.FromT1(validationResult.Errors.ToErrorArray());
        }

        var result = await _taskItemRepository.AddAsync(entity);

        return _mapper.Map<TaskItemResponseDTO>(result);
    }

    public async Task<OneOf<TaskItemResponseDTO, IEnumerable<AppError>>> UpdateTaskItemAsync(Guid id, TaskItemRequestUpdateDTO taskItem)
    {

        if (!await _taskItemRepository.ExistsAsync(taskItem.Id))
        {
            return new[] { new TaskNotFoundByIdError() };
        }

        var entity = await _taskItemRepository.GetByIdAsync(id);

        entity = _mapper.Map(taskItem, entity);

        var validationResult = _validator.Validate(entity!);

        if (!validationResult.IsValid)
        {
            return OneOf<TaskItemResponseDTO, IEnumerable<AppError>>.FromT1(validationResult.Errors.ToErrorArray());
        }

        var result = await _taskItemRepository.UpdateAsync(entity!);

        return _mapper.Map<TaskItemResponseDTO>(result);
    }

    public async Task<OneOf<bool, IEnumerable<AppError>>> DeleteTaskItemAsync(Guid id)
    {
        if (!await _taskItemRepository.ExistsAsync(id))
        {
            return new[] { new TaskNotFoundByIdError() };
        }

        await _taskItemRepository.DeleteAsync(id);

        return true;
    }

    public async Task<bool> TaskItemExistsAsync(Guid id)
    {
        return await _taskItemRepository.ExistsAsync(id);
    }
}
