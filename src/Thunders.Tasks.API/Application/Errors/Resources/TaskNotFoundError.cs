using Thunders.Tasks.API.Application.Enumerators;

namespace Thunders.Tasks.API.Application.Errors.Resources;

public record TaskNotFoundByIdError() : AppError("Tarefa não encontrada", EnumErrorType.BusinessRule);
public record TaskNoneFoundError() : AppError("Nenhuma tarefa encontrada", EnumErrorType.BusinessRule);
