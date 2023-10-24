using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Locataire
{
    public class DetailLocataireResponse : IResponse<LocataireViewModel>
    {
        public required LocataireViewModel Contenu { get; set; }
    }
}
