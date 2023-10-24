namespace Infoeco.infrastructure.Configuration
{
    public interface IAppConfiguration
    {
        DatabaseSettings DatabaseSettings { get; set; }
        AuthentificationSettings AuthentificationSettings { get; set; }
        JwtOptions JwtOptions { get; set; }
    }
}
