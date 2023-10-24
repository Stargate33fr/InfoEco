using Infeco.Api.Queries.AgenceImmobiliere;
using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModel;
using Infeco.Api.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("agenceImmobiliere")]
    public class AgenceImmobiliereController : AppControllerBase
    {
        public AgenceImmobiliereController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpPost]
        [Route("appartements/recherche", Name = "obtenirAppartementParIdAgenceImmobiliere")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<AppartementViewModel>>> ObtenirAppartementParIdAgenceImmobiliereAsync([FromBody] ObtenirAppartementsQueryBase request, PaginationModel pagination, TriModel tri, CancellationToken cancellationToken)
        {
            var recherche = new ObtenirAppartementsQuery
            {
                Adresse = request.Adresse,
                NomLocataire = request.NomLocataire,
                TypeAppartementId = request.TypeAppartementId,
                AgenceImmobiliereId = request.AgenceImmobiliereId,
                Pagination = pagination,
                Tri = tri,
                NomVille = request.NomVille,
            };

            return Ok(await Mediator.Send(recherche, cancellationToken));
        }

    }
}
