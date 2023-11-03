using AutoMapper;
using Infeco.Api.Infrastructure.MediatR;
using Infeco.Api.Queries.Locataire;
using Infeco.Api.ViewModel;
using Infoeco.Services;
using InfoEco.Domain.Request;

namespace Infeco.Api.Queries.LocataireAppartement
{
    public class RechercheLocataireAppartementQueryHandler : QueryHandlerBase<RechercheLocataireAppartementQuery, DetailLocataireAppartementResponse>
    {
        private readonly IInfoEcoService _iInfoEcoService;

        public RechercheLocataireAppartementQueryHandler(IInfoEcoService iInfoEcoService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(mapper, httpContextAccessor)
        {
            _iInfoEcoService = iInfoEcoService ?? throw new ArgumentNullException(nameof(iInfoEcoService));
        }

        public override async Task<DetailLocataireAppartementResponse> Handle(RechercheLocataireAppartementQuery request, CancellationToken cancellationToken)
        {
            var requestService = new GetLocatairesAppartementRequest();
            requestService.AppartementId = request.AppartementId;
            requestService.Id = request.Id;
            requestService.LocataireId = request.LocataireId;
            requestService.MailLocataire = request.Email;
            requestService.VilleId = request.VilleId;

            var locatairesAppartement = await _iInfoEcoService.RechercheLocataireAppartementAsync(requestService);

            if (locatairesAppartement == null)
            {
                throw new Exception("il n'existe aucun locataire ou appartement avec ces critères");
            }

            var listLocataireAppartViewModel = new List<LocataireAppartementViewModel>();

            foreach (var locataireAppartement in locatairesAppartement)
            {
                if (listLocataireAppartViewModel.Count==0 || listLocataireAppartViewModel.FirstOrDefault(u => u.AppartementId == locataireAppartement.AppartementId) == null)
                {
                    var locataireAppartViewModel = new LocataireAppartementViewModel();
                    locataireAppartViewModel.AppartementId = locataireAppartement.AppartementId;
                    locataireAppartViewModel.Appartement = Mapper.Map<AppartementViewModel>(locataireAppartement.Appartement);
                    locataireAppartViewModel.DepotDeGarantieRegle = locataireAppartement.DepotDeGarantieRegle;
                    locataireAppartViewModel.Locataires = new List<LocataireViewModel>();
                    locataireAppartViewModel.Id = locataireAppartement.Id;
                    listLocataireAppartViewModel.Add(locataireAppartViewModel);
                }
            }
         
            foreach (var appartLocataire in locatairesAppartement)
            {
                var elementAppartement = listLocataireAppartViewModel.FirstOrDefault(u => u.AppartementId == appartLocataire.AppartementId);
                if (elementAppartement != null && appartLocataire.Locataire!=null)
                {
                    var locataire = Mapper.Map<LocataireViewModel>(appartLocataire.Locataire);
                    locataire.DateEntree = appartLocataire.DateEntree;
                    locataire.DateSortie = appartLocataire.DateSortie;
                    elementAppartement.Locataires.Add(locataire);
                }
            }

            return new DetailLocataireAppartementResponse
            {
                Contenu = listLocataireAppartViewModel
            };
        }
    }
}
