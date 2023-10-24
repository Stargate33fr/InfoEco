using FluentValidation.Results;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.Locataire.Validations;
using Infeco.Api.Commands.Paiement.Validations;

namespace Infeco.Api.Commands.Paiement
{
    public class ModifierPaiementCommand : ActionPaiementCommand
    {
        public override ValidationResult Valide()
        {
            return new ModifierPaiementCommandValidation().Validate(this);
        }
    }
}
