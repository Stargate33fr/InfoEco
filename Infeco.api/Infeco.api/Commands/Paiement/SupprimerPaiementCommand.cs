using FluentValidation.Results;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.Paiement.Validations;

namespace Infeco.Api.Commands.Paiement
{
    public class SupprimerPaiementCommand : ActionPaiementCommand
    {
        public override ValidationResult Valide()
        {
            return new SupprimerPaiementCommandValidation().Validate(this);
        }
    }
}
