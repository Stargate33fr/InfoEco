using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.QuittanceLoyer
{
    public class ObtenirQuittanceLoyersQueryHandler : QueryHandlerBase<ObtenirQuittanceLoyersQuery, DetailQuittanceLoyersResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirQuittanceLoyersQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailQuittanceLoyersResponse> Handle(ObtenirQuittanceLoyersQuery request, CancellationToken cancellationToken)
        {
            var quittancesLoyer = await _iInfoEcoService.ObtientQuittanceLoyersAsync(request.LocataireAppartementId, cancellationToken);

            return new DetailQuittanceLoyersResponse
            {
                Contenu = Mapper.Map<List<QuittanceLoyersViewModel>>(quittancesLoyer)
            };
        }
    }
}
