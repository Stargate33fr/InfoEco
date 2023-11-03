using FluentValidation;

namespace Infeco.Api.Commands.QuittanceLoyer.Validations
{
    public class SupprimerQuittanceLoyerCommandValidation : AbstractValidator<SupprimerQuittanceLoyerCommand>
    {
        public SupprimerQuittanceLoyerCommandValidation()
        {
            RuleFor(ac => ac.Id).GreaterThan(0)
             .WithMessage("l'id du la quittance loyer doit être renseigné");

            RuleFor(ac => ac.LocataireAppartementId).GreaterThan(0)
               .WithMessage("l'id du locataireAppartement doit être renseigné");
        }
    }
}
