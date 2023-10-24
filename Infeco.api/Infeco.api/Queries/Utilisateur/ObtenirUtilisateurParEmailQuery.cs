using MediatR;

namespace Infeco.Api.Queries.Utilisateur
{
    public class ObtenirUtilisateurParEmailQuery : IRequest<DetailUtilisateurResponse>
    {
        public string Mail { get; set; } = string.Empty;
    }
}
