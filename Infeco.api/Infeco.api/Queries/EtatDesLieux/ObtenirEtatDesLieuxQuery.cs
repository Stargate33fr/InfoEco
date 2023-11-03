using Infeco.Api.Queries.EtatDesLieux;
using MediatR;

namespace Infeco.Api.Queries.EtatDesLieux
{
    public class ObtenirEtatDesLieuxQuery : IRequest<DetailEtatDesLieuxResponse>
    {
        public int LocataireAppartementId { get; set; }
    }
}
