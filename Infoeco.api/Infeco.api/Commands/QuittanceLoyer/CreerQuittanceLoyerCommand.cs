using FluentValidation.Results;
using Infeco.Api.Commands.Appartements.Validations;
using Infeco.Api.Commands.EtatDesLieux.Validations;
using Infeco.Api.Commands.QuittanceLoyer.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.QuittanceLoyer
{
    public class CreerQuittanceLoyerCommand : Command
    {
        public int LocataireAppartementId { get; set; }
        public int mois { get; set; }
        public int Annee { get; set; }
        public double Montant { get; set; }
        public bool StatutPaiement {get; set;}

        public override ValidationResult Valide()
        {
            return new CreerQuittanceLoyerCommandValidation().Validate(this);
        }
    }
}
