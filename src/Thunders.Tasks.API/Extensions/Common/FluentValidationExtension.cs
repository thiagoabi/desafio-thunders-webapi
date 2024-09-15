using FluentValidation.Results;

namespace Thunders.Tasks.API;

public static class FluentValidationExtension
{
    public static IEnumerable<AppError> ToErrorArray(this IEnumerable<ValidationFailure> validations)
    {
        return validations.Select(x => new AppError(x.ErrorMessage, Application.Enumerators.EnumErrorType.Validation));
    }
}
