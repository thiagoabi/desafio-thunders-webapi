using FluentValidation;
using Thunders.Tasks.API.Domain.Entities;

namespace Thunders.Tasks.API.Application.Validations;

public class TaskItemValidator : AbstractValidator<TaskItem>
{
    public TaskItemValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Título é obrigatório.")
            .MaximumLength(100).WithMessage("Título não pode ter mais de 100 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Descrição é obrigatório.")
            .MaximumLength(500).WithMessage("Descrição não pode ter mais de 500 caracteres.");

        RuleFor(x => x.DueDate)
            .NotEmpty().WithMessage("Data de vencimento é obrigatória.")
            .Must(BeGreaterThanOrEqualToToday)
            .WithMessage("Data de vencimento tem que ser maior ou igual a data atual.");
    }

    private bool BeGreaterThanOrEqualToToday(DateOnly dueDate)
    {
        return dueDate >= DateOnly.FromDateTime(DateTime.Now);
    }
}