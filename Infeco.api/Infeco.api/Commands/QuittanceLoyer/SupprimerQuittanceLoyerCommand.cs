using FluentValidation.Results;
using Infeco.Api.Commands.QuittanceLoyer.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.QuittanceLoyer
{
    public class SupprimerQuittanceLoyerCommand : Command
    {
        public int? LocataireAppartementId { get; set; }
        public override ValidationResult Valide()
        {
            return new SupprimerQuittanceLoyerCommandValidation().Validate(this);
        }
    }
}
