using MediatR;

namespace Infeco.Api.Queries
{
    public class ObtenirUtilisateurParIdentifiantEtMotDePasseQuery : IRequest<DetailUtilisateurResponse?>
    {
        public string? Identifiant { get; set; }
        public string? MotDePasse { get; set; }
    }
}
