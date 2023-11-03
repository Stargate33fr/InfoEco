using FluentValidation.Results;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.Paiement.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.Paiement
{
    public class CreerPaiementCommand : ActionPaiementCommand
    {
        public override ValidationResult Valide()
        {
            return new CreerPaiementCommandValidation().Validate(this);
        }
    }
}
