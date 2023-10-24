using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Paiement
{
    public class CreerPaiementCommandHandler : CommandHandlerBase<CreerPaiementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public CreerPaiementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerPaiementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerPaiementCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                var paiement = Mapper.Map<PaiementEntite>(commande);

                var resultat = await _iInfoEcoService.AjoutPaiement(paiement);
                if (resultat != null)
                {
                    commande.Id = resultat.Id;
                }
            }
        }
    }
}

