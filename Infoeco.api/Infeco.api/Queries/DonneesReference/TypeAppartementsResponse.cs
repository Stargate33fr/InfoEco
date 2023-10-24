using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.DonneesReference
{
    public class TypeAppartementsResponse : IResponse<IReadOnlyCollection<TypeAppartementViewModel>>
    {
        public required IReadOnlyCollection<TypeAppartementViewModel> Contenu { get; set; }

        public int Total { get; set; }
    }
}
