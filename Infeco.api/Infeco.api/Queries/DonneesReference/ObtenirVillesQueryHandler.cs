using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.DonneesReference
{
    public class ObtenirVillesQueryHandler : QueryHandlerBase<ObtenirVillesQuery, VillesResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirVillesQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<VillesResponse> Handle(ObtenirVillesQuery request, CancellationToken cancellationToken)
        {
            var villes = await _iInfoEcoService.ObtientVilleParNom(request.NomRecherche, cancellationToken);

            return new VillesResponse
            {
                Contenu = Mapper.Map<IReadOnlyCollection<VilleViewModel>>(villes),
                Total= villes.Count
            };
        }
    }
}
