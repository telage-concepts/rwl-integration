using Core.FinTech.Domain.ViewModels.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Core.FinTech.Infrastructure.Services.Abstracts
{
    public interface IAuthenticationService 
    {
        Task<bool> SignUp(RegistrationVM registrationVM);
        Task<SignInResult> SignIn(SignInVM signInVM, List<Claim>? claims = null);
        Task<string> GeneratePasswordResetTokenUrl(string email, string scheme);
        Task<bool> ResetPassword(ResetPasswordVM ResetPasswordVM);
        Task<string> GenerateEmailConfirmationTokenUrl(string email, string scheme);
        Task<bool> ConfirmEmail(string token, string email);
    }
}
