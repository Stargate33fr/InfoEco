using FluentValidation.Results;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Locataire
{
    public class ModifierLocataireCommand : ActionLocataireCommand
    {
        public override ValidationResult Valide()
        {
            return new ModifierLocataireCommandValidation().Validate(this);
        }
    }
}
