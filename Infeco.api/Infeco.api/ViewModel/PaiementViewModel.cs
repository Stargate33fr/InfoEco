using Infoeco.infrastructure.Entities;

namespace Infeco.Api.ViewModel
{
    public class PaiementViewModel
    {
        public int Id { get; set; }
        public int LocataireAppartementId { get; set; }
        public double Montant { get; set; }
        public int TypePaiementId { get; set; }
        public DateTime DatePaiement { get; set; }
        public virtual LocataireViewModel? Locataire { get; set; }
        public virtual TypePaiementViewModel? TypePaiement { get; set; }
    }
}
