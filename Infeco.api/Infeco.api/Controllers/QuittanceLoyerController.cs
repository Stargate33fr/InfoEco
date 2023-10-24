using Infeco.Api.Commands.Paiement;
using Infeco.Api.Commands.QuittanceLoyer;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("qittanceLoyer")]
    public class QuittanceLoyerController : AppControllerBase
    {
        public QuittanceLoyerController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpPost]
        [Route("", Name = "creerQuittanceLoyer")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocataireAppartementViewModel>> CreerQuittanceLoyerAsync([FromBody] CreerQuittanceLoyerCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
