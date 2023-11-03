using Infeco.Api.Commands.Locataire;
using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{

    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("locataires")]
    public class LocataireController : AppControllerBase
    {

        public LocataireController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{id:int}", Name = "obtenirLocataireParId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocataireViewModel>> ObtenirLocataireParIdAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirLocataireParIdQuery
            {
                LocataireId = id
            }, cancellationToken));
        }

        [HttpPost]
        [Route("", Name = "creerUnLocataire")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ResponseCreation>> CreerUnLocataireAsync([FromBody] CreerLocataireCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }

        [HttpPut]
        [Route("{id:int}", Name = "modifierUnLocataire")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task ModifierUnLocataireAsync([FromRoute] int id, [FromBody] ModifierLocataireCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await Mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        [Route("{id:int}", Name = "SupprimerUnLocataire")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task SupprimerUnLocataireAsync([FromRoute] int id, CancellationToken cancellationToken)
        {
            var command = new SupprimerLocataireCommand
            {
                Id = id
            };
     
            await Mediator.Send(command, cancellationToken);
        }
    }
}
