using Core.EdTech.Domain.DataAccess.Interfaces;
using Core.EdTech.Domain.Entities;
using Core.EdTech.Domain.Entities.Identity;
using Core.EdTech.Domain.ViewModels;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EdtechSim.Pages
{
  [Authorize]
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly IRWLService _rdfService;
    private readonly IUnitOfWork<UserProfile> unitOfWork;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICourseManagementService courseManagementService;

    public List<Course> AllCourses { get; set; }
    public UserProfile UserProfile { get; set; }
    public List<ServiceProviderResponse> ServiceProviderResponses { get; set; }
    public IndexModel(ILogger<IndexModel> logger, IRWLService rdfService, IUnitOfWork<UserProfile> unitOfWork, UserManager<ApplicationUser> userManager, ICourseManagementService courseManagementService)
    {
      _logger = logger;
      _rdfService = rdfService;
      this.unitOfWork = unitOfWork;
      this.userManager = userManager;
      this.courseManagementService = courseManagementService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
      var user = await userManager.GetUserAsync(User);
      UserProfile = unitOfWork.Repository.Get(x => x.Email == user.Email).FirstOrDefault();

      if (UserProfile.RwlMemberId == null)
      {
        UserProfile.RwlMemberId = (await _rdfService.Initialize(new RWLUserDetail
        {
          Email = UserProfile.Email,
          FirstName = UserProfile.FirstName,
          LastName = UserProfile.LastName,
          PhoneNumber = UserProfile.PhoneNumber
        })).Data.RwlMemberId;
      }


      AllCourses = await courseManagementService.GetCourses();
      return Page();
    }
  }
}
