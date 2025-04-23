using FluentValidation;
using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Application.UseCases.Boats.Commands.Validators;
internal sealed class BoatModelValidator : AbstractValidator<BoatModel>
{
    public BoatModelValidator()
    {
        RuleFor(b => b.Name)
            .NotEmpty()
            .MaximumLength(BoatConstraints.NameMaxLength);

        RuleFor(b => b.Description)
            .NotEmpty()
            .MaximumLength(BoatConstraints.DescriptionMaxLength);
    }
}
