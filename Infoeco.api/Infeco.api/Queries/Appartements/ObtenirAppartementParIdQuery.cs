using MediatR;

namespace Infeco.Api.Queries.Appartements
{
    public class ObtenirAppartementParIdQuery : IRequest<DetailAppartementResponse>
    {
        public int AppartementId { get; set; }
    }
}
