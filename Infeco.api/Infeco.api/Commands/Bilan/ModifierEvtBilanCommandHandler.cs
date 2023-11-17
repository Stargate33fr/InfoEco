using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Bilan
{
    public class ModifierEvtBilanCommandHandler : CommandHandlerBase<ModifierEvtBilanCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public ModifierEvtBilanCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(ModifierEvtBilanCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(ModifierEvtBilanCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null)
                {
                    var bilan = Mapper.Map<BilanEntite>(commande);
                    await _iInfoEcoService.ModifierEvtBilan(bilan);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}