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
    [JsonPropertyName("service_provider_id")]
    public string ServiceProviderId { get; set; }
    [JsonPropertyName("service_provider_name")]
    public string ServiceProviderName { get; set; }
  }
}
