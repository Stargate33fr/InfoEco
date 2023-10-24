using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Paiement
{
    public class DetailPaiementResponse : IResponse<PaiementViewModel>
    {
        public required PaiementViewModel Contenu { get; set; }
    }
}
