using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Infrastructure.Services.Abstracts
{
  public interface IDiscountWalletManager
  {
    public Task<bool> AddBalance(Guid Id, decimal Amount);
    public Task<bool> DeductBalance(Guid Id, decimal Amount);
  }
}
