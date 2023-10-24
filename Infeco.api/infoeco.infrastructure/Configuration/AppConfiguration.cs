namespace Infoeco.infrastructure.Configuration
{
    public class AppConfiguration : IAppConfiguration
    {
        public DatabaseSettings DatabaseSettings { get; set; } = new DatabaseSettings();
        public AuthentificationSettings AuthentificationSettings { get; set; } = new AuthentificationSettings();
        public JwtOptions JwtOptions { get; set; } = new JwtOptions();
    }
}
