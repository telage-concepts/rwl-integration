namespace Core.FinTech.Infrastructure.Helpers.AppSettings
{
    public class ConfigProviders
    {
        public required MailSettings MailSettings { get; set; }
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

}
