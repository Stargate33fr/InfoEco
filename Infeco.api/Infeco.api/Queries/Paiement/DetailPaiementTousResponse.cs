using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Paiement
{
    public class DetailPaiementTousResponse : IResponse<List<PaiementViewModel>>
    {
        public required List<PaiementViewModel> Contenu { get; set; }
    }
}
