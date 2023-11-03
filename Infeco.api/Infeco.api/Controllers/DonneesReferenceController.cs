using Infeco.Api.Queries.Appartements;
using Infeco.Api.Queries.DonneesReference;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("donneesReference")]
    public class DonneesReferenceController : AppControllerBase
    {

        public DonneesReferenceController(IMediator mediator)
          : base(mediator)
        {
        }


        [HttpGet]
        [Route("civilites", Name = "obtenirCivilites")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AppartementViewModel>> ObtenirCivilitesAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirCivilitesQuery(), cancellationToken));
        }

        [HttpGet]
        [Route("ville/{nomRecherche}", Name = "obtenirVilles")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AppartementViewModel>> ObtenirVillesAsync([FromRoute] string nomRecherche, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirVillesQuery() { NomRecherche = nomRecherche }, cancellationToken));
        }

        [HttpGet]
        [Route("typeAppartement", Name = "obtenirTypeAppartements")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TypeAppartementViewModel>> ObtenirTypeAppartementsAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirTypeAppartementsQuery(), cancellationToken));
        }

        [HttpGet]
        [Route("typePaiement", Name = "obtenirTypePaiements")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<TypePaiementViewModel>> ObtenirTypePaiementsAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirTypePaiementsQuery(), cancellationToken));
        }
    }
}
