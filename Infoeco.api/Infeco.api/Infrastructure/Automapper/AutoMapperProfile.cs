using AutoMapper;
using Infeco.Api.Commands.Appartements;
using Infeco.Api.Commands.Locataire;
using Infeco.Api.Commands.LocataireAppartement;
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
            CreateMap<CreerLocataireCommand, LocataireEntite>();
            CreateMap<LocataireAppartementEntite, LocataireAppartementViewModel>();
            CreateMap<TypeAppartementEntite, TypeAppartementViewModel>();
            CreateMap<AgenceImmobiliereEntite, AgenceImmobiliereViewModel>();
            CreateMap<AssignerAppartementCommand, LocataireAppartementEntite>();
            CreateMap<LocataireAppartementEntite, VilleViewModel>();
            CreateMap<LocataireEntite, LocataireViewModel>();
            CreateMap<PaiementEntite, PaiementViewModel>();
            CreateMap<ProvenancePaiementEntite, ProvenancePaiementViewModel>();
            CreateMap<TriModel, TriRequest>();
            CreateMap<PaginationModel, PaginationRequest>();
            CreateMap<ObtenirAppartementsQueryBase, GetAppartementsParCriteresRequest>();
            CreateMap<CiviliteEntite, CiviliteViewModel>();
        }
    }
}
