using InfoEco.Domain.Authentification;
using System.Security.Claims;

namespace IDSoft.CrmWelcome.Infrastructure.Helpers
{
    public static class OutilsWeb
    {
        public static int? DonneIdUtilisateurAuthentifié(this HttpContext httpContext)
        {
            if (httpContext.User.Identity != null)
            {
                return DonneValeurClaim<int>((ClaimsIdentity)httpContext.User.Identity, InfecoClaimTypes.Id);
            }
            return null;
        }
        public static int? DonneIdAgenceUtilisateurAuthentifié(this HttpContext httpContext)
        {
            if (httpContext.User.Identity != null)
            {
                return DonneValeurClaim<int>((ClaimsIdentity)httpContext.User.Identity, InfecoClaimTypes.AgenceImmobiliereId);
            }
            return null;
        }

        private static T? DonneValeurClaim<T>(ClaimsIdentity identity, string claimType)
        {
            var claim = identity.Claims?.SingleOrDefault(x => x.Type == claimType);
            if (claim != default)
                return (T)Convert.ChangeType(claim.Value, typeof(T));

            return default;
        }
    }
}
