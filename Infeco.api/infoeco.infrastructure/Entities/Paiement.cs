using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class PaiementEntite: TrackedEntity
    {
        public int Id { get; set; }
        public double Montant {  get; set; }
        public int LocataireId { get; set; }
        public int ProvenancePaiementId { get; set; }
        public DateTime DatePaiement {  get; set; }
        public virtual LocataireEntite? Locataire { get; set; }
        public virtual ProvenancePaiementEntite? ProvenancePaiement { get; set; }
       
    }
}
