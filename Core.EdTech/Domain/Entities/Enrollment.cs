using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Domain.Entities;

namespace Core.EdTech.Domain.Entities
{
  public class Enrollment : EntityBase
  {
    public Guid CourseId { get; set; }
    public Course Course { get; set;}
    public Guid UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }
  }
}
