using FluentValidation.Results;
using Infeco.Api.Commands.Locataire.Validations;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Locataire
{
    public class SupprimerLocataireCommand : ActionLocataireCommand
    {
        public override ValidationResult Valide()
        {
            return new SupprimerLocataireCommandValidation().Validate(this);
        }
    }
}
