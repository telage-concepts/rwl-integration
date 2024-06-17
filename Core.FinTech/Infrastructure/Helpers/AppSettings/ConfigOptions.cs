using Microsoft.Extensions.Options;

namespace Core.FinTech.Infrastructure.Helpers.AppSettings
{
    public class ConfigOptions
    {
        private readonly IOptions<ConfigProviders> config;

        public ConfigOptions(IOptions<ConfigProviders> config)
        {
            this.config = config;
        }
        public MailSettings MailSettings => config.Value.MailSettings;
        public RWLParams RWLParams => config.Value.RWLParams;
  }
}
