using Thunders.Tasks.API.Application.Enumerators;

namespace Thunders.Tasks.API;

public record AppError(string Message, EnumErrorType ErrorType);
public record AppErrorReponse(string ErrorType, string Message);

public static class ErrorExtensions
{
    public static IEnumerable<AppErrorReponse> ToResponse(this IEnumerable<AppError> appError)
    {
        return appError.Select(x => new AppErrorReponse(Enum.GetName(x.ErrorType)!, x.Message));
    }
}

