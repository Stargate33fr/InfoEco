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
        [Route("tous/{locataireAppartementId:int}", Name = "ObtenirPaiementTous")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<PaiementViewModel>>> ObtenirPaiementTousAsync([FromRoute] int locataireAppartementId, CancellationToken cancellationToken)
        {
            var command = new ObtenirTousPaiementQuery
            {
                LocataireAppartementId = locataireAppartementId
            };
            
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        [HttpGet]
        [Route("{locataireAppartementId:int}/{id:int}", Name = "ObtenirPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<PaiementViewModel>> ObtenirPaiementAsync([FromRoute] int locataireAppartementId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new ObtenirPaiementQuery
            {
                Id = id,
                LocataireAppartementId = locataireAppartementId
            };
           
            return Ok(await Mediator.Send(command, cancellationToken));
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
        [Route("{locataireAppartementId:int}/{id:int}", Name = "modifierPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task ModifierPaiementAsync([FromRoute] int locataireAppartementId, [FromRoute] int id, [FromBody] ModifierPaiementCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            command.LocataireAppartementId = locataireAppartementId;
            await Mediator.Send(command, cancellationToken);
        }


        [HttpDelete]
        [Route("{locataireAppartementId:int}/{id:int}", Name = "supprimerPaiement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task SupprimerPaiementAsync([FromRoute] int locataireAppartementId, [FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SupprimerPaiementCommand
            {
                Id = id,
                LocataireAppartementId = locataireAppartementId
            };

            await Mediator.Send(command, cancellationToken);
        }
    }
}
