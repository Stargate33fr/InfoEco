using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class VilleEntite : TrackedEntity
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Nom { get; set; } = string.Empty;
        public string CodePostal { get; set; } = string.Empty;
    }
}
