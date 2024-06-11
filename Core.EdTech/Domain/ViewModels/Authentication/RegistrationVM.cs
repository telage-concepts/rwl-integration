using Core.EdTech.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Core.EdTech.Domain.ViewModels.Authentication
{
  public class RegistrationVM
  {
    [Required]
    public InputModel Input { get; set; }
    [Required]
    public UserProfile UserProfile { get; set; }
  }

  public class InputModel
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
  }
}
