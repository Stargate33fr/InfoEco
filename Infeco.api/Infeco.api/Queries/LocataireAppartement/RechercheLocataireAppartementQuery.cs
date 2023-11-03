using Infeco.Api.Queries.Locataire;
using MediatR;

namespace Infeco.Api.Queries.LocataireAppartement
{
    public class RechercheLocataireAppartementQuery : IRequest<DetailLocataireAppartementResponse>
    {
        public int? Id { get; set; }
        public int? LocataireId { get; set; }
        public string? Nom { get; set; }
        public string? Email { get; set; }
        public int? AppartementId {get; set;}
        public int? VilleId { get; set; }
    }
}
