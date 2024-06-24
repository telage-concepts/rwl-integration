using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Domain.Entities;

namespace Core.EdTech.Domain.Entities
{
  public class Course : EntityBase
  {
    public string Name { get; set; }
    public string Price { get; set; }
    public string ImageUrl { get; set; }
    public string RwlCode { get; set; }
  }
}
