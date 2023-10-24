using Infeco.Api.Commands.LocataireAppartement;
using Infeco.Api.Queries.Appartements;
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
    [Route("LocatairesAppartement")]
    public class LocataireAppartementController : AppControllerBase
    {
        public LocataireAppartementController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpPost]
        [Route("recherche", Name = "obtenirLocataireAppartementParLocataireId")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocataireAppartementViewModel>> ObtenirLocataireAppartementParLocataireIdAsync([FromBody] RechercheLocataireAppartementQuery recherche, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(recherche, cancellationToken));
        }

        [HttpPost]
        [Route("assignerAppartement", Name = "assignerLocataireAUnAppartement")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<LocataireAppartementViewModel>> AssignerLocataireAUnAppartementAsync([FromBody] AssignerAppartementCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok(new ResponseCreation(command.Id));
        }
    }
}
