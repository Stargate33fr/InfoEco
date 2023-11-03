using FluentValidation.Results;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Locataire
{
    public class CreerLocataireCommand : ActionLocataireCommand
    {
        public override ValidationResult Valide()
        {
            return new CreerLocataireCommandValidation().Validate(this);
        }
    }
}
