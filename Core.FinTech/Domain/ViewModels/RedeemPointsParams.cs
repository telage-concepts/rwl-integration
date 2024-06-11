using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.ViewModels
{
  public class RedeemPointsParams
  {
    public string RequestId { get; set; }
    public string MemberRWLId { get; set; }
    public decimal ProgramPrice { get; set; }
    public int Multiple { get; set; }
  }
}
