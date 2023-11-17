using FluentValidation;
using Infeco.Api.Commands.Bilan;

namespace Infeco.Api.Commands.Locataire.Validations
{
    public abstract class EvtBilanCommandValidation<T> : AbstractValidator<T>
        where T : EvtBilanCommand
    {
        protected void ValideId()
        {
            RuleFor(c => c.Id).NotEmpty()
              .WithMessage("l'id doit être renseigné");
        }
        protected void ValideMail()
        {
            RuleFor(c => c.Mail).NotEmpty()
              .WithMessage("l'email doit être renseigné");
        }

        protected void ValideDateEnvoi()
        {
            RuleFor(c => c.Date).NotEmpty()
             .WithMessage("le mois doit être renseigné");
        }
    }
}
