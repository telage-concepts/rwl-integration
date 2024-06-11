using System.ComponentModel.DataAnnotations;

namespace Core.Common.Domain.ViewModels.Authentication
{
  public class RegistrationVM
  {
    [Required]
    public InputModel Input { get; set; }
    public EdTech.Domain.Entities.UserProfile? UserProfileEd { get; set; }
    public FinTech.Domain.Entities.UserProfile? UserProfileFin { get; set; }
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
