using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infoeco.infrastructure.Entities
{
    public class BilanEntite: TrackedEntity
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public DateTime Date { get; set; }
    }
}
