using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.EtatDesLieux
{
    public class CreerEtatDesLieuxCommandHandler : CommandHandlerBase<CreerEtatDesLieuxCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public CreerEtatDesLieuxCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerEtatDesLieuxCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerEtatDesLieuxCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                var etatDesLieux = Mapper.Map<EtatDesLieuxEntite>(commande);

                var resultat = await _iInfoEcoService.AjoutEtatDesLieux(etatDesLieux);
                if (resultat != null)
                {
                    commande.Id = resultat.Id;
                }
            }
        }
    }
}

