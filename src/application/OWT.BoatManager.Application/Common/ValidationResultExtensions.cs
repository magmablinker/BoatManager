using FluentValidation.Results;
using OWT.BoatManager.Application.Abstractions;

namespace OWT.BoatManager.Application.Common;
internal static class ValidationResultExtensions
{
    public static Response<TResponse> ToBadRequestResponse<TResponse>(this ValidationResult validationResult) =>
        Response<TResponse>.Failure(ErrorDefaults.Generic.BadRequest()
            .AddMessages(validationResult.Errors.Select(e => e.ErrorMessage)));
}
