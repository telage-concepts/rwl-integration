using Core.FinTech.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace FintechSim.Controllers
{
  public class ComputeDiscountController : Controller
  {
    private readonly IRWLService rWLService;
    public async Task<IActionResult> Index(Request request)
    {
      var data = await rWLService.ComputeDiscount(request.rwl_program_code, request.rwl_member_id);
      return Ok(new
      {
        discount_percentage = data.DiscountPercentage,
        multiple = data.Multiple
      });
    }

    public class Request{
      public string rwl_program_code { get; set; }
      public int rwl_member_id { get; set; }
    }
  }
}
