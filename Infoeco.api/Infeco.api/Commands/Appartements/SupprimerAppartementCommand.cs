using FluentValidation.Results;
using Infeco.Api.Commands.Appartements.Validations;

namespace Infeco.Api.Commands.Appartements
{
    public class SupprimerAppartementCommand : ActionAppartementCommand
    {
        public override ValidationResult Valide()
        {
            return new SupprimerAppartementCommandValidation().Validate(this);
        }
    }
}
