using Infeco.Api.Commands.Appartements;
using Infeco.Api.Queries.Appartements;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{

    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("appartements")]
    public class AppartementController : AppControllerBase
    {

        public AppartementController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{id}", Name = "obtenirAppartementParId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<AppartementViewModel>> ObtenirAppartementParIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirAppartementParIdQuery
            {
                AppartementId = id
            }, cancellationToken));
        }

        [HttpPost]
        [Route("", Name = "creerUnAppartement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> CreerUnAppartementAsync([FromBody] CreerAppartementCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }

        [HttpPut]
        [Route("{id:int}", Name = "modifierUnAppartement")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task ModifierUnAppartementAsync([FromRoute] int id, [FromBody] ModifierAppartementCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "SupprimerUnAppartement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task SupprimerUnAppartementAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SupprimerAppartementCommand
            {
                Id = id
            };
     
            await Mediator.Send(command, cancellationToken);
        }
    }
}
