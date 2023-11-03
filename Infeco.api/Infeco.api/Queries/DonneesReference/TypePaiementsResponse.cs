using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.DonneesReference
{
    public class TypePaiementsResponse : IResponse<IReadOnlyCollection<TypePaiementViewModel>>
    {
        public required IReadOnlyCollection<TypePaiementViewModel> Contenu { get; set; }

        public int Total { get; set; }
    }
}
