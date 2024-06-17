using Core.Common.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.FinTech.Domain.Entities
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

    public override string ToString()
    {
      return FirstName + " " + LastName;
    }

    public int Age()
    {
      var today = DateTime.Today;
      var age = today.Year - DateOfBirth.Year;
      if (DateOfBirth.Date > today.AddYears(-age)) age--;

      return age;
    }
  }
}
