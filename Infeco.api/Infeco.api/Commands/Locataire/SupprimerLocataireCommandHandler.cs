using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.Services;

namespace Infeco.Api.Commands.Locataires
{
    public class SupprimerLocataireCommandHandler : CommandHandlerBase<SupprimerLocataireCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public SupprimerLocataireCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(SupprimerLocataireCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(SupprimerLocataireCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null)
                {
                    var Locataire = await _iInfoEcoService.ObtientLocataireParIdAsync(commande.Id, cancellationToken);
                    if (Locataire != null)
                    {
                        await _iInfoEcoService.SupprimerLocataireAsync(Locataire);
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

