using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.EdTech.Domain.ViewModels
{
  public class RWLUserDetail
  {
    [JsonPropertyName("email")]
    public string Email { get; set; }
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    [JsonPropertyName("phone")]
    public string PhoneNumber { get; set; }
  }
}
