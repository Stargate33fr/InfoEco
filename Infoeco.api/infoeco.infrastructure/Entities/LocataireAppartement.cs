using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class LocataireAppartementEntite: TrackedEntity
    {
        public int Id { get; set; }
        [Required]
        public int LocataireId {  get; set; }
        [Required]
        public int AppartementId { get; set; }
        public bool DepotDeGarantieRegle { get; set; } = false;
        [Required]
        public DateTime DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }
        public virtual required LocataireEntite Locataire {  get; set; }
        public virtual required AppartementEntite Appartement { get; set; }
    }
}
