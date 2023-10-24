using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModels;
using MediatR;

namespace Infeco.Api.Queries.AgenceImmobiliere
{
    public class ObtenirAppartementsQuery : ObtenirAppartementsQueryBase
    {
        public PaginationModel? Pagination { get; set; }
        public TriModel? Tri { get; set; }
    }
}
