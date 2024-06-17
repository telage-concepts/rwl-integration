using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Core.FinTech.Domain.Entities.Identity;
using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FintechSim.Pages { 

  [Authorize]
    public class RequestVoucherModel : PageModel
    {
        private readonly IRWLService _rdfService;
        private readonly IUnitOfWork<UserProfile> unitOfWork;
        private readonly UserManager<ApplicationUser> userManager;

    public RequestVoucherModel(IRWLService rdfService, IUnitOfWork<UserProfile> unitOfWork, UserManager<ApplicationUser> userManager)
    {
      _rdfService = rdfService;
      this.unitOfWork = unitOfWork;
      this.userManager = userManager;
    }

    public string BaseUrl { get; set; }
        public CourseDetails.ClassData Data { get; set; }
        public UserProfile UserProfile { get; set; }
        public string ProgramImage { get; set; }
        public string ProgramFee { get; set; }
        public string ProgramName { get; set; }
        public string ProgramCode { get; set; }
        public async Task<IActionResult> OnGetAsync(CourseDetails.ClassData data)
        {
          var user = await userManager.GetUserAsync(User);
          UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).FirstOrDefault();
          if (data == null) return NotFound();

          Data = data;
          BaseUrl = $"{Request.Scheme}://{Request.Host.Value}";
          return Page();
        }
    }
}
