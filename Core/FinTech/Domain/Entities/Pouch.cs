using Core.Common.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.FinTech.Domain.Entities
{
  public class Pouch : EntityBase
  {
    public Guid UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
    public int Balance { get; set; }
  }
}
