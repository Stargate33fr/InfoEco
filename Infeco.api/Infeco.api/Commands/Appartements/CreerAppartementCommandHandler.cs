using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Appartements
{
    public class CreerAppartementCommandHandler : CommandHandlerBase<CreerAppartementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public CreerAppartementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerAppartementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerAppartementCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext !=null)
            {
                var idAgence = HttpContextAccessor.HttpContext.DonneIdAgenceUtilisateurAuthentifié();
                if (idAgence != null)
                {
                    var appartement = Mapper.Map<AppartementEntite>(commande);
                    appartement.AgenceImmobiliereId = idAgence.Value;

                    var resultat = await _iInfoEcoService.CreateAppartementAsync(appartement);

                    if (resultat != null)
                    {
                       commande.Id = resultat.Id;
                    }           
                }
            }
        }
    }
}

