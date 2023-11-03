using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Locataires
{
    public class ModifierLocataireCommandHandler : CommandHandlerBase<ModifierLocataireCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public ModifierLocataireCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(ModifierLocataireCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(ModifierLocataireCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null)
                {
                    var Locataire = Mapper.Map<LocataireEntite>(commande);
                    await _iInfoEcoService.ModifierLocataireAsync(Locataire);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

