using Infeco.Api.Commands.Appartements;
using Infeco.Api.Commands.Bilan;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{

    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("bilan")]
    public class BilanController : AppControllerBase
    {
        public BilanController(IMediator mediator) : base(mediator)
        {

        }

        [HttpPost]
        [Route("", Name = "creerEvtBilan")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> CreerEvtBilanAsync([FromBody] CreerEvtBilanCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }

        [HttpPut]
        [Route("{id:int}", Name = "modifierEvtBilan")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> ModifierEvtBilanAsync([FromRoute] int id, [FromBody] ModifierEvtBilanCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
