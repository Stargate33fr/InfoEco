using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Locataire
{
    public class DetailLocataireAppartementResponse : IResponse<List<LocataireAppartementViewModel>>
    {
        public required List<LocataireAppartementViewModel> Contenu { get; set; }
    }
}
