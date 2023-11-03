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
        public int Mois { get; set; }
        public int Annee { get; set; }
        public double Montant { get; set; }
        public bool Paye {get; set;}

        public override ValidationResult Valide()
        {
            return new CreerQuittanceLoyerCommandValidation().Validate(this);
        }
    }
}
