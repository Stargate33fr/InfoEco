using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.QuittanceLoyer
{
    public class CreerQuittanceLoyerCommandHandler : CommandHandlerBase<CreerQuittanceLoyerCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;


        public CreerQuittanceLoyerCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerQuittanceLoyerCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerQuittanceLoyerCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                var quittance = Mapper.Map<QuittanceLoyerEntite>(commande);

                var resultat = await _iInfoEcoService.AjoutQuittanceLoyer(quittance);
                if (resultat != null)
                {
                    commande.Id = resultat.Id;
                }
            }
        }
    }
}

