using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.AgenceImmobiliere
{
    public class AppartementsResponse : IResponse<IReadOnlyCollection<AppartementViewModel>>
    {
        public required IReadOnlyCollection<AppartementViewModel> Contenu { get; set; }

        public int? Total { get; set; }
    }
}
