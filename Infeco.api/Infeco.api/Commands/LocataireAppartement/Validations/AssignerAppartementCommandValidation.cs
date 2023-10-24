using FluentValidation;

namespace Infeco.Api.Commands.LocataireAppartement.Validations
{
    public class AssignerAppartementCommandValidation : AbstractValidator<AssignerAppartementCommand>
    {
        public AssignerAppartementCommandValidation()
        {
            RuleFor(c => c.AppartementId).NotEmpty()
           .WithMessage("l'id de l'appartement doit être renseigné");

            RuleFor(c => c.LocataireId).NotEmpty()
            .WithMessage("l'id du locataire être renseigné");

        }
    }
}
