using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Commands.LocataireAppartement;
using Infeco.Api.Queries.Appartements;
using Infeco.Api.Queries.EtatDesLieux;
using Infeco.Api.Queries.LocataireAppartement;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{

    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("etatDesLieux")]
    public class EtatDesLieuxController : AppControllerBase
    {
        public EtatDesLieuxController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{appartementLocataireId:int}", Name = "obtenirEtatDesLieux")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<EtatDesLieuxViewModel>> ObtenirEtatDesLieux([FromRoute] int appartementLocataireId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirEtatDesLieuxQuery
            {
                LocataireAppartementId = appartementLocataireId
            }, cancellationToken));
        }

        [HttpPost]
        [Route("", Name = "creerUnEtatsDesLieux")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> CreerUnEtatsDesLieuxAsync([FromBody] CreerEtatDesLieuxCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
