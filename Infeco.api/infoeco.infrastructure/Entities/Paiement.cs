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
        public int TypePaiementId { get; set; }
        public DateTime DatePaiement {  get; set; }
        public int LocataireAppartementId { get; set; }
        public virtual LocataireAppartementEntite? LocataireAppartement { get; set; }
        public virtual TypePaiementEntite? TypePaiement { get; set; }
       
    }
}
