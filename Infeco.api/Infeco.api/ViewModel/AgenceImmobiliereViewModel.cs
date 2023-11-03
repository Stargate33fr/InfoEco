using Infoeco.infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.ViewModel
{
    public class AgenceImmobiliereViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;
        public string Adresse { get; set; } = string.Empty;
        public string? ComplementAdresse { get; set; }
        public VilleViewModel? Ville { get; set; }
        public double FraisAgence { get; set; } = 8;
        public virtual ICollection<AppartementViewModel>? UtilisateursMetier { get; set; } 
    }
}
