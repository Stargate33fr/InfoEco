using AutoMapper;
using Infeco.Api.Commands.Appartements;
using Infeco.Api.Commands.Bilan;
using Infeco.Api.Commands.EtatDesLieux;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.LocataireAppartement;
using Infeco.Api.Commands.Paiement;
using Infeco.Api.Commands.QuittanceLoyer;
using Infeco.Api.Queries.AgenceImmobiliere;
using Infeco.Api.ViewModel;
using Infeco.Api.ViewModels;
using Infeco.Api.ViewModels.Habilitations;
using Infoeco.infrastructure.Entities;
using InfoEco.Domain.Request;

namespace Infeco.Api.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UtilisateurEntite, UtilisateurViewModel>();
            CreateMap<AppartementEntite, AppartementViewModel>();
            CreateMap<VilleEntite, VilleViewModel>();
            CreateMap<CreerAppartementCommand, AppartementEntite>();
            CreateMap<ModifierAppartementCommand, AppartementEntite>();
            CreateMap<ModifierLocataireCommand, LocataireEntite>();
            CreateMap<CreerLocataireCommand, LocataireEntite>();
            CreateMap<LocataireAppartementEntite, LocataireAppartementViewModel>();
            CreateMap<TypeAppartementEntite, TypeAppartementViewModel>();
            CreateMap<AgenceImmobiliereEntite, AgenceImmobiliereViewModel>();
            CreateMap<AssignerAppartementCommand, LocataireAppartementEntite>();
            CreateMap<LocataireAppartementEntite, VilleViewModel>();
            CreateMap<LocataireEntite, LocataireViewModel>();
            CreateMap<PaiementEntite, PaiementViewModel>();
            CreateMap<TypePaiementEntite, TypePaiementViewModel>();
            CreateMap<TriModel, TriRequest>();
            CreateMap<PaginationModel, PaginationRequest>();
            CreateMap<ObtenirAppartementsQueryBase, GetAppartementsParCriteresRequest>();
            CreateMap<CiviliteEntite, CiviliteViewModel>();
            CreateMap<QuittanceLoyerEntite, QuittanceLoyersViewModel>();
            CreateMap<EtatDesLieuxEntite, EtatDesLieuxViewModel>(); 
            CreateMap<CreerEtatDesLieuxCommand, EtatDesLieuxEntite>(); 
            CreateMap<CreerPaiementCommand, PaiementEntite>();
            CreateMap<CreerQuittanceLoyerCommand, QuittanceLoyerEntite>();
            CreateMap<CreerEvtBilanCommand, BilanEntite>();
            CreateMap<ModifierEvtBilanCommand, BilanEntite>();
        }
    }
}
