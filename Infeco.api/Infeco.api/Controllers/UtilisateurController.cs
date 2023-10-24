using Infeco.Api.Queries.Appartements;
using Infeco.Api.Queries.Utilisateur;
using Infeco.Api.ViewModels.Habilitations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infeco.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [Authorize]
    [Route("utilisateur")]
    public class UtilisateurController : AppControllerBase
    {

        public UtilisateurController(IMediator mediator)
          : base(mediator)
        {
        }

        [HttpGet]
        [Route("{mail}", Name = "obtenirUtilisateurParEmail")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UtilisateurViewModel>> ObtenirUtilisateurEmailAsync([FromRoute] string mail, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new ObtenirUtilisateurParEmailQuery
            {
                Mail = mail
            }, cancellationToken)
            );
        }
    } 
}
