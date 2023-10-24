using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.DonneesReference
{
    public class VillesResponse : IResponse<IReadOnlyCollection<VilleViewModel>>
    {
        public required IReadOnlyCollection<VilleViewModel> Contenu { get; set; }

        public int Total { get; set; }
    }
}
