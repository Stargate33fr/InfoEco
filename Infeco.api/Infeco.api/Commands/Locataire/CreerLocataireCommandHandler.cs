using AutoMapper;
using FluentValidation.Results;
using IDSoft.CrmWelcome.Infrastructure.Helpers;
using Infeco.Api.Commands.Appartements;
using Infeco.Api.Infrastructure.MediatR;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;

namespace Infeco.Api.Commands.Locataire
{
    public class CreerLocataireCommandHandler : CommandHandlerBase<CreerLocataireCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        public CreerLocataireCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerLocataireCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerLocataireCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext !=null)
            {
                var idAgence = HttpContextAccessor.HttpContext.DonneIdAgenceUtilisateurAuthentifié();
                if (idAgence != null)
                {
                    var Locataire = Mapper.Map<LocataireEntite>(commande);

                    var resultat = await _iInfoEcoService.CreateLocataireAsync(Locataire);
                    if (resultat != null)
                    {
                        commande.Id = resultat.Id;
                    }
                }
            }
        }
    }
}

