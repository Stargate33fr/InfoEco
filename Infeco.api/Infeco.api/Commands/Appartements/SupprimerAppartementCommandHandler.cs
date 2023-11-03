using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;
using InfoEco.Domain.Exceptions;
using InfoEco.Domain.Request;

namespace Infeco.Api.Commands.Appartements
{
    public class SupprimerAppartementCommandHandler : CommandHandlerBase<SupprimerAppartementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public SupprimerAppartementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(SupprimerAppartementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(SupprimerAppartementCommand commande, CancellationToken cancellationToken)
        {

            if (HttpContextAccessor.HttpContext != null)
            {
                var locataireAppartement = await _iInfoEcoService.RechercheLocataireAppartementAsync(new GetLocatairesAppartementRequest
                {
                    AppartementId = commande.Id
                });

                if (locataireAppartement!=null && locataireAppartement.Count==0)
                {
                    var appartement = await _iInfoEcoService.ObtientAppartementParIdAsync(commande.Id, cancellationToken);
                    if (appartement != null)
                    {
                        await _iInfoEcoService.SupprimerAppartementAsync(appartement);
                    }
                    else
                    {
                        throw new GlobalException("Vous ne pouvez pas supprimer cet appartement car il n'existe pas");
                    }
                }
                else
                {
                    throw new GlobalException("Vous ne pouvez pas supprimer cet appartement car il y a un ou des locataire(s) dedans");
                }
            }
        }
    }
}

