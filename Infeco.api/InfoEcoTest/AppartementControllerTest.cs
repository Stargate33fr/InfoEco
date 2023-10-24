using FluentAssertions;
using Infeco.Api.Controllers;
using Infeco.Api.Queries.Appartements;
using Infeco.Api.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace InfoEco.Api.Test
{
    public class AppartementControllerTest
    {
        private readonly Mock<IMediator> _mockMediator;

        public AppartementControllerTest()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [Fact]
        public async Task PeutRenvoyerUnAppartementAsync()
        {
            // arrange
            _mockMediator.Setup(_ => _.Send(It.IsAny<ObtenirAppartementParIdQuery>(), It.IsAny<CancellationToken>())).ReturnsAsync(new DetailAppartementResponse()
            {
                Contenu = new AppartementViewModel
                {
                    Id = 1,
                    Adresse = "Adresse",
                    AgenceImmobiliere = new AgenceImmobiliereViewModel() { Id = 1, Nom = "Nom Agence", Adresse = "Adresse Agence", FraisAgence = 600, Ville = new VilleViewModel() { Id = 1, Nom = "Paris" } },
                    DepotDeGarantie = 1500,
                    PrixDesCharges = 150,
                    Loyer = 1500,
                    Ville = new VilleViewModel() { Id = 1, Nom = "Paris" }
                }
            });

            // act
            var controller = new AppartementController(_mockMediator.Object);
            var response = await controller.ObtenirAppartementParIdAsync(1, CancellationToken.None);

            // assert
            response?.Result.Should().NotBeNull();
            var okContent = response?.Result as OkObjectResult;
            if (okContent != null)
            {
                okContent.StatusCode.Should().Be(200);
            }
           
            if (response?.Result != null)
            {
                var ff = (((OkObjectResult)response.Result).Value as DetailAppartementResponse)?.Contenu;
                if (ff != null)
                {
                    ff.Id.Should().Be(1);
                    ff.Loyer.Should().Be(1500);
                    if (ff?.Ville != null)
                    {
                        ff.Ville.Nom.Should().Be("Paris");
                    }
                   
                    if (ff?.AgenceImmobiliere != null)
                    {
                        ff.AgenceImmobiliere.Nom.Should().Be("Nom Agence");
                    }
                }
            }
        }
    }
}
