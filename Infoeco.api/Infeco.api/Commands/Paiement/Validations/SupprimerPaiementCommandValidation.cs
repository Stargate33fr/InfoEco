using FluentValidation;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Paiement.Validations
{
    public class SupprimerPaiementCommandValidation : ActionPaiementCommandValidation<SupprimerPaiementCommand>
    {
        public SupprimerPaiementCommandValidation()
        {
            ValideId();
            ValideLocataireId();
        }
    }
}
