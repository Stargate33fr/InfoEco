using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.Paiement
{
    public class ObtenirAppartementParIdQueryHandler : QueryHandlerBase<ObtenirPaiementQuery, DetailPaiementResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirAppartementParIdQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailPaiementResponse> Handle(ObtenirPaiementQuery request, CancellationToken cancellationToken)
        {
            var paiement = await _iInfoEcoService.ObtientPaiementParIdEtlocataireIdAsync(request.Id, request.LocataireId, cancellationToken);

            return new DetailPaiementResponse
            {
                Contenu = Mapper.Map<PaiementViewModel>(paiement)
            };
        }
    }
}
