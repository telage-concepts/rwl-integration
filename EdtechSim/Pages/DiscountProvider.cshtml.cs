using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EdtechSim.Pages
{
  public class DiscountProviderModel : PageModel
  {
    private readonly ICourseManagementService _courseManagementService;
    private readonly IUnitOfWork<UserProfile> unitOfWork;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IRWLService rWLService;

    public DiscountProviderModel(ICourseManagementService courseManagementService, IUnitOfWork<UserProfile> unitOfWork, UserManager<ApplicationUser> userManager, IRWLService rWLService)
    {
      _courseManagementService = courseManagementService;
      this.unitOfWork = unitOfWork;
      this.userManager = userManager;
      this.rWLService = rWLService;
    }

    public List<SelectListItem> ServiceProviderList { get; set; } = new List<SelectListItem>();
    public Course? Course { get; set; }
    [BindProperty]
    public string DiscountProviderId { get; set; }
    public UserProfile UserProfile { get; set; }
    public async Task<IActionResult> OnGetAsync(Guid Id)
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).FirstOrDefault();
      Course = await _courseManagementService.GetCourse(Id);

      foreach(var item in (await rWLService.GetServiceProviders(UserProfile.RwlMemberId.Value)).Data)
      {
        ServiceProviderList.Add(new SelectListItem {
          Text = item.ServiceProviderName,
          Value = item.ServiceProviderId.ToString()
        });
      }
      return Page();
    }

    public async Task<IActionResult> OnPostComputeDiscount()
    {
      return RedirectToPage();
    }
  }
}
