using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Appartements
{
    public class DetailAppartementResponse : IResponse<AppartementViewModel>
    {
        public required AppartementViewModel Contenu { get; set; }
    }
}
