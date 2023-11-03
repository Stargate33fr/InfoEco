using FluentValidation;
using Infeco.Api.Commands.Locataire.Validations;

namespace Infeco.Api.Commands.Paiement.Validations
{
    public class ModifierPaiementCommandValidation : ActionPaiementCommandValidation<ModifierPaiementCommand>
    {
        public ModifierPaiementCommandValidation()
        {
            ValideId();
        }
    }
}
