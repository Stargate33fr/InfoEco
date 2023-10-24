using AutoMapper;
using FluentAssertions;
using FluentValidation;
using Infeco.Api.Commands.Appartements;
using Infeco.Api.Infrastructure.AutoMapper;
using Infoeco.infrastructure;
using Infoeco.Services;
using Infoeco.Services.Implementation;
using InfoEco.Domain.Authentification;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Linq;
using System.Security.Claims;

namespace InfoEco.Api.Test.Commands
{
    public class CreerAppartementCommandHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILoggerFactory _loggerFactory;
        private readonly IInfoEcoService _infoEcoService;
        private readonly IVerifieurReferencesService _verifieurReferenceService;

        public CreerAppartementCommandHandlerTest()
        {
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            _mapper = new Mapper(configuration);


            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
           
            var claims = new List<Claim>()
            {
                new Claim(InfecoClaimTypes.AgenceImmobiliereId, "1"),
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");

            var claimsPrincipal = new ClaimsPrincipal(identity);

            mockHttpContextAccessor.Setup(req => req.HttpContext.User).Returns(claimsPrincipal);

            _httpContextAccessor = mockHttpContextAccessor.Object;

            _verifieurReferenceService = new VerifieurReferencesService();

            _loggerFactory = new LoggerFactory();
                    

            var options = new DbContextOptionsBuilder<InfoEcoDbContext>()
                .UseInMemoryDatabase(databaseName: "dbContext")
                .Options;

            var mockContext = new InfoEcoDbContext(options);
           
            _infoEcoService = new InfoEcoService(mockContext);
        }

        [Fact]
        public async Task PeutCreerUnAppartementAsync()
        {
            var command = new CreerAppartementCommand
            {
                VilleId = 1,
                Adresse = "adresse",
                Loyer = 500,
                PrixDesCharges = 125.50,
                DepotDeGarantie = 500,
            };

            var handler = new CreerAppartementCommandHandler(_infoEcoService, _mapper, _httpContextAccessor, _verifieurReferenceService, _loggerFactory);
            await handler.Handle(command, CancellationToken.None);
            command.Id.Should().Be(1);
        }

        [Fact]
        public async Task NePeutCreerUnAppartementAsync()
        {
            var command = new CreerAppartementCommand();

            var handler = new CreerAppartementCommandHandler(_infoEcoService, _mapper, _httpContextAccessor, _verifieurReferenceService, _loggerFactory);
            await handler.Handle(command, CancellationToken.None);
            command.Id.Should().Be(1);
        }
    }
}
