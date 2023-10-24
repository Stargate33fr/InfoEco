using FluentAssertions;
using Infeco.Api.Controllers;
using Infeco.Api.Queries.Appartements;
using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoEco.Api.Test
{
    public class LocataireControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        public LocataireControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task PeutRenvoyerUnLocataireAsync()
        {
            // arrange
            _mockMediator.Setup(_ => _.Send(It.IsAny<ObtenirLocataireParIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DetailLocataireResponse()
            {
                Contenu = new LocataireViewModel
                {
                    Id = 1,
                    Nom = "Nom Locataire",
                    Prenom = "Prenom Locataire",
                    Mail = "nom.prenom@test.fr",
                    Telephone = "0600000000",
                    DateNaissance = new DateTime(1978, 2, 26),
                    IBAN = "FRXXXXXXXXXXXXXXXXXX"
                }
            }); ;

            // act
            var controller = new LocataireController(_mockMediator.Object);
            var response = await controller.ObtenirLocataireParIdAsync(1, CancellationToken.None);

            // assert
            response?.Result.Should().NotBeNull();
            var okContent = response?.Result as OkObjectResult;
            if (okContent != null)
            {
                okContent.StatusCode.Should().Be(200);
            }

            if (response?.Result != null)
            {
                var locataire = (((OkObjectResult)response.Result).Value as DetailLocataireResponse)?.Contenu;
                if (locataire != null)
                {
                    locataire.Id.Should().Be(1);
                    locataire.Nom.Should().Be("Nom Locataire");
                    locataire.Prenom.Should().Be("Prenom Locataire");
                    locataire.Telephone.Should().Be("0600000000");
                    locataire.DateNaissance.Should().Be(new DateTime(1978, 2, 26));
                    locataire.IBAN.Should().Be("FRXXXXXXXXXXXXXXXXXX");
                    locataire.Mail.Should().Be("nom.prenom@test.fr");
                }
            }
        }
    }
}
