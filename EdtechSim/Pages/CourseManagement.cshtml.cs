using Core.EdTech.Domain.Entities;
using Core.EdTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EdtechSim.Pages
{
  [Authorize]
  public class CourseManagementModel : PageModel
  {
    private readonly ICourseManagementService courseManagementService;
    private readonly ILogger<CourseManagementModel> logger;
    public List<Course> CourseList { get; set; }
    [BindProperty]
    public Course AddCourse { get; set; }

    public CourseManagementModel(ICourseManagementService courseManagementService, ILogger<CourseManagementModel> logger)
    {
      this.courseManagementService = courseManagementService;
      this.logger = logger;
    }

    public async Task<IActionResult> OnGetAsync()
    {
      CourseList = await courseManagementService.GetCourses();
      return Page();
    }

    public async Task<IActionResult> OnPostAddCourse()
    {
      logger.LogWarning("Beginning Add course function");
      await courseManagementService.AddCourse(AddCourse);
      return RedirectToPage();
    }
  }
}
