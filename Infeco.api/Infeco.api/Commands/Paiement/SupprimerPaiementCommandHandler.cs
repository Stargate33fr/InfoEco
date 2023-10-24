using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Commands.Paiement;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;
using InfoEco.Domain.Request;

namespace Infeco.Api.Commands.Appartements
{
    public class SupprimerPaiementCommandHandler : CommandHandlerBase<SupprimerPaiementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public SupprimerPaiementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(SupprimerPaiementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(SupprimerPaiementCommand commande, CancellationToken cancellationToken)
        {
            try
            {
                if (HttpContextAccessor.HttpContext != null)
                {
                    var paiement = await _iInfoEcoService.ObtientPaiementParIdEtlocataireIdAsync(commande.Id,commande.LocataireId, cancellationToken);
                    if (paiement == null)
                    {
                        throw new Exception($"Ce paiement avec cet id {commande.Id} n'existe pas");
                    }
                    await _iInfoEcoService.SupprimerPaiementAsync(paiement);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

