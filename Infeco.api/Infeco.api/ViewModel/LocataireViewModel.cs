using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.ViewModel
{
    public class LocataireViewModel
    {
        public int Id { get; set; }
        public int CiviliteId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        public string Mail { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public DateTime? DateNaissance { get; set; }
        public string IBAN { get; set; } = string.Empty;
        public DateTime? DateEntree { get; set; }
        public DateTime? DateSortie { get; set; }
        public bool? DepotDeGarantie { get; set; }
        public CiviliteViewModel? Civilite { get; set; }
    }
}
