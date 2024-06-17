using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Infrastructure.Services.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Infrastructure.Services.Implementations
{
  public class DiscountWalletManager : IDiscountWalletManager
  {
    private readonly IUnitOfWork<DiscountWallet> unitOfWork;

    public DiscountWalletManager(IUnitOfWork<DiscountWallet> unitOfWork)
    {
      this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddBalance(Guid Id, decimal Amount)
    {
      var wallet = unitOfWork.Repository.Get(x => x.Id == Id).First();
      wallet.Balance += Amount;
      unitOfWork.Repository.Update(wallet);
      await unitOfWork.SaveAsync();
      return true;
    }

    public async Task<bool> DeductBalance(Guid Id, decimal Amount)
    {
      var wallet = unitOfWork.Repository.Get(x => x.Id == Id).First();
      if (wallet.Balance >= Amount)
      {
        wallet.Balance -= Amount;
        unitOfWork.Repository.Update(wallet);
        await unitOfWork.SaveAsync();
        return true;
      }
      return false;
    }
  }
}
