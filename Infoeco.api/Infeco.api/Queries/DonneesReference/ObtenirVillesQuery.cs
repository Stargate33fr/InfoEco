using MediatR;

namespace Infeco.Api.Queries.DonneesReference
{
    public class ObtenirVillesQuery :  IRequest<VillesResponse>
    {
        public required string NomRecherche {  get; set; }
    }
}
