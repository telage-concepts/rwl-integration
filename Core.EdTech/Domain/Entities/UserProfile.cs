using System.ComponentModel.DataAnnotations;
using Core.Common.Domain.Entities;

namespace Core.EdTech.Domain.Entities
{
  [Microsoft.EntityFrameworkCore.Index(nameof(Email), IsUnique = true)]
  [Microsoft.EntityFrameworkCore.Index(nameof(PhoneNumber), IsUnique = true)]
  public class UserProfile : EntityBase
    {
    [Required, StringLength(128)]
    public string FirstName { get; set; }
    [Required, StringLength(128)]
    public string LastName { get; set; }
    [Required, StringLength(126)]
    public string Email { get; set; }
    [Required, StringLength(32)]
    public string PhoneNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public int? RwlMemberId { get; set; }
  }
}
