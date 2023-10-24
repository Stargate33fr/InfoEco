using Infoeco.infrastructure.Entities;
using System.ComponentModel.DataAnnotations;

namespace Infeco.Api.ViewModel
{
    public class LocataireAppartementViewModel
    {
        public int Id { get; set; }
        public int LocataireId { get; set; }
        public int AppartementId { get; set; }
        public bool DepotDeGarantieRegle { get; set; } = false;
        public List<LocataireViewModel> Locataires { get; set; } = new List<LocataireViewModel>();   
        public AppartementViewModel? Appartement { get; set; }
    }
}
