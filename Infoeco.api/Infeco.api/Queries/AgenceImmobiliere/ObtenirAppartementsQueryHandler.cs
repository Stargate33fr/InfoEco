using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;
using InfoEco.Domain.Request;

namespace Infeco.Api.Queries.AgenceImmobiliere
{
    public class ObtenirAppartementsQueryHandler : QueryHandlerBase<ObtenirAppartementsQuery, AppartementsResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirAppartementsQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<AppartementsResponse> Handle(ObtenirAppartementsQuery request, CancellationToken cancellationToken)
        {
            var criteres = Mapper.Map<ObtenirAppartementsQueryBase, GetAppartementsParCriteresRequest>(request);
            criteres.Tri = Mapper.Map<TriRequest>(request.Tri);
            criteres.Pagination = Mapper.Map<PaginationRequest>(request.Pagination);

            var appartements = await _iInfoEcoService.RechercheAppartementsAsync(criteres, cancellationToken);
            var appartementsTotal = await _iInfoEcoService.GetCountAppartementsAsync(criteres, cancellationToken);

            if (appartements == null)
            {
                throw new Exception("le locataire n'existe pas");
            }
            return new AppartementsResponse
            {
                Contenu = Mapper.Map<IReadOnlyCollection<AppartementViewModel>>(appartements),
                Total = appartementsTotal
            };
        }
    }
}
