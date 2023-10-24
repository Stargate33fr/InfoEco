using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.Queries.DonneesReference;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.DonneesReference
{
    public class ObtenirCivilitesQueryHandler : QueryHandlerBase<ObtenirCivilitesQuery, CivilitesResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirCivilitesQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<CivilitesResponse> Handle(ObtenirCivilitesQuery request, CancellationToken cancellationToken)
        {
            var civilites = await _iInfoEcoService.ObtientCivilites(cancellationToken);

            return new CivilitesResponse
            {
                Contenu = Mapper.Map<IReadOnlyCollection<CiviliteViewModel>>(civilites),
                Total= civilites.Count
            };
        }
    }
}
