using Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.Entities
{
  public class Voucher : EntityBase
  {
    public string Code { get; set; }
    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
    public string ProgramName { get; set; }
    public string ProgramCode { get; set; }
  }
}
