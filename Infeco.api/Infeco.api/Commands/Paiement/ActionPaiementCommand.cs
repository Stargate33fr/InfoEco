using Infeco.Api.Infrastructure.MediatR;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.Commands.Locataire
{
    public abstract class ActionPaiementCommand : Command
    {
        public int LocataireAppartementId { get; set; }
        public double Montant { get; set; }
        public int TypePaiementId { get; set; }
        public DateTime DatePaiement { get; set; }
        public bool GenererQuittanceLoyer { get; set; } = false;
        public int Mois { get; set; }
        public int Annee { get; set; }  
    }
}
