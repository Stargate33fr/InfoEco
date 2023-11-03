using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.Paiement;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.Services;


namespace Infeco.Api.Commands.QuittanceLoyer
{
    public class SupprimerQuittanceLoyerCommandHandler : CommandHandlerBase<SupprimerQuittanceLoyerCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public SupprimerQuittanceLoyerCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(SupprimerQuittanceLoyerCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(SupprimerQuittanceLoyerCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null && commande.LocataireAppartementId.HasValue)
                {
                    var quittance = await _iInfoEcoService.ObtientQuittanceLoyersParIdAsync(commande.Id, commande.LocataireAppartementId.Value, cancellationToken);
                    if (quittance == null)
                    {
                        throw new Exception($"Cettye quittance avec avec cet id {commande.Id} n'existe pas");
                    }
                    await _iInfoEcoService.SupprimerQuittanceLoyerAsync(quittance);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

