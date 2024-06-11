using Core.EdTech.Domain.ViewModels.Authentication;
using Core.EdTech.Domain.ViewModels.Mail;
using Core.EdTech.Infrastructure.Helpers.AppSettings;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EdtechSim.Pages
{
  public class RegisterModel : PageModel
    {
        private readonly ConfigOptions configOptions;
        private readonly IMailService mailService;
        private readonly IAuthenticationService authenticationService;
        public string? ReturnUrl { get; set; }

        public RegisterModel(ConfigOptions configOptions, IMailService mailService, IAuthenticationService authenticationService)
        {
          this.configOptions = configOptions;
          this.mailService = mailService;
          this.authenticationService = authenticationService;
          ReturnUrl = "./EmailConfirmation";
    }

        [BindProperty]
        public RegistrationVM RegistrationVM { get; set; }
        public SignInVM? SignInVM { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(RegistrationVM?.Input == null || RegistrationVM?.UserProfile == null)
            {
                return Page();
            }

            RegistrationVM.UserProfile.Email = RegistrationVM.Input.Email;
            ModelState.ClearValidationState(nameof(RegistrationVM));

            if (TryValidateModel(RegistrationVM, nameof(RegistrationVM)))
            {
              var result = await authenticationService.SignUp(RegistrationVM);

              if (result)
              {
                SignInVM = new SignInVM
                {
                  Email = RegistrationVM.Input.Email,
                  Password = RegistrationVM.Input.Password
                };
              }
              var callbackUrl = await authenticationService.GenerateEmailConfirmationTokenUrl(RegistrationVM.Input.Email, Request.Scheme);
              MailDataVM mailDataVM = new MailDataVM
              {
                EmailToId = RegistrationVM.Input.Email,
                EmailToName = "",
                EmailSubject = "GetOnline Email Confirmation",
                EmailBody = $"Kindly confirm your email at {callbackUrl}"
              };
              await mailService.SendMailAsync(mailDataVM);

              return Redirect(ReturnUrl);
            }

            else
            {
              foreach (var error in ViewData.ModelState.Values.SelectMany(modelState => modelState.Errors))
              {
                string messages = string.Join("; ", ModelState.Values
                                    .SelectMany(x => x.Errors)
                                    .Select(x => x.ErrorMessage));
                Console.WriteLine(messages);
              }
      }
      return Page();
        }
  }
}
