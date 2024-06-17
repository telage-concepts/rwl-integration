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
    [JsonPropertyName("message")]
    public string Message { get; set; }
    [JsonPropertyName("data")]
    public ClassData Data { get; set; }

    public class ClassData
    {
      [JsonPropertyName("program_name")]
      public string ProgramName { get; set; }
      [JsonPropertyName("program_code")]
      public string ProgramCode { get; set; }
      [JsonPropertyName("program_image")]
      public string ProgramImage { get; set; }
      [JsonPropertyName("program_fee")]
      public string ProgramFee { get; set; }
    }
  }
}
