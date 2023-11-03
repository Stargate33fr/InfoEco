using Infeco.Api.ViewModels.Habilitations;

namespace Infeco.Api.Infrastructure.Authentication
{
    public interface ITokenService
    {
        string? DonneAccessToken(UtilisateurViewModel utilisateur);
        string? DonneRefreshToken(string login);
    }
}
