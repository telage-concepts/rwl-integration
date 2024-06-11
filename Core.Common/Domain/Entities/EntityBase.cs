using System.ComponentModel;

namespace Core.Common.Domain.Entities
{
  public abstract class EntityBase
  {
    public Guid Id { get; set; }

    public Guid? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }

    public Guid? LastUpdatedBy { get; set; }
    public DateTime? LastUpdatedOn { get; set; }
    public bool IsDeleted { get; set; } = false;

    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedOn { get; set; }
    [DefaultValue(true)]
    public bool IsActive { get; set; } = true;
  }
}
