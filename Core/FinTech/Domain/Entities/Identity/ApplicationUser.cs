using Microsoft.AspNetCore.Identity;

namespace Core.FinTech.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? LastLoginDate { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public bool Disabled { get; set; }
        public bool MustChangePassword { get; set; }
    }
}
