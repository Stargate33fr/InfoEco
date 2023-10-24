using Infeco.Api.Commands.Appartements;
using Infeco.Api.Commands.LocataireAppartement;
using Infeco.Api.Commands.Paiement;
using Infeco.Api.Queries.Paiement;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("paiement")]
    public class PaiementController: AppControllerBase
    {
        public PaiementController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{LocataireId:int}/{id:int}", Name = "ObtenirPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PaiementViewModel>> ObtenirPaiementAsync([FromRoute] int locataireId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new ObtenirPaiementQuery
            {
                Id = id,
                LocataireId = locataireId
            };
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }


        [HttpPost]
        [Route("", Name = "creerPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PaiementViewModel>> CreerPaiementAsync([FromBody] CreerPaiementCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }

        [HttpPut]
        [Route("{LocataireAppartementId:int}/{id:int}", Name = "modifierPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task ModifierPaiementAsync([FromRoute] int locataireId, [FromRoute] int id, [FromBody] ModifierPaiementCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            command.LocataireId = locataireId;
            await Mediator.Send(command, cancellationToken);
        }


        [HttpDelete]
        [Route("{LocataireId:int}/{id:int}", Name = "supprimerPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task SupprimerPaiementAsync([FromRoute] int locataireId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SupprimerPaiementCommand
            {
                Id = id,
                LocataireId = locataireId
            };

            await Mediator.Send(command, cancellationToken);
        }
    }
}
