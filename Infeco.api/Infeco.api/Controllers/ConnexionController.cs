using Infeco.Api.Controllers;
using Infeco.Api.Infrastructure.Authentication;
using Infeco.Api.Infrastructure.Helpers;
using Infeco.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Net.Http.Headers;
using System.Text;

namespace IDSoft.CrmWelcome.Api.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    public class ConnexionController : AppControllerBase
    {
        private readonly ITokenService _tokenservice;
        private readonly ILogger<ConnexionController> _logger;

        public ConnexionController(ITokenService tokenService, ILogger<ConnexionController> logger, IMediator mediator)
            : base(mediator)
        {
            _tokenservice = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _logger = logger;
        }

        [HttpPost("login"), AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> LoginAsync(CancellationToken cancellationToken)
        {
            if (!AuthenticationHeaderValue.TryParse(Request.Headers[HeaderNames.Authorization], out var authHeader) || authHeader.Scheme != "Basic" || string.IsNullOrEmpty(authHeader.Parameter))
                return Unauthorized();

            try
            {
                if (authHeader!=null && authHeader.Parameter != null)
                {
                    var userNameAndPassword = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
                    var userName = userNameAndPassword[0];
                    var password = userNameAndPassword[1];
               
                    var response = await Mediator.Send(new ObtenirUtilisateurParIdentifiantEtMotDePasseQuery
                    {
                        Identifiant = userName,
                        MotDePasse = Encryption.Encrypt(password)
                    }, cancellationToken);
                    if (response == null || (response!=null && response.Contenu!=null && !response.Contenu.EstActif))
                        return Unauthorized();
                    if (response != null && response.Contenu != null)
                    {
                        response.Contenu.Password = Encryption.Encrypt(password);
                      
                        return Ok(new { accessToken = _tokenservice.DonneAccessToken(response.Contenu) });
                    }
                    return Unauthorized();
                }
                return Unauthorized();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Erreur");
                return Unauthorized();
            }
        }
    }
}