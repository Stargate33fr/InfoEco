
using MediatR;
using Infeco.Api.ViewModels.Habilitations;
using InfoEco.Domain.Abstractions.Queries;

namespace Infeco.Api.Queries
{
    public class DetailUtilisateurResponse : IResponse<UtilisateurViewModel>
    {
        public UtilisateurViewModel? Contenu { get; set; }
    }
}