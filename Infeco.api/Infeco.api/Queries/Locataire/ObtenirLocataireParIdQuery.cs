using MediatR;

namespace Infeco.Api.Queries.Locataire
{
    public class ObtenirLocataireParIdQuery : IRequest<DetailLocataireResponse>
    {
        public int LocataireId { get; set; }
    }
}
