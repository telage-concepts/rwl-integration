using Microsoft.Extensions.Options;

namespace Core.Infrastructure.Helpers.AppSettings
{
    public class ConfigOptions
    {
        private readonly IOptions<ConfigProviders> config;

        public ConfigOptions(IOptions<ConfigProviders> config)
        {
            this.config = config;
        }

        public Coinbase Coinbase => config.Value.Coinbase;
        public Bybit Bybit => config.Value.Bybit;
        public Stellar Stellar => config.Value.Stellar;
        public MailSettings MailSettings => config.Value.MailSettings;
  }
}
