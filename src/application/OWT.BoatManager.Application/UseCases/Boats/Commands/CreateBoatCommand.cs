using FluentValidation;
using OWT.BoatManager.Application.Abstractions;
using OWT.BoatManager.Application.Common;
using OWT.BoatManager.Application.UseCases.Boats.Commands.Mappings;
using OWT.BoatManager.Application.UseCases.Boats.Models;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands;
public sealed class CreateBoatCommand : IRequest<Guid>
{
    public required BoatModel Boat { get; init; }
}

internal sealed class CreateBoatCommandHandler : RequestHandler<CreateBoatCommand, Guid>
{
    private readonly IValidator<BoatModel> _validator;
    private readonly IBoatStore _boatStore;

    public CreateBoatCommandHandler(IValidator<BoatModel> validator, IBoatStore boatStore)
    {
        _validator = validator;
        _boatStore = boatStore;
    }

    public override async Task<Response<Guid>> HandleAsync(CreateBoatCommand request, CancellationToken cancellationToken = default)
    {
        var validationResult = await _validator.ValidateAsync(request.Boat, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.ToBadRequestResponse<Guid>();

        var entity = request.Boat.ToEntity();
        await _boatStore.AddAsync(entity, cancellationToken);

        return Response<Guid>.Success(entity.Id);
    }
}