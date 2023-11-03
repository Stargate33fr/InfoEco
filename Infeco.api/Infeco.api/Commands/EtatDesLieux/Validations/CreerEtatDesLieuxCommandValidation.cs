using FluentValidation;

namespace Infeco.Api.Commands.EtatDesLieux.Validations
{
    public class CreerEtatDesLieuxCommandValidation : AbstractValidator<CreerEtatDesLieuxCommand>
    {
        public CreerEtatDesLieuxCommandValidation()
        {
            RuleFor(c => c.LocataireAppartementId).NotEmpty()
           .WithMessage("lid de la location doit être renseigné");

            RuleFor(c => c.DateEtatDesLieux).NotEmpty()
            .WithMessage("la date de l'état des lieux doit être renseignée");

        }
    }
}
