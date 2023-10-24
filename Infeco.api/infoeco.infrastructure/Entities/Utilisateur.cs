using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class UtilisateurEntite : TrackedEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Prenom { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Courriel { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string Passe { get; set; } = string.Empty;

        public bool EstActif { get; set; } = true;

        public int AgenceImmobiliereId { get; set; }

        public virtual AgenceImmobiliereEntite? AgenceImmobiliere { get; set; }
    }
}
