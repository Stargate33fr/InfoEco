using MediatR;

namespace Infeco.Api.Queries.QuittanceLoyer
{
    public class ObtenirQuittanceLoyersQuery : IRequest<DetailQuittanceLoyersResponse>
    {
        public int LocataireAppartementId { get; set; }
    }
}
