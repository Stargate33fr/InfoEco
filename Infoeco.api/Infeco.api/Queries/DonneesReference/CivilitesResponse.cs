using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.DonneesReference
{
    public class CivilitesResponse : IResponse<IReadOnlyCollection<CiviliteViewModel>>
    {
        public required IReadOnlyCollection<CiviliteViewModel> Contenu { get; set; }

        public int Total { get; set; }
    }
}
