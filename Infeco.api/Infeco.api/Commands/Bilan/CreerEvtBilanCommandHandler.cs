using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Bilan
{
    public class CreerEvtBilanCommandHandler : CommandHandlerBase<CreerEvtBilanCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public CreerEvtBilanCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerEvtBilanCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerEvtBilanCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                var bilan = Mapper.Map<BilanEntite>(commande);

                var resultat = await _iInfoEcoService.AjoutEvtBilan(bilan);
                if (resultat != null)
                {
                    commande.Id = resultat.Id;
                }
            }
        }
    }
}