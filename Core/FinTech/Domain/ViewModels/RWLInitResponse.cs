using System.Text.Json.Serialization;

namespace Core.FinTech.Domain.ViewModels
{
  public class RWLInitResponse
  {
    [JsonPropertyName("rwl_member_id")]
    public string RwlMemberId { get; set; }
  }
}
