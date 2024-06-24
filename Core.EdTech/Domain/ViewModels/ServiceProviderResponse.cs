using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.EdTech.Domain.ViewModels
{
  public class ServiceProviderResponse
  {
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("data")]
    public List<ClassData> Data { get; set; }
    public class ClassData
    {
      [JsonPropertyName("service_provider_id")]
      public int ServiceProviderId { get; set; }
      [JsonPropertyName("service_provider_name")]
      public string ServiceProviderName { get; set; }
      [JsonPropertyName("service_provider_logo")]
      public string ServiceProviderLogo { get; set; }
    }

  }
}
