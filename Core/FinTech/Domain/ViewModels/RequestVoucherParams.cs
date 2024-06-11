using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.ViewModels
{
  public class RequestVoucherParams
  {
    public string RWLMemberId { get; set; }
    public string RequestId { get; set; }
    public string CourseId { get; set; }
  }
}
