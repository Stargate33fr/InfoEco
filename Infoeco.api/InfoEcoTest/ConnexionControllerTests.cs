using FluentAssertions;
using IDSoft.CrmWelcome.Api.Controllers;
using Infeco.Api.Infrastructure.Authentication;
using Infeco.Api.Queries;
using Infeco.Api.ViewModels.Habilitations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Moq;
using System.Text;

namespace InfoEco.Api.Test
{
    public class ConnexionControllerTest
    {
        private readonly Mock<ITokenService> _mockTokenService;

        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<ILogger<ConnexionController>> _mockLogger;

        private const string AccessToken = "accessToken";

        public ConnexionControllerTest()
        {
            _mockTokenService = new Mock<ITokenService>();
            _mockTokenService.Setup(_ => _.DonneAccessToken(It.IsAny<UtilisateurViewModel>())).Returns(AccessToken);

            _mockMediator = new Mock<IMediator>();

            _mockLogger = new Mock<ILogger<ConnexionController>>();
        }

        [Fact]
        public async Task PeutRepondreOkAUneDemandeDeLoginCorrecteAsync()
        {
            // arrange
            _mockMediator.Setup(_ => _.Send(It.IsAny<ObtenirUtilisateurParIdentifiantEtMotDePasseQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DetailUtilisateurResponse()
            {
                Contenu = new UtilisateurViewModel()
                {
                    Courriel = "test@test.com",
                    EstActif = true,
                    Nom = "Nom",
                    Prenom = "Prenom",
                    Password = "Password",
                    Id = 1,
                    AgenceImmobiliereId = 1
                }
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add(HeaderNames.Authorization, "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("nimporte:quoi")));
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            // act
            var controller = new ConnexionController(_mockTokenService.Object, _mockLogger.Object, _mockMediator.Object) { ControllerContext = controllerContext };

            var response = await controller.LoginAsync(CancellationToken.None);

            // assert
            response.Should().NotBeNull();
            response.GetType().Should().Be(typeof(OkObjectResult));
            var okContent = response as OkObjectResult;
            okContent.Should().NotBeNull();
            okContent?.Value.Should().NotBeNull();
            var accessTokenObject = new { accessToken = AccessToken };
            okContent?.Value.Should().BeEquivalentTo(accessTokenObject);
        }

        [Fact]
        public async Task PeutRepondreUnauthorizedAUneDemandeDeLoginIncorrecteAsync()
        {
            _mockMediator.Setup(_ => _.Send(It.IsAny<ObtenirUtilisateurParIdentifiantEtMotDePasseQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DetailUtilisateurResponse()
            {
                Contenu = null
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add(HeaderNames.Authorization, "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("nimporte:quoi")));
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new ConnexionController(_mockTokenService.Object, _mockLogger.Object, _mockMediator.Object) { ControllerContext = controllerContext };

            var result = await controller.LoginAsync(new CancellationToken());

            result.Should().NotBeNull();
            result.GetType().Should().Be(typeof(UnauthorizedResult));
        }

        [Fact]
        public async Task PeutRepondreUnauthorizedAUneDemandeDeLoginCorrecteMaisUtilisateurInactifAsync()
        {
            _mockMediator.Setup(_ => _.Send(It.IsAny<ObtenirUtilisateurParIdentifiantEtMotDePasseQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DetailUtilisateurResponse()
            {
                Contenu = new UtilisateurViewModel()
                {
                    Courriel = "test@test.com",
                    EstActif = false,
                    Nom = "Nom",
                    Prenom = "Prenom",
                    Password = "Password",
                    Id = 1,
                    AgenceImmobiliereId = 1
                }
            });

            var httpContext = new DefaultHttpContext();
            httpContext.Request.Headers.Add(HeaderNames.Authorization, "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes("nimporte:quoi")));
            var controllerContext = new ControllerContext()
            {
                HttpContext = httpContext,
            };

            var controller = new ConnexionController(_mockTokenService.Object, _mockLogger.Object, _mockMediator.Object) { ControllerContext = controllerContext };

            var response = await controller.LoginAsync(CancellationToken.None);

            response.Should().NotBeNull();
            response.GetType().Should().Be(typeof(UnauthorizedResult));
        }
    }
}
