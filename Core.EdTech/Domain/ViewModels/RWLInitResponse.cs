using System.Text.Json.Serialization;

namespace Core.EdTech.Domain.ViewModels
{
  public class RWLInitResponse
  {
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("data")]
    public ClassData Data { get; set; }

    public class ClassData
    {
      [JsonPropertyName("rwl_member_id")]
      public int? RwlMemberId { get; set; } = null;
    }
  }
}
