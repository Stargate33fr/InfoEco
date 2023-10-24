using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class QuittanceLoyerEntite: TrackedEntity
    {
        public int Id { get; set; }
        public int LocataireAppartementId { get; set; }
        public int Mois {  get; set; }
        public int Annee { get; set; }
        public double Montant { get; set; }
        public bool Paye { get; set; } = false;
        public virtual LocataireAppartementEntite? LocataireAppartement { get; set; }
    }
}
