using Bogus;
using OWT.BoatManager.Application.UseCases.Boats.Models;
using OWT.BoatManager.Core.UseCases.Boats;

namespace OWT.BoatManager.Application.Tests.UseCases.Boats;

internal sealed class BoatModelFaker : Faker<BoatModel>
{
    public BoatModelFaker()
    {
        RuleFor(b => b.Name, f => f.Random.String(1, BoatConstraints.NameMaxLength));
        RuleFor(b => b.Description, f => f.Random.String(1, BoatConstraints.DescriptionMaxLength));
    }
}