using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.Paiement
{
    public class ObtenirTousPaiementQueryHandler : QueryHandlerBase<ObtenirTousPaiementQuery, DetailPaiementTousResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirTousPaiementQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailPaiementTousResponse> Handle(ObtenirTousPaiementQuery request, CancellationToken cancellationToken)
        {
            var paiements = await _iInfoEcoService.ObtientPaiementTousAsync(request.LocataireAppartementId, cancellationToken);

            return new DetailPaiementTousResponse
            {
                Contenu = Mapper.Map<List<PaiementViewModel>>(paiements)
            };
        }
    }
}
