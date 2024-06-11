using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Abstracts
{
  public interface IVoucherManagementService
  {
    public Task CreateVoucher();
    public Task RedeemVoucher();

  }
}
