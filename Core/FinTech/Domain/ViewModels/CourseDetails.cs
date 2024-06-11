using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.ViewModels
{
  public class CourseDetails
  {
    [JsonPropertyName("program_name")]
    public string ProgramName { get; set; }
    [JsonPropertyName("program_code")]
    public string ProgramCode { get; set; }
    [JsonPropertyName("program_image")]
    public string ProgramImage { get; set; }
    [JsonPropertyName("program_fee")]
    public decimal ProgramFee { get; set; }
    [JsonPropertyName("member_id")]
    public string MemberId { get; set; }
  }
}
