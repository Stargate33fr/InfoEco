using Infeco.Api.Commands.LocataireAppartement;
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
    [Route("etatsDesLieux")]
    public class EtatDesLieuxController : AppControllerBase
    {
        public EtatDesLieuxController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpPost]
        [Route("", Name = "creerUnEtatsDesLieux")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocataireAppartementViewModel>> CreerUnEtatsDesLieuxAsync([FromBody] AssignerAppartementCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
