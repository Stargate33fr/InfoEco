using Infeco.Api.Commands.Paiement;
using Infeco.Api.Commands.QuittanceLoyer;
using Infeco.Api.Queries.EtatDesLieux;
using Infeco.Api.Queries.QuittanceLoyer;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("quittanceLoyer")]
    public class QuittanceLoyerController : AppControllerBase
    {
        public QuittanceLoyerController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{appartementLocataireId:int}", Name = "obtenirQuittanceLoyers")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<QuittanceLoyersViewModel>>> ObtenirQuittanceLoyer([FromRoute] int appartementLocataireId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirQuittanceLoyersQuery
            {
                LocataireAppartementId = appartementLocataireId
            }, cancellationToken));
        }

        [HttpPost]
        [Route("", Name = "creerQuittanceLoyer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> CreerQuittanceLoyerAsync([FromBody] CreerQuittanceLoyerCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }

        [HttpDelete]
        [Route("{locataireAppartementId:int}/{id:int}", Name = "supprimerQuittanceLoyer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> SupprimerQuittanceLoyerAsync([FromRoute] int locataireAppartementId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SupprimerQuittanceLoyerCommand
            {
                Id = id,
                LocataireAppartementId = locataireAppartementId
            };
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
