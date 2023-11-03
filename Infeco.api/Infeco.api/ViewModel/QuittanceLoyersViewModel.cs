using Infoeco.infrastructure.Entities;

namespace Infeco.Api.ViewModel
{
    public class QuittanceLoyersViewModel
    {
        public int Id { get; set; }
        public int LocataireAppartementId { get; set; }
        public int Mois { get; set; }
        public int Annee { get; set; }
        public double Montant { get; set; }
        public bool Paye { get; set; } = false;
        public LocataireAppartementViewModel? LocataireAppartement { get; set; }
    }
}
