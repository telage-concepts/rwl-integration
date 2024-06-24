using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Core.EdTech.Domain.ViewModels;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EdtechSim.Pages
{
  [Authorize]
  public class CourseDetailModel : PageModel
  {
    private readonly ICourseManagementService _courseManagementService;
    private readonly IUnitOfWork<UserProfile> unitOfWork;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IRWLService rWLService;

    public CourseDetailModel(ICourseManagementService courseManagementService, IUnitOfWork<UserProfile> unitOfWork, UserManager<ApplicationUser> userManager, IRWLService rWLService)
    {
      _courseManagementService = courseManagementService;
      this.unitOfWork = unitOfWork;
      this.userManager = userManager;
      this.rWLService = rWLService;
    }

    public UserProfile UserProfile { get; set; }

    [BindProperty]
    public Guid CourseId { get; set; }
    public Course? Course { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid Id)
    {
      Course = await _courseManagementService.GetCourse(Id);
      return Page();
    }

    public async Task<IActionResult> OnPostGetDiscount()
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).FirstOrDefault();
      if(UserProfile.RwlMemberId == null)
      {
        UserProfile.RwlMemberId = (await rWLService.Initialize(new RWLUserDetail
        {
          FirstName = UserProfile.FirstName,
          LastName = UserProfile.LastName,
          Email = UserProfile.Email,
          PhoneNumber = UserProfile.PhoneNumber
        })).Data.RwlMemberId;

        unitOfWork.Repository.Update(UserProfile);
        await unitOfWork.SaveAsync();
      }
      return RedirectToPage("/DiscountProvider", new { Id = CourseId});
    }
  }
}
