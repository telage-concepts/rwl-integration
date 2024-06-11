using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Domain.Entities;

namespace Core.EdTech.Domain.Entities
{
  public class Voucher : EntityBase
  {
    public string Code { get; set; }
    public string RequestId { get; set; }
  }
}
