using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Core.FinTech.Domain.Entities.Identity;
using Core.FinTech.Domain.ViewModels.Authentication;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Core.FinTech.Infrastructure.Services.Implementations
{
  public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IUnitOfWork<UserProfile> userProfileUoW;
        private readonly ILogger<AuthenticationService> logger;

        public AuthenticationService(ILogger<AuthenticationService> logger,
         UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
         IUnitOfWork<UserProfile> userProfileUoW)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.userProfileUoW = userProfileUoW;
            this.logger = logger;
        }

        public async Task<bool> SignUp(RegistrationVM registrationVM)
        {
            IdentityResult result;
            var user = new ApplicationUser{
                UserName = registrationVM.Input?.Email,
                Email = registrationVM.Input?.Email,
                Disabled = false,
                MustChangePassword = false
            };
            var resultCheck = true;

            if(await userManager.FindByEmailAsync(registrationVM.Input?.Email) == null)
            {
                result = await userManager.CreateAsync(user, registrationVM.Input?.Password);
                resultCheck =result.Succeeded;
            }


            if(resultCheck)
            {
                logger.LogInformation("User account created");
                await userProfileUoW.Repository.Insert(registrationVM.UserProfile);
                await userProfileUoW.SaveAsync();
                await signInManager.SignInAsync(user, isPersistent: false);
            }

            return resultCheck;
        }

        public async Task<SignInResult> SignIn(SignInVM signInVM, List<Claim>? claims = null)
        {
            var result = await signInManager.PasswordSignInAsync(signInVM.Email, signInVM.Password, signInVM.RememberMe, lockoutOnFailure: false);
            if(result.Succeeded && claims != null)
            {
                var user = await userManager.FindByEmailAsync(signInVM.Email);
                foreach(var claim in await userManager.GetClaimsAsync(user))
                {
                    if(claims.Where(c => c.Type == claim.Type).FirstOrDefault() != null)
                    {
                        await userManager.RemoveClaimAsync(user, claim);
                    }
                }
                await userManager.AddClaimsAsync(user, claims);
            }
            return result;
        }


        public async Task<bool> ResetPassword(ResetPasswordVM ResetPasswordVM)
        {
            var user = await userManager.FindByEmailAsync(ResetPasswordVM.Email);
            if(user == null) return false;
            var token = ResetPasswordVM.Token.Replace(" ", "+");
            var resetPassResult = await userManager.ResetPasswordAsync(user, token, ResetPasswordVM.Password);
            if(!resetPassResult.Succeeded)
            {
                foreach (var item in resetPassResult.Errors)
                {
                    
                    Console.WriteLine(item.Description);
                }
            }
            else
            {
                Console.WriteLine("Password change successful");
            }
            return resetPassResult.Succeeded;
        }


        public async Task<bool> ConfirmEmail(string raw_token, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user == null) return false;
            var token = raw_token.Replace(" ", "+");
            var resetPassResult = await userManager.ConfirmEmailAsync(user, token);
            if(!resetPassResult.Succeeded)
            {
                foreach (var item in resetPassResult.Errors)
                {
                    
                    Console.WriteLine(item.Description);
                }
            }
            else
            {
                Console.WriteLine("User email confirmed");
            }
            return resetPassResult.Succeeded;
        }

        public Task<string> GeneratePasswordResetTokenUrl(string email, string scheme)
        {
          throw new NotImplementedException();
        }

        public Task<string> GenerateEmailConfirmationTokenUrl(string email, string scheme)
        {
          throw new NotImplementedException();
        }
  }
}
