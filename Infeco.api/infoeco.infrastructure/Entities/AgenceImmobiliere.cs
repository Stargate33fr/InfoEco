using System.ComponentModel.DataAnnotations;

namespace Infoeco.infrastructure.Entities
{
    public class AgenceImmobiliereEntite: TrackedEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? ComplementAdresse { get; set; }
        public int VilleId { get; set; }
        public virtual VilleEntite? Ville { get; set; }
        public double FraisAgence { get; set; } = 8;
        public virtual ICollection<AppartementEntite> Appartements { get; set; } = new HashSet<AppartementEntite>();
    }
}
