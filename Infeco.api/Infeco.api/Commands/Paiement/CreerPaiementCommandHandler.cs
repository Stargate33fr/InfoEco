using AutoMapper;
using FluentValidation.Results;
using Infeco.Api.Commands.QuittanceLoyer;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.Queries.Paiement;
using Infoeco.infrastructure.Entities;
using Infoeco.Services;
using Infoeco.Services.Implementation;
using Microsoft.AspNetCore.Http;

namespace Infeco.Api.Commands.Paiement
{
    public class CreerPaiementCommandHandler : CommandHandlerBase<CreerPaiementCommand>
    {
        private readonly IInfoEcoService _iInfoEcoService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IVerifieurReferencesService _verifieurReferenceService;
        private readonly ILoggerFactory _loggerFactory;

        public CreerPaiementCommandHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor, IVerifieurReferencesService verifieurReferenceService, ILoggerFactory loggerFactory) : base(mapper, httpContextAccessor, verifieurReferenceService, loggerFactory)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _verifieurReferenceService= verifieurReferenceService ?? throw new ArgumentNullException(nameof(verifieurReferenceService));
            _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
        }

        protected override List<Func<Task<ValidationFailure>>>? DefinitLesVerifieurs(CreerPaiementCommand commande, CancellationToken cancellationToken)
        {
            return null;
        }

        protected override async Task ExecuteCommandeAsync(CreerPaiementCommand commande, CancellationToken cancellationToken)
        {
            if (HttpContextAccessor.HttpContext != null)
            {
                var paiement = Mapper.Map<PaiementEntite>(commande);

                var resultat = await _iInfoEcoService.AjoutPaiement(paiement);

                if (commande.GenererQuittanceLoyer)
                {
                    var creerQuittanceLoyerCommand = new CreerQuittanceLoyerCommand();
                    creerQuittanceLoyerCommand.Annee = commande.DatePaiement.ToLocalTime().Year;
                    creerQuittanceLoyerCommand.Mois = commande.DatePaiement.ToLocalTime().Month;
                    creerQuittanceLoyerCommand.LocataireAppartementId = commande.LocataireAppartementId;
                    // on regarde si il n'y a pas eu un montant CAF avant
                    var queryCaf = new ObtenirTousPaiementQuery();
                    queryCaf.LocataireAppartementId = commande.LocataireAppartementId;

                    var queryCafHandler = new ObtenirTousPaiementQueryHandler(_iInfoEcoService, _mapper, _httpContextAccessor);
                    var resultatCaf = await queryCafHandler.Handle(queryCaf, cancellationToken);

                    if (resultatCaf != null && resultatCaf.Contenu.Count > 0)
                    {
                       var resultatPaiements =  resultatCaf.Contenu.Where(u => u.DatePaiement.Month == commande.DatePaiement.Month && u.DatePaiement.Year == commande.DatePaiement.Year).ToList();
                       if (resultatPaiements.Count > 0)
                       {
                            creerQuittanceLoyerCommand.Montant = resultatPaiements.Where(u => u.TypePaiement!=null && u.TypePaiement.Nom != "Dépot de garantie").Sum(u => u.Montant);
                        }
                    }
                    var creerQuittanceLoyerCommandHandler = new CreerQuittanceLoyerCommandHandler(_iInfoEcoService, _mapper, _httpContextAccessor, _verifieurReferenceService, _loggerFactory);
                    await creerQuittanceLoyerCommandHandler.Handle(creerQuittanceLoyerCommand, cancellationToken);
                }

                if (resultat != null)
                {
                    commande.Id = resultat.Id;
                }
            }
        }
    }
}

