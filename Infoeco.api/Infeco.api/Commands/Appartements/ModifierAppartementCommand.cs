using FluentValidation;
using FluentValidation.Results;
using Infeco.Api.Commands.Appartements.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.Appartements
{
    public class ModifierAppartementCommand : ActionAppartementCommand
    {
        public override ValidationResult Valide()
        {
            return new ModifierAppartementCommandValidation().Validate(this);
        }
    }
}
