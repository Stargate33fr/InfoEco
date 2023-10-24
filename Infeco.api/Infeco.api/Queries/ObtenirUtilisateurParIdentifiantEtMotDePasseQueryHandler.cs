using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModels.Habilitations;
using Infoeco.Services;

namespace Infeco.Api.Queries
{
    public class ObtenirUtilisateurParIdentifiantEtMotDePasseQueryHandler : QueryHandlerBase<ObtenirUtilisateurParIdentifiantEtMotDePasseQuery, DetailUtilisateurResponse?>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirUtilisateurParIdentifiantEtMotDePasseQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailUtilisateurResponse?> Handle(ObtenirUtilisateurParIdentifiantEtMotDePasseQuery request, CancellationToken cancellationToken)
        {
            if (request.Identifiant!=null && request.MotDePasse != null)
            {
                var utilisateurGlobal = await _iInfoEcoService.ObtientParIdentifiantEtMotDePasseAsync(request.Identifiant, request.MotDePasse, cancellationToken);

                if (utilisateurGlobal == null)
                {
                    // TODO : vérifier le nombre de tentatives erronées, et s'il s'agit bien d'un utilisateur qui existe, si le nombre max d'erreurs est atteint, on le verrouille
                    // var ug = _utilisateurGlobalNHibernateRepository.ObtientParIdentifiant(login.Username);
                }

                return new DetailUtilisateurResponse
                {
                    Contenu = Mapper.Map<UtilisateurViewModel>(utilisateurGlobal)
                };
            }
            return null; ;
        }
    }
}
