using Core.FinTech.Domain.ViewModels;
using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FintechSim.Pages
{
  public class IndexnModel : PageModel
  {
    private readonly ILogger<IndexnModel> _logger;
    private readonly IRWLService rWLService;

    [BindProperty]
    public IndexPageVM IndexPageVM { get; set; }
    public IndexnModel(ILogger<IndexnModel> logger, IRWLService rWLService)
    {
      _logger = logger;
      this.rWLService = rWLService;
    }

    public void OnGet()
    {

    }

    public async Task<IActionResult> OnPostAsync()
    {
      var init_response = await rWLService.Initialize(new RWLUserDetail {
        Email = IndexPageVM.Email,
        FirstName = IndexPageVM.FirstName,
        LastName = IndexPageVM.LastName,
        PhoneNumber = IndexPageVM.PhoneNumber
      });

      return RedirectToPage("/FindCourse", new { RwlMemberId = init_response.RwlMemberId});
    }
  }
}
