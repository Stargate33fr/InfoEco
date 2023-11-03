using MediatR;

namespace Infeco.Api.Queries.Paiement
{
    public class ObtenirTousPaiementQuery : IRequest<DetailPaiementTousResponse>
    {
        public int Id { get; set; }
        public int LocataireAppartementId { get; set; }
    }
}
