using Infeco.Api.Infrastructure.MediatR;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.Commands.Locataire
{
    public abstract class ActionPaiementCommand : Command
    {
        public int LocataireId { get; set; }
        public double Montant { get; set; }
        public int ProvenancePaiementId { get; set; }
        public DateTime DatePaiement { get; set; }
    }
}
