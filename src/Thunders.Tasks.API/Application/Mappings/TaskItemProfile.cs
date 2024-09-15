using AutoMapper;
using Thunders.Tasks.API.Application.DTOs;
using Thunders.Tasks.API.Domain.Entities;

namespace Thunders.Tasks.API.Application.Mappings;

public class TaskItemProfile : Profile
{

    public TaskItemProfile()
    {
        CreateMap<TaskItemRequestCreateDTO, TaskItem>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted));
        CreateMap<TaskItemRequestUpdateDTO, TaskItem>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.DueDate, opt => opt.MapFrom(src => src.DueDate))
            .ForMember(dest => dest.IsCompleted, opt => opt.MapFrom(src => src.IsCompleted));
        CreateMap<TaskItem, TaskItemResponseDTO>()
            .ConstructUsing(src => new TaskItemResponseDTO(src.Id, src.Title, src.Description, src.IsCompleted, src.DueDate, src.CreatedAt, src.UpdatedAt));
    }
}
