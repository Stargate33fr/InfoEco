using FluentValidation;

namespace Infeco.Api.Commands.Appartements.Validations
{
    public abstract class ActionAppartementCommandValidation<T> : AbstractValidator<T>
        where T : ActionAppartementCommand
    {
        protected void ValideId()
        {
            RuleFor(ac => ac.Id).GreaterThan(0)
                .WithMessage("L'identifiant de l'appartement doit être renseigné");
        }

        protected void ValideAdresse()
        {
            RuleFor(c => c.Adresse).NotEmpty()
               .WithMessage("l'adresse doit être renseigné");
        }

        protected void ValideVille()
        {
            RuleFor(c => c.VilleId).NotEmpty()
           .WithMessage("l'id de la ville doit être renseigné");
        }

        protected void ValidePrixDesCharges()
        {
            RuleFor(c => c.PrixDesCharges).NotEmpty()
           .WithMessage("le prix des charges doit être renseigné");
        }

        protected void ValideDepotDeGarantie()
        {
            RuleFor(c => c.DepotDeGarantie).NotEmpty()
                 .WithMessage("le dépôt de garantie doit être renseigné");
        }
        protected void ValideLoyer()
        {
            RuleFor(c => c.Loyer).NotEmpty()
      .WithMessage("le loyer de l'appartement doit être renseigné");

        }

        protected void ValideTypeAppartement()
        {
            RuleFor(c => c.TypeAppartementId).NotEmpty()
            .WithMessage("le type d'appartement doit être renseigné");
        }
    }
}
