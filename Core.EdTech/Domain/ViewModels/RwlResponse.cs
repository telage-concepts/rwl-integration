using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.EdTech.Domain.ViewModels
{
  public class RwlResponse<T> where T : class
  {
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("data")]
    public T Data { get; set; }
  }
}
