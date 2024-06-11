using Core.FinTech.Domain.ViewModels.Authentication;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FintechSim.Pages
{
  public class LoginModel : PageModel
    {
        private readonly IAuthenticationService authenticationService;

        public LoginModel(IAuthenticationService authenticationService)
        {
          this.authenticationService = authenticationService;
        }

        [BindProperty]
        public SignInVM? SignInVM { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = "/Index")
        {
            if(SignInVM == null)
            {
                return Page();
            }

            var result = await authenticationService.SignIn(SignInVM, null);
            if(result.Succeeded)
            {
                var user = HttpContext.User;

                return Redirect(returnUrl);
            }
            return Page();
        }
    }
}
