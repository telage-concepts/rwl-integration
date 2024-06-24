using Core.FinTech.Domain.DataAccess.Interfaces;
using Core.FinTech.Domain.Entities;
using Core.FinTech.Domain.Entities.Identity;
using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FintechSim.Pages
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly IRWLService _rdfService;
    private readonly IUnitOfWork<UserProfile> unitOfWork;
    private readonly IUnitOfWork<DiscountWallet> dwunitOfWork;
    private readonly UserManager<ApplicationUser> userManager;
    public int MemberId { get; set; }
    public UserProfile UserProfile { get; set; }

    public IndexModel(IRWLService rdfService, IUnitOfWork<UserProfile> unitOfWork, UserManager<ApplicationUser> userManager, IUnitOfWork<DiscountWallet> dwunitOfWork)
    {
      _rdfService = rdfService;
      this.unitOfWork = unitOfWork;
      this.userManager = userManager;
      this.dwunitOfWork = dwunitOfWork;
    }

    [BindProperty]
    public PageVM? pageVM { get; set; }
    public async Task<IActionResult> OnGetAsync()
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).First();
      if(dwunitOfWork.Repository.Get(x => x.UserProfileId == UserProfile.Id).FirstOrDefault() == null)
      {
        await dwunitOfWork.Repository.Insert(new DiscountWallet { UserProfileId = UserProfile.Id });
        await dwunitOfWork.SaveAsync();
      }
      return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).First();
      if (UserProfile.RwlMemberId == null)
      {
        var temp = await _rdfService.Initialize(new RWLUserDetail
        {
          Email = UserProfile.Email,
          FirstName = UserProfile.FirstName,
          LastName = UserProfile.LastName,
          PhoneNumber = UserProfile.PhoneNumber
        });
        Console.WriteLine("");
        Console.WriteLine("RWL Member ID : {0}", temp.Data.RwlMemberId);
        Console.WriteLine("");

        UserProfile.RwlMemberId = temp.Data.RwlMemberId;

        unitOfWork.Repository.Update(UserProfile);
        await unitOfWork.SaveAsync();
      }
      var data = await _rdfService.GetCourseDetails(pageVM.CourseCode);
      return RedirectToPage("RequestVoucher", data.Data);
    }

    public class PageVM
    {
      public string MemberId { get; set; }
      public string CourseCode { get; set; }
    }
  }
}
