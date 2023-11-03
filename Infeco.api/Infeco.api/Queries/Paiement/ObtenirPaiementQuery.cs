using MediatR;

namespace Infeco.Api.Queries.Paiement
{
    public class ObtenirPaiementQuery : IRequest<DetailPaiementResponse>
    {
        public int Id { get; set; }
        public int LocataireAppartementId { get; set; }
    }
}
