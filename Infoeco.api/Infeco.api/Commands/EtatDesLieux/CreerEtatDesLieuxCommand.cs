using FluentValidation.Results;
using Infeco.Api.Commands.EtatDesLieux.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.EtatDesLieux
{
    public class CreerEtatDesLieuxCommand : Command
    {
        public int LocataireAppartementId { get; set; }
        public DateTime DateEtatDesLieux { get; set; }
        public string? Remarque { get; set; }    


        public override ValidationResult Valide()
        {
            return new CreerEtatDesLieuxCommandValidation().Validate(this);
        }
    }
}
