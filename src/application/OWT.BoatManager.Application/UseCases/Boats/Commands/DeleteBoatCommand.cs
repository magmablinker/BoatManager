using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.Common;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands;
public sealed class DeleteBoatCommand : IRequest<Unit>
{
    public required Guid Id { get; init; }
}

internal sealed class DeleteBoatCommandHandler : RequestHandler<DeleteBoatCommand, Unit>
{
    private readonly IBoatStore _boatStore;

    public DeleteBoatCommandHandler(IBoatStore boatStore)
    {
        _boatStore = boatStore;
    }

    public override async Task<Response<Unit>> HandleAsync(DeleteBoatCommand request, CancellationToken cancellationToken = default)
    {
        var boat = await _boatStore.DeleteAsync(request.Id, cancellationToken);
        return boat is null ? Response<Unit>.Failure(ErrorDefaults.Generic.NotFound()) : Response<Unit>.Success(Unit.Value);
    }
}