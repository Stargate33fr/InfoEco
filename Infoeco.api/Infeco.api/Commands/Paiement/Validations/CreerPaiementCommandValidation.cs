using FluentValidation;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Paiement.Validations
{
    public class CreerPaiementCommandValidation : ActionPaiementCommandValidation<CreerPaiementCommand>
    {
        public CreerPaiementCommandValidation()
        {
            ValideLocataireId();
            ValideDatePaiement();
            ValideProvenancePaiementId();
            ValideMontant();
        }
    }
}
