using Infeco.Api.ViewModels.Habilitations;
using Infoeco.infrastructure.Configuration;
using InfoEco.Domain.Authentification;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infeco.Api.Infrastructure.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly AuthentificationSettings? _authentificationSettings;
        private readonly JwtOptions? _jwtOptions;
        private readonly byte[]? _secretJwt;

        public TokenService(IAppConfiguration appConfiguration)
        {
            if (appConfiguration!=null && appConfiguration.AuthentificationSettings!=null && appConfiguration.JwtOptions != null)
            {
                _authentificationSettings = appConfiguration.AuthentificationSettings;
                _jwtOptions = appConfiguration.JwtOptions;
                _secretJwt = Encoding.UTF8.GetBytes(appConfiguration.AuthentificationSettings.SecretKey);
            }
        }

        public string? DonneAccessToken(UtilisateurViewModel utilisateur)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, utilisateur.Courriel),
                new Claim(InfecoClaimTypes.Id, utilisateur.Id.ToString()),
                new Claim(InfecoClaimTypes.AgenceImmobiliereId, utilisateur.AgenceImmobiliereId.ToString()),
                new Claim(InfecoClaimTypes.Prenom, utilisateur.Prenom),
                new Claim(InfecoClaimTypes.Password, utilisateur.Password),
                new Claim(InfecoClaimTypes.Nom, utilisateur.Nom),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var clé = new SymmetricSecurityKey(_secretJwt);
            var creds = new SigningCredentials(clé, SecurityAlgorithms.HmacSha256);
            if (_authentificationSettings?.DureeValiditeAccessToken != null)
            {
                var token = new JwtSecurityToken(_jwtOptions?.Issuer,
               _jwtOptions?.Audience,
               claims,
               expires: DateTime.Now.AddSeconds(_authentificationSettings.DureeValiditeAccessToken),
               signingCredentials: creds);
                
               return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }

        public string? DonneRefreshToken(string login)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var clé = new SymmetricSecurityKey(_secretJwt);
            var creds = new SigningCredentials(clé, SecurityAlgorithms.HmacSha256);
            if (_authentificationSettings?.DureeValiditeRefreshToken != null)
            {
                var token = new JwtSecurityToken(_jwtOptions?.Issuer,
                _jwtOptions?.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_authentificationSettings.DureeValiditeRefreshToken),
                signingCredentials: creds);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return null;
        }
    }
}
