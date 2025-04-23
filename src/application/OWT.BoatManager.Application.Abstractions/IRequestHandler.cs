namespace OWT.BoatManager.Application.Abstractions;
public interface IRequestHandler
{
    Task<object> HandleAsync(object request, CancellationToken cancellationToken = default);
}

public interface IRequestHandler<in TRequest, TResponse> : IRequestHandler
    where TRequest : class, IRequest<TResponse>
{
    Task<Response<TResponse>> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}