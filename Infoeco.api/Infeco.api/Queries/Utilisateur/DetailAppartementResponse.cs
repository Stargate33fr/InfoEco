using Infeco.Api.ViewModel;
using Infeco.Api.ViewModels.Habilitations;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries.Utilisateur
{
    public class DetailUtilisateurResponse : IResponse<UtilisateurViewModel>
    {
        public required UtilisateurViewModel Contenu { get; set; }
    }
}
