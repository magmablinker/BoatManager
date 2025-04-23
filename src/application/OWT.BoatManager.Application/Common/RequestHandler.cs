using OWT.BoatManager.Application.Abstractions;

namespace OWT.BoatManager.Application.Common;

internal abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
{
    public abstract Task<Response<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);

    public async Task<object> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not TRequest typedRequest)
        {
            throw new ArgumentException($"Invalid request type. Expected {typeof(TRequest)}, but got {request.GetType()}.",
                nameof(request));
        }

        return await HandleAsync(typedRequest, cancellationToken);
    }
}