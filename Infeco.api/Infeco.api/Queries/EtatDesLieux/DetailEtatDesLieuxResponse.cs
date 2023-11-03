using Infeco.Api.ViewModel;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.EtatDesLieux
{
    public class DetailEtatDesLieuxResponse : IResponse<EtatDesLieuxViewModel>
    {
        public required EtatDesLieuxViewModel Contenu { get; set; }
    }
}
