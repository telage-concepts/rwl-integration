namespace Core.Infrastructure.Helpers.AppSettings
{
    public class ConfigProviders
    {
        public required Coinbase Coinbase { get; set; }
        public required Bybit Bybit { get; set; }
        public required Stellar Stellar { get; set; }
        public required MailSettings MailSettings { get; set; }
    }

    public class Coinbase
    {
        public string BaseUrl { get; set; }
        public WalletSettings WalletSettings { get; set; }
    }

    public class Bybit
    {
        public string BaseUrl { get; set; }
    }

    public class Stellar
    {
        public string ServerUrl { get; set; }
        public string USDCIssuerAddress { get; set; }
    }

    public class MailSettings
    {
      public string Server { get; set; }
      public int Port { get; set; }
      public string SenderName { get; set; }
      public string SenderEmail { get; set; }
      public string UserName { get; set; }
      public string Password { get; set; }
    }

    public class WalletSettings
    {
      public string ApiKey { get; set; }
      public string ApiSecret { get; set; }
      public string? Passphrase { get; set; }
      public string AssetCode { get; set; }
  }
}
