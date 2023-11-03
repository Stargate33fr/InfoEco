using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.EtatDesLieux
{
    public class ObtenirEtatDesLieuxQueryHandler : QueryHandlerBase<ObtenirEtatDesLieuxQuery, DetailEtatDesLieuxResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirEtatDesLieuxQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailEtatDesLieuxResponse> Handle(ObtenirEtatDesLieuxQuery request, CancellationToken cancellationToken)
        {
            var etatDesLieux = await _iInfoEcoService.ObtientEtatDesLieuxAsync(request.LocataireAppartementId, cancellationToken);

            if (etatDesLieux == null)
            {
                throw new Exception("l'état des lieux n'existe pas");
            }
            return new DetailEtatDesLieuxResponse
            {
                Contenu = Mapper.Map<EtatDesLieuxViewModel>(etatDesLieux)
            };
        }
    }
}
