using FluentValidation;

namespace Infeco.Api.Commands.Locataire.Validations
{
    public abstract class ActionLocataireCommandValidation<T> : AbstractValidator<T>
        where T : ActionLocataireCommand
    {
        protected void ValideId()
        {
            RuleFor(ac => ac.Id).GreaterThan(0)
                .WithMessage("L'identifiant du locataire doit être renseigné");
        }

        protected void ValidePrenom()
        {
            RuleFor(c => c.Prenom).NotEmpty()
               .WithMessage("le prénom doit être renseigné");
        }

        protected void ValideNom()
        {
            RuleFor(c => c.Nom).NotEmpty()
           .WithMessage("le nom doit être renseigné");
        }

        protected void ValidDateNaissance()
        {
            RuleFor(c => c.DateNaissance).NotEmpty()
           .WithMessage("la date de naissance doit être renseigné");
        }

        protected void ValideTelephone()
        {
            RuleFor(c => c.Telephone).NotEmpty()
                 .WithMessage("le telephone doit être renseigné");
        }

        protected void ValideIBAN()
        {
            RuleFor(c => c.IBAN).NotEmpty()
                 .WithMessage("l'IBAN doit être renseigné");
        }

        protected void ValideEmail()
        {
            RuleFor(c => c.Mail).NotEmpty()
                 .WithMessage("l'adresse email doit être renseigné");
        }
    }
}
