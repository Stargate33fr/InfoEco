namespace Infoeco.infrastructure.Configuration
{
    public class AuthentificationSettings
    {
        public int DureeValiditeAccessToken { get; set; }
        public int DureeValiditeRefreshToken { get; set; }
        public string SecretKey { get; set; } = string.Empty;
        public string? PasseSecret { get; set; }
    }
}
