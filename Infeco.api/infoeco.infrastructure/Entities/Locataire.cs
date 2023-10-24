using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class LocataireEntite : TrackedEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; } =string.Empty;
        [Required]
        [StringLength(255)]
        public string Prenom { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string Mail { get; set; } = string.Empty;
        [Required]
        [StringLength(32)]
        public string Telephone { get; set; } = string.Empty;
        public DateTime? DateNaissance { get; set; }
        [Required]
        [StringLength(255)]
        public string IBAN { get; set; } = string.Empty;
        [Required]
        public int CiviliteId { get; set; }
        public virtual required CiviliteEntite Civilite { get; set; }
    }
}
