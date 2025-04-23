using Microsoft.Extensions.Logging;
using OWT.BoatManager.Application.Abstractions;

namespace OWT.BoatManager.Application.Common;
internal sealed class Sender : ISender
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<Sender> _logger;

    public Sender(IServiceProvider serviceProvider, ILogger<Sender> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task<Response<TResponse>> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var requestType = request.GetType();
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(TResponse));

            var handler = _serviceProvider.GetService(handlerType);
            if (handler is null) throw new InvalidOperationException($"No handler found for request type {requestType}");

            var method = handlerType.GetMethod(nameof(IRequestHandler.HandleAsync));
            return await (Task<Response<TResponse>>)method?.Invoke(handler, [request, cancellationToken])!;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured while processing the request");
            return Response<TResponse>.Failure(ErrorDefaults.Generic.InternalServerError());
        }
    }
}