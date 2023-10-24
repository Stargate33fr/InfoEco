using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.DonneesReference
{
    public class ObtenirTypeAppartementsQueryHandler : QueryHandlerBase<ObtenirTypeAppartementsQuery, TypeAppartementsResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirTypeAppartementsQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<TypeAppartementsResponse> Handle(ObtenirTypeAppartementsQuery request, CancellationToken cancellationToken)
        {
            var typeAppartements = await _iInfoEcoService.ObtientTypeAppartements(cancellationToken);

            return new TypeAppartementsResponse
            {
                Contenu = Mapper.Map<IReadOnlyCollection<TypeAppartementViewModel>>(typeAppartements),
                Total= typeAppartements.Count
            };
        }
    }
}
