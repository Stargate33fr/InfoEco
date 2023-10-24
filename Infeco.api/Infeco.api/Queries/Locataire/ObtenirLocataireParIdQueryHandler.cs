using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.Locataires
{
    public class ObtenirLocataireParIdQueryHandler : QueryHandlerBase<ObtenirLocataireParIdQuery, DetailLocataireResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirLocataireParIdQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailLocataireResponse> Handle(ObtenirLocataireParIdQuery request, CancellationToken cancellationToken)
        {
            var locataire = await _iInfoEcoService.ObtientLocataireParIdAsync(request.LocataireId, cancellationToken);

            if (locataire == null)
            {
                throw new Exception("le locataire n'existe pas");
            }
            return new DetailLocataireResponse
            {
                Contenu = Mapper.Map<LocataireViewModel>(locataire)
            };
        }
    }
}
