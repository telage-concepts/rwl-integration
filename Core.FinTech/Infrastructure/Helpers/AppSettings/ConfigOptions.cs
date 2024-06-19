using Microsoft.Extensions.Options;
using System.Security.Cryptography.X509Certificates;

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
        public Paystack Paystack => config.Value.Paystack;
  }
}
