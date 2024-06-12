using Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.EdTech.Domain.Entities
{
  public class DiscountWallet : EntityBase
  {
    public WalletCurrency Currency { get; set; }
    public decimal Balance { get; set; }
    public Guid UserProfileId { get; set; }
    public UserProfile? UserProfile { get; set; }
  }

  public enum WalletCurrency
  {
    Naira
  }
}
