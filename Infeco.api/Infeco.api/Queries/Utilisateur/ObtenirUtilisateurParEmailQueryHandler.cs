using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infeco.Api.ViewModels.Habilitations;
using Infoeco.Services;

namespace Infeco.Api.Queries.Utilisateur
{
    public class ObtenirUtilisateurParIdQueryHandler : QueryHandlerBase<ObtenirUtilisateurParEmailQuery, DetailUtilisateurResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirUtilisateurParIdQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailUtilisateurResponse> Handle(ObtenirUtilisateurParEmailQuery request, CancellationToken cancellationToken)
        {
            var Utilisateur = await _iInfoEcoService.ObtientUtilisateurParEmailAsync(request.Mail, cancellationToken);

            return new DetailUtilisateurResponse
            {
                Contenu = Mapper.Map<UtilisateurViewModel>(Utilisateur)
            };
        }
    }
}
