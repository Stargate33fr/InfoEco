using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.DonneesReference
{
    public class ObtenirTypePaiementsQueryHandler : QueryHandlerBase<ObtenirTypePaiementsQuery, TypePaiementsResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirTypePaiementsQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<TypePaiementsResponse> Handle(ObtenirTypePaiementsQuery request, CancellationToken cancellationToken)
        {
            var typePaiements = await _iInfoEcoService.ObtientTypePaiements(cancellationToken);

            return new TypePaiementsResponse
            {
                Contenu = Mapper.Map<IReadOnlyCollection<TypePaiementViewModel>>(typePaiements),
                Total= typePaiements.Count
            };
        }
    }
}
