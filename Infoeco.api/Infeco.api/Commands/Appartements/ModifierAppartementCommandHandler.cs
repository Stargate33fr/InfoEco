using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Appartements
{
    public class ModifierAppartementCommandHandler : CommandHandlerBase<ModifierAppartementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public ModifierAppartementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(ModifierAppartementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(ModifierAppartementCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null)
                {
                    var idAgence = HttpContextAccessor.HttpContext.DonneIdAgenceUtilisateurAuthentifié();
                    var appartement = await _iInfoEcoService.ObtientAppartementParIdAsync(commande.Id, cancellationToken);
                    if (idAgence != null && appartement != null)
                    {
                        var appartementAModifier = Mapper.Map<AppartementEntite>(commande);
                        appartementAModifier.Id = commande.Id;
                        appartementAModifier.AgenceImmobiliereId = idAgence.Value;

                        await _iInfoEcoService.ModifierAppartementAsync(appartementAModifier);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

