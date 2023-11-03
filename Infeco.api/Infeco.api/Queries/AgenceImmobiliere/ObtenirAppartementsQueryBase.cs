using Infeco.Api.Queries.Locataire;
using MediatR;

namespace Infeco.Api.Queries.AgenceImmobiliere
{
    public class ObtenirAppartementsQueryBase : IRequest<AppartementsResponse>
    {
        public int? AgenceImmobiliereId { get; set; }
        public string? NomVille { get; set; }
        public string? Adresse { get; set; }
        public string? NomLocataire { get; set; }
        public int? TypeAppartementId { get; set; }
    }
}
