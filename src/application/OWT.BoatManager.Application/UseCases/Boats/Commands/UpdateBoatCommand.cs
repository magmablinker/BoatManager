using FluentValidation;
using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.Common;
using OWT.BoatManager.Application.UseCases.Boats.Commands.Mappings;
using OWT.BoatManager.Application.UseCases.Boats.Models;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands;
public sealed class UpdateBoatCommand : IRequest<Unit>
{
    public required BoatDetailModel Boat { get; init; }
}

internal sealed class UpdateBoatCommandHandler : RequestHandler<UpdateBoatCommand, Unit>
{
    private readonly IValidator<BoatModel> _validator;
    private readonly IBoatStore _boatStore;

    public UpdateBoatCommandHandler(IValidator<BoatModel> validator, IBoatStore boatStore)
    {
        _validator = validator;
        _boatStore = boatStore;
    }

    public override async Task<Response<Unit>> HandleAsync(UpdateBoatCommand request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request.Boat, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToBadRequestResponse<Unit>();

        var existing = await _boatStore.FindAsync(request.Boat.Id, cancellationToken);
        if (existing is null)
            return Response<Unit>.Failure(ErrorDefaults.Generic.NotFound());

        request.Boat.AssignTo(existing);

        await _boatStore.UpdateAsync(existing, cancellationToken);

        return Response<Unit>.Success(Unit.Value);
    }
}