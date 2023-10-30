using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoEco.Domain.Request
{
    public class GetLocataireAppartementRequest
    {
        public int? LocataireId { get; set; }
        public int? AppartementId { get; set; }
        public string? MailLocataire { get; set; }
    }
}
