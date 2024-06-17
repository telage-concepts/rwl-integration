using System.Text.Json.Serialization;

namespace Core.EdTech.Domain.ViewModels
{
  public class RWLInitResponse
  {
    [JsonPropertyName("rwl_member_id")]
    public string RwlMemberId { get; set; }
  }
}
