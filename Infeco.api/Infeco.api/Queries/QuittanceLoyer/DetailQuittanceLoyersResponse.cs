using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.QuittanceLoyer
{
    public class DetailQuittanceLoyersResponse : IResponse<List<QuittanceLoyersViewModel>>
    {
        public required List<QuittanceLoyersViewModel> Contenu { get; set; }
    }
}
