using FluentValidation.Results;
using Infeco.Api.Commands.LocataireAppartement.Validations;
using Infeco.Api.Infrastructure.MediatR;

namespace Infeco.Api.Commands.LocataireAppartement
{
    public class AssignerAppartementCommand: Command
    {
        public int AppartementId { get; set; }
        public int LocataireId { get; set; }
        public bool DepotDeGarantieRegle { get; set; } = false;
        public DateTime DateEntree {  get; set; } = DateTime.Now;

        public override ValidationResult Valide()
        {
            return new AssignerAppartementCommandValidation().Validate(this);
        }
    }
}
