using OWT.BoatManager.Application.Abstractions;

namespace OWT.BoatManager.Api.Common;

internal static class ResponseExtensions
{
    public static IResult ToResult<T>(this Response<T> response)
    {
        if (response.IsSuccess)
        {
            return response.Value is not null
                ? Results.Ok(response.Value)
                : Results.NoContent();
        }

        var messages = response.Error.Messages.Any() ? response.Error.Messages : null;

        return response.Error.Identifier switch
        {
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.NotFound)}" => Results.NotFound(messages),
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.Forbidden)}" => Results.NotFound(messages),
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.Unauthorized)}" => Results.Unauthorized(),
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.Conflict)}" => Results.Conflict(messages),
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.UnprocessableEntity)}" => Results.UnprocessableEntity(messages),
            $"{nameof(ErrorDefaults.Generic)}.{nameof(ErrorDefaults.Generic.BadRequest)}" => Results.BadRequest(messages),
            _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
        };
    }
}