using FluentValidation;

namespace Infeco.Api.Commands.Locataire.Validations
{
    public abstract class ActionPaiementCommandValidation<T> : AbstractValidator<T>
        where T : ActionPaiementCommand
    {
        protected void ValideId()
        {
            RuleFor(ac => ac.Id).GreaterThan(0)
                .WithMessage("l'id du paiement doit être renseigné");
        }

        protected void ValideLocataireId()
        {
            RuleFor(ac => ac.LocataireId).GreaterThan(0)
                .WithMessage("l'id du locataire doit être renseigné");
        }

        protected void ValideDatePaiement()
        {
            RuleFor(c => c.DatePaiement).NotEmpty()
           .WithMessage("la date du paiement n doit être renseignée");
        }

        protected void ValideProvenancePaiementId()
        {
            RuleFor(c => c.ProvenancePaiementId).NotEmpty()
           .WithMessage("l'id de la provenance du paiement doit être renseigné");
        }
        protected void ValideMontant()
        {
            RuleFor(c => c.Montant).NotEmpty()
           .WithMessage("le montant du paiement doit être renseigné");
        }
    }
}
