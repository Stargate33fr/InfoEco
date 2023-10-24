using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.ViewModel;
using Infoeco.Services;

namespace Infeco.Api.Queries.Appartements
{
    public class ObtenirAppartementParIdQueryHandler : QueryHandlerBase<ObtenirAppartementParIdQuery, DetailAppartementResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public ObtenirAppartementParIdQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailAppartementResponse> Handle(ObtenirAppartementParIdQuery request, CancellationToken cancellationToken)
        {
            var appartement = await _iInfoEcoService.ObtientAppartementParIdAsync(request.AppartementId, cancellationToken);

            return new DetailAppartementResponse
            {
                Contenu = Mapper.Map<AppartementViewModel>(appartement)
            };
        }
    }
}
