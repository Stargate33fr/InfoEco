using FluentValidation;
using Infeco.Api.Commands.Appartements;
using Infeco.Api.Commands.LocataireAppartement;

namespace Infeco.Api.Commands.QuittanceLoyer.Validations
{
    public class CreerQuittanceLoyerCommandValidation : AbstractValidator<CreerQuittanceLoyerCommand>
    {
        public CreerQuittanceLoyerCommandValidation()
        {
            RuleFor(c => c.LocataireAppartementId).NotEmpty()
           .WithMessage("lid de la location doit être renseigné");

            RuleFor(c => c.mois).NotEmpty()
            .WithMessage("le mois et l'année de la quittance doivent être renseignés");

            RuleFor(c => c.Annee).NotEmpty()
           .WithMessage("le mois et l'année de la quittance doivent être renseignés");
        }
    }
}
