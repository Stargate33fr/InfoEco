using Infoeco.infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.ViewModel
{
    public class EtatDesLieuxViewModel
    {
        public int Id { get; set; }
        public DateTime DateEtatDesLieux { get; set; }
        public string? Remarque { get; set; }
        public int LocataireAppartementId { get; set; }
        public virtual LocataireAppartementViewModel? LocataireAppartement { get; set; }
    }
}
